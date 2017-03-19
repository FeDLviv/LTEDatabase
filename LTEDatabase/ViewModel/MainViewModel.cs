using LTEDatabase.Model;
using LTEDatabase.Command;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using System.Data.Entity;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Controls;
using System.ComponentModel;

namespace LTEDatabase.ViewModel 
{
    class MainViewModel : BaseViewModel
    {
        private object locker = new object();

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

        private Visibility isStartWork = Visibility.Collapsed;
        public Visibility IsStartWork 
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

        private bool isSelectedObjectsLeaf = true;
        public bool IsSelectedObjectsList 
        {
            get { return isSelectedObjectsLeaf; }
            set
            {
                if (isSelectedObjectsLeaf != value)
                {
                    isSelectedObjectsLeaf = value;
                    OnPropertyChanged("IsSelectedObjectsList");
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
          
            SelectedObjectsLeaf = new BaseCommand(DoSelectedObjectsList, CanSelectedObjectsList);
            ShowDetailsCommand = new BaseCommand(DoShowDetailsCommand);
            UpdateObjectsCommand = new BaseCommand(DoUpdateObjectsCommand, CanUpdateObjectsCommand);
            ExitCommand = new ExitCommand();
        }

        private void DoSelectedObjectsList(object obj)
        {
            //ПРОБЛЕМА ВІДОБРАЖЕННЯ ІНФОРМАЦІЇ ПРО ОБ'ЄКТ, КОЛИ ВИБРАНО РАЙОН ЧИ ТИП ОБ'ЄКТА
            //ПРОБЛЕМА ЗМІНИ АДРЕСИ ПРИ ПЕРШОМУ EXCEPTION
            SelectedObject = obj as objects;
            if (SelectedObject != null)
            {
                IsSelectedObjectsList = false;
                IsStartWork = Visibility.Visible;
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
                                 where x.idObject == selectedObject.idObject
                                 select x).Include("missions").AsNoTracking().ToList());
                    }
                }
                catch (Exception ex)
                {
                    IsStartWork = Visibility.Collapsed;
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }));
                }
                finally
                {
                    IsStartWork = Visibility.Collapsed;
                    IsSelectedObjectsList = true;
                }
            }
        }

        private bool CanSelectedObjectsList(object obj)
        {
            return IsSelectedObjectsList;
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
            IsStartWork = Visibility.Visible;
            Task.Factory.StartNew(() =>
            {
                try
                {
                    Objects = new ObjectsViewModel();
                }
                catch (Exception ex)
                {
                    IsStartWork = Visibility.Collapsed;
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }));
                }
                finally
                {
                    IsStartWork = Visibility.Collapsed;
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