using LTEDatabase.Model;

using System;
using System.Collections.Generic;
using System.Linq;

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
            //Other struct
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
                if (point1 != -1 || point2 != -1)
                {
                    string street1 = x.address.Substring(0, point1);
                    string street2 = y.address.Substring(0, point2);
                    string other1 = x.address.Substring(point1 + 2, x.address.Length - (point1 + 2));
                    string other2 = y.address.Substring(point2 + 2, y.address.Length - (point2 + 2));               
                    int number1;
                    int number2;
                    if (street1.CompareTo(street2) == 0)
                    {
                        if (Int32.TryParse(other1, out number1) && Int32.TryParse(other2, out number2))
                        {
                            return (number1 - number2);
                        }
                        else
                        {
                            string letter1 = System.Text.RegularExpressions.Regex.Replace(other1, "[0-9]", "");
                            string letter2 = System.Text.RegularExpressions.Regex.Replace(other2, "[0-9]", "");
                            if (Int32.TryParse(other1, out number1) && Int32.TryParse(other2.Substring(0, other2.Length - 1), out number2))
                            {
                                return (number1 - number2);
                            }
                            else if (Int32.TryParse(other2, out number2) && Int32.TryParse(other1.Substring(0, other1.Length - 1), out number1))
                            {
                                return (number1 - number2);
                            }
                            else if (Int32.TryParse(other1.Substring(0, other1.Length - 1), out number1) && Int32.TryParse(other2.Substring(0, other2.Length - 1), out number2))
                            {
                                return (number1 - number2);
                            }
                        }
                    }
                }
                return x.address.CompareTo(y.address);
            }
        }
    }
}