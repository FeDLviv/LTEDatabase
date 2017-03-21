using LTEDatabase.Model;
using LTEDatabase.Command;

using System;
using System.Linq;

using System.Threading.Tasks;
using System.Data.Entity;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;

namespace LTEDatabase.ViewModel 
{
    class MainViewModel : BaseViewModel
    {
        private object locker = new object();
        private bool isSelectedObjectsLeaf = true;

        private ObjectsViewModel objects;
        public ObjectsViewModel Objects
        {
            get { return objects; }
            set
            {
                if (objects != value)
                {
                    objects = value;
                    OnPropertyChanged("Objects");
                }
            }
        }

        private objects selectedObject;
        public objects SelectedObject 
        { 
            get 
            {
                return selectedObject; 
            }

            set 
            {
                if(selectedObject != value)
                {
                    selectedObject = value;
                    OnPropertyChanged("SelectedObject");
                }
            }
        }

        private Collection<motors_lte> motors;
        public Collection<motors_lte> Motors 
        {
            get { return motors; }
            set 
            {
                if (motors != value)
                {
                    motors = value;
                    OnPropertyChanged("Motors");
                }
            }
        }

        private bool isStartWork = false;
        public bool IsStartWork 
        {
            get { return isStartWork; }
            set
            {
                if (isStartWork != value)
                {
                    isStartWork = value;
                    OnPropertyChanged("IsStartWork");
                }
            }
        }
                    
        private bool isUpdateObjectsCommand = true;
        public bool IsUpdateObjectsCommand 
        {
            get { return isUpdateObjectsCommand; }
            set
            {
                if (isUpdateObjectsCommand != value)
                {
                    isUpdateObjectsCommand = value;
                    OnPropertyChanged("IsUpdateObjectsCommand");
                }
            }
        }

        public ICommand SelectedObjectsLeaf { set; get; }
        public ICommand ShowDetailsCommand { set; get; }
        public ICommand UpdateObjectsCommand { set; get; }
        public ICommand ExitCommand { set; get; }
               
        public MainViewModel()
        {
            System.Diagnostics.Stopwatch time = new System.Diagnostics.Stopwatch();
            time.Start();
            Objects = new ObjectsViewModel();
            time.Stop();
            Console.WriteLine("Create ObjectsViewModel={0}", time.ElapsedMilliseconds);

            //time.Restart();

            SelectedObjectsLeaf = new BaseCommand(DoSelectedObjectsList);
            ShowDetailsCommand = new BaseCommand(DoShowDetailsCommand);
            UpdateObjectsCommand = new BaseCommand(DoUpdateObjectsCommand, CanUpdateObjectsCommand);
            ExitCommand = new BaseRoutedCommand(DoMainWindowClosing);
        }

        private void DoSelectedObjectsList(object obj)
        {
            SelectedObject = obj as objects;
            if (SelectedObject != null && isSelectedObjectsLeaf)
            {
                isSelectedObjectsLeaf = false;
                IsStartWork = true;
                Motors = null;
                Task.Factory.StartNew(UpdateMotors, SelectedObject);
            }
        }

        private void UpdateMotors(object obj)
        {
            lock (locker)
            {
                try
                {
                    objects temp = obj as objects;
                    if (temp != null && temp == SelectedObject)
                    {
                        Motors = new ObservableCollection<motors_lte>(
                                 (from x in Database.GetContext().motors_lte
                                 where x.idObject == temp.idObject
                                 select x).Include("missions").AsNoTracking().ToList());
                    }
                }
                catch (Exception ex)
                {
                    IsStartWork = false;
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }));
                }
                finally
                {
                    IsStartWork = false;
                    isSelectedObjectsLeaf = true;
                }
            }
        }

        private void DoShowDetailsCommand(object obj)
        {
            DataGrid table = obj as DataGrid;
            if (table != null)
            {
                DataGridRow row = table.ItemContainerGenerator.ContainerFromIndex(table.SelectedIndex) as DataGridRow;
                if (row != null)
                {
                    row.DetailsVisibility = (row.DetailsVisibility == Visibility.Collapsed) ? Visibility.Visible : Visibility.Collapsed;
                }
            }
        }

        private void DoUpdateObjectsCommand(object obj)
        {
            IsUpdateObjectsCommand = false;
            IsStartWork = true;
            Task.Factory.StartNew(() =>
            {
                try
                {
                    Objects = new ObjectsViewModel();
                }
                catch (Exception ex)
                {
                    IsStartWork = false;
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }));
                }
                finally
                {
                    IsStartWork = false;
                    IsUpdateObjectsCommand = true;
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        CommandManager.InvalidateRequerySuggested();
                    }));
                }
            });
        }

        private bool CanUpdateObjectsCommand(object obj)
        {
            return IsUpdateObjectsCommand;
        }

        private static void DoMainWindowClosing(object sender, object args)
        {
            Window temp = sender as Window;
            CancelEventArgs ev = args as CancelEventArgs;
            if (temp != null && ev != null)
            {
                MessageBoxResult result = MessageBox.Show("Вийти з програми?", temp.Title, MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Database.Close();
                }
                else
                {
                    ev.Cancel = true;
                }
            }
        }

        //public void DoMainWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        //{
        //    //БАЖАНО ПЕРЕНЕСТИ В КОМАНДУ
        //    Window temp = sender as Window;
        //    if (temp != null)
        //    {
        //        MessageBoxResult result = MessageBox.Show("Вийти з програми?", temp.Title, MessageBoxButton.YesNo, MessageBoxImage.Question);
        //        if (result == MessageBoxResult.Yes)
        //        {
        //            Database.Close();
        //        }
        //        else
        //        {
        //            e.Cancel = true;
        //        }
        //    }
        //}
    }
}