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

namespace LTEDatabase.ViewModel 
{
    class MainViewModel : BaseViewModel
    {
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

        public MyCommand UpdateObjectsCommand { set; get; }

        public MyCommand ExitCommand { set; get; }
        
        public MainViewModel()
        {
            System.Diagnostics.Stopwatch time = new System.Diagnostics.Stopwatch();
            time.Start();
            Objects = new ObjectsViewModel();
            time.Stop();
            Console.WriteLine("Create ObjectsViewModel={0}", time.ElapsedMilliseconds);
            
            //time.Restart();
          
            SelectedObjectsLeaf = new MyCommand(DoSelectedObjectsList);
            UpdateObjectsCommand = new MyCommand(DoUpdateObjectsCommand);
            ExitCommand = new MyCommand(DoExitCommand);
        }

        private void DoSelectedObjectsList(object obj)
        {
            SelectedObject = obj as objects;
            if (SelectedObject != null)
            {
                Motors = new ObservableCollection<motors_lte>(
                        (from x in Database.GetContext().motors_lte
                         where x.idObject == SelectedObject.idObject
                         select x).Include("missions").AsNoTracking().ToList());
            }
        }

        private void DoUpdateObjectsCommand(object obj)
        {
            Objects = new ObjectsViewModel();
        }

        private void DoExitCommand(object obj)
        {
            Database.Close();
        }
    }
}