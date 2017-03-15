using LTEDatabase.Model;
using LTEDatabase.Command;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Entity;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Data;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LTEDatabase.ViewModel 
{
    class MainViewModel : BaseViewModel
    {
        private object locked = new object();

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

        private ObservableCollection<motors_lte> motors;
        public ObservableCollection<motors_lte> Motors 
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

        public MyCommand SelectedObjectsLeaf { set; get; }
        public MyCommand ShowDetailsCommand { set; get; }
        public MyCommand UpdateObjectsCommand { set; get; }
               
        public MainViewModel()
        {
            System.Diagnostics.Stopwatch time = new System.Diagnostics.Stopwatch();
            time.Start();
            Objects = new ObjectsViewModel();
            time.Stop();
            Console.WriteLine("Create ObjectsViewModel={0}", time.ElapsedMilliseconds);
            
            //time.Restart();
          
            SelectedObjectsLeaf = new MyCommand(DoSelectedObjectsList);
            ShowDetailsCommand = new MyCommand(DoShowDetailsCommand);
            UpdateObjectsCommand = new MyCommand(DoUpdateObjectsCommand);
        }

        private void DoSelectedObjectsList(object obj)
        {
            SelectedObject = obj as objects;
            Motors = null;
            Task.Factory.StartNew((temp) =>
            {
                lock (locked)
                {
                    try
                    {
                        objects selectedObject = temp as objects;
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
                        Application.Current.Dispatcher.Invoke(new Action(() => 
                        { 
                            MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error); 
                        }));                       
                    }
                }
             }, SelectedObject);
        }

        private void DoShowDetailsCommand(object obj)
        {
            System.Windows.Controls.DataGrid table = obj as System.Windows.Controls.DataGrid;
            if (table != null)
            {
                System.Windows.Controls.DataGridRow row = table.ItemContainerGenerator.ContainerFromIndex(table.SelectedIndex) as System.Windows.Controls.DataGridRow;
                if (row != null)
                {
                    row.DetailsVisibility = (row.DetailsVisibility == Visibility.Collapsed) ? Visibility.Visible : Visibility.Collapsed;
                }
            }
        }

        private void DoUpdateObjectsCommand(object obj)
        {
            Objects = new ObjectsViewModel();
        }

        public void DoMainWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Window temp = sender as Window;
            if (temp != null)
            {
                MessageBoxResult result = MessageBox.Show("Вийти з програми?", temp.Title, MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Database.Close();
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }
    }
}