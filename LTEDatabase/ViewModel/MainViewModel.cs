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
    class MainViewModel : INotifyPropertyChanged
    {
        private DBContext context;

        private MyCommand selectedObjectsLeaf;
        private MyCommand exitCommand;

        private objects selectedObject;
                
        public ObservableCollection<objects> Objects { get; set; }
        public ObservableCollection<motors_lte> MotorsLTE { get; set; }
        public ObservableCollection<Region> Regions { get; set; }

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

        public MyCommand SelectedObjectsLeaf { get { return selectedObjectsLeaf; } }
        public MyCommand ExitCommand { get { return exitCommand; } }
        
        public MainViewModel()
        {
            System.Diagnostics.Stopwatch time = new System.Diagnostics.Stopwatch();
            time.Start();
            context = new DBContext();
            time.Stop();
            Console.WriteLine("Create Context={0}", time.ElapsedMilliseconds);
            
            time.Restart();
            context.objects.Load();
            time.Stop();
            Console.WriteLine("First Load={0}", time.ElapsedMilliseconds);
            
            time.Restart();
            Objects = context.objects.Local;
            time.Stop();
            Console.WriteLine("First Local={0}", time.ElapsedMilliseconds);

            time.Restart();
            context.motors_lte.Include("missions").Load();
            time.Stop();
            Console.WriteLine("Second Load={0}", time.ElapsedMilliseconds);

            time.Restart();
            MotorsLTE = context.motors_lte.Local;
            time.Stop();
            Console.WriteLine("Second Local={0}", time.ElapsedMilliseconds);

            time.Restart();
            CreateRegions();
            time.Stop();
            Console.WriteLine("Create Regions={0}", time.ElapsedMilliseconds);

            selectedObjectsLeaf = new MyCommand(DoSelectedObjectsList);
            exitCommand = new MyCommand(DoExitCommand);
           
            //!BUG NOT COMMENT!
            CollectionViewSource.GetDefaultView(MotorsLTE).Filter = (x) => { return false; };
        }

        private void DoSelectedObjectsList(object obj)
        {
            SelectedObject = obj as objects;
            if (SelectedObject != null)
            {
                (CollectionViewSource.GetDefaultView(MotorsLTE)).Filter = (x) =>
                {
                    motors_lte i = x as motors_lte;
                    if (i != null && i.idObject == SelectedObject.idObject)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                };
            }
            else
            {
                CollectionViewSource.GetDefaultView(MotorsLTE).Filter = (x) => { return false; };
            }
        }

        private void DoExitCommand(object obj)
        {
            context.Dispose();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void CreateRegions()
        {
            //Header
            //Other class (ViewModel)
            //Ohter struct from table objects + sort in View
            var regions = from x in Objects
                      orderby x.region
                      group x by x.region into g
                      select new Region { Name = g.Key };
            Regions = new ObservableCollection<Region>(regions);
            foreach (Region region in Regions)
            {
                var types = (from y in Objects
                             where y.region == region.Name
                             group y by y.type into g
                             select new Type { Name = g.Key }).OrderBy(x => Array.IndexOf(array, x.Name));
                region.Types = new ObservableCollection<Type>(types);
                foreach (Type type in region.Types)
                {
                    var list = from z in Objects
                               where z.region == region.Name && z.type == type.Name
                               orderby z.address
                               select z;
                    type.Objects = new ObservableCollection<objects>(list);
                }
            }
        }
        
        public class Type
        {
            public string Name { get; set; }
            public ObservableCollection<objects> Objects { get; set; }
        }

        public class Region
        {
            public string Name { get; set; }
            public ObservableCollection<Type> Types { get; set; }
        }

        string[] array = new string[] { "котельня", "ЦТП", "ІТП", "майстерня", "ТК", "склад", "гуртожиток" };
    }
}