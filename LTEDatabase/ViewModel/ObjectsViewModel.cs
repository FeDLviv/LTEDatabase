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
            //Ohter struct from table objects + sort in View
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
                    var list = from z in temp
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
    }
}