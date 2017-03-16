using LTEDatabase.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections.ObjectModel;

namespace LTEDatabase.ViewModel
{
    class ObjectsViewModel : BaseViewModel
    {
        private string[] typeOfObjects = new string[] { "котельня", "ЦТП", "ІТП", "майстерня", "ТК", "склад", "гуртожиток" };
        private static MyCompare compare = new MyCompare();

        private ObservableCollection<Region> regions;
        public ObservableCollection<Region> Regions 
        {
            get { return regions; }
            set
            {
                if (regions != value)
                {
                    regions = value;
                    OnPropertyChanged("Regions");
                }
            }
        }

        public ObjectsViewModel()
        {
            CreateRegions();
        }

        private void CreateRegions()
        {
            //Header
            //Other struct + sort in View
            List<objects> temp = Database.GetContext().objects.AsNoTracking().ToList();
            var regions = from x in temp
                          orderby x.region
                          group x by x.region into g
                          select new Region { Name = g.Key };
            Regions = new ObservableCollection<Region>(regions);
            foreach (Region region in Regions)
            {
                var types = (from y in temp
                             where y.region == region.Name
                             group y by y.type into g
                             select new Type { Name = g.Key }).OrderBy(x => Array.IndexOf(typeOfObjects, x.Name));
                region.Types = new ObservableCollection<Type>(types);
                foreach (Type type in region.Types)
                {
                    var list = (from z in temp
                                where z.region == region.Name && z.type == type.Name
                                //orderby z.address
                                select z).OrderBy((z => z), compare);
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

        public class MyCompare : IComparer<objects>
        {
            public int Compare(objects x, objects y)
            {
                int point1 = x.address.IndexOf(',');
                int point2 = y.address.IndexOf(',');

                if (point1 != -1 && point2 != -1)
                {
                    string street1 = x.address.Substring(0, point1);
                    string street2 = y.address.Substring(0, point2);

                    string other1 = x.address.Substring(point1 + 2, x.address.Length - (point1 + 2));
                    string other2 = y.address.Substring(point2 + 2, y.address.Length - (point2 + 2));
                    
                    int number1;
                    int number2;

                    if (Int32.TryParse(other1, out number1) && Int32.TryParse(other2, out number2))
                    {
                        if (street1.CompareTo(street2) == 0)
                        {
                            if (number1 > number2)
                            {
                                return 1;
                            }
                            else if (number1 < number2)
                            {
                                return -1;
                            }
                        }
                        return x.address.CompareTo(y.address);
                    }
                    else
                    { 
                        //ДОПИСАТИ
                    }
                    //ДОПИСАТИ
                    return x.address.CompareTo(y.address);
                }
                else
                {
                    return x.address.CompareTo(y.address);
                }
            }
        }
    }
}