using LTEDatabase.Command;
using LTEDatabase.ViewModel;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Input;
using LiveCharts;
using LiveCharts.Wpf;

namespace LTEDatabase.ViewModel
{
    class ChartViewModel : BaseViewModel
    {
        private const int COUNT = 11;
        private List<ChartData> list = new List<ChartData>();
        
        private bool showChart = false;
        public bool ShowChart 
        {
            get { return showChart; }
            set
            {
                if (showChart != value)
                {
                    showChart = value;
                    OnPropertyChanged("ShowChart");
                }
            }
        }

        private bool isMore = true;
        public bool IsMore 
        {
            get { return isMore; }
            set
            {
                if (isMore != value)
                {
                    isMore = value;
                    OnPropertyChanged("IsMore");
                }
            }
        }

        private int maxFilter = 0;
        public int MaxFilter 
        {
            get { return maxFilter; }
            set
            {
                if (maxFilter != value)
                {
                    maxFilter = value;
                    OnPropertyChanged("MaxFilter");
                }
            }
        }

        private int filterValue = 1;
        public int FilterValue 
        {
            get { return filterValue; }
            set
            {
                if (filterValue != value)
                {
                    filterValue = value;
                    OnPropertyChanged("FilterValue");
                }
            }
        }
        
        private string titleChart = string.Empty;
        public string TitleChart 
        {
            get { return titleChart; }
            set
            {
                if (titleChart != value)
                {
                    titleChart = value;
                    OnPropertyChanged("TitleChart");
                }
            }
        }

        private SeriesCollection data = new SeriesCollection();
        public SeriesCollection Data
        {
            get { return data; }
            set
            {
                if (data != value)
                {
                    data = value;
                    OnPropertyChanged("Data");
                }
            }
        }

        public ICommand PieChartMotorCommand { get; set; }
        public ICommand FilterCommand { get; set; }
        public ICommand ChartHiddenCommand { get; set; }

        public ChartViewModel()
        {
            //ЗАГОЛОВОК ДІАГРАМИ
            //ЗНИКНЕННЯ ToolTip при розкритті/закритті діаграми
            //НАЗВИ ВСІХ ЧАСТИН (DataLabels, LabelPoint(проблема сумісності з ToolTip))

            //public Func<ChartPoint, string> PointLabel { get; set; }
            //PointLabel = (chartPoint) => { return chartPoint.SeriesView.Title; };
            //DataLabels=true

            //ФІЛЬТРАЦІЯ 

            //ВІКНО ПОТРІБНО "ОБРІЗАТИ"

            //ЗАПИТИ - SQL, а не - LINQ
            //Thumb.DragCompleted
            //ЛЕЙБОЧКА НА РЕШТУ ЯКЩО < 10
            //EXCEPTION SQL
            //ГІСТОГРАММА (Basic Column)

            PieChartMotorCommand = new BaseCommand(DoPieChartMotorCommand);
            FilterCommand = new BaseCommand(DoFilterCommand);
            ChartHiddenCommand = new BaseCommand(DoChartHiddenCommand);
        }

        private void DoPieChartMotorCommand(object obj)
        {
            TitleChart = "Двигуни на об'єктах ЛМКП \"Львівтеплоенерго\"";

            list.Clear();
            list = (from x in Database.GetContext().motors_lte.AsNoTracking()
                    group x by x.series into g
                    let count = g.Count()
                    orderby count descending
                    select new ChartData { Key = g.Key, Value = g.Count() }).ToList();

            Data.Clear();

            IsMore = (list.Count > COUNT);
            if (!IsMore)
            {
                foreach (ChartData x in list)
                {
                    Data.Add(new PieSeries() { Title = x.Key, Values = new ChartValues<int> { x.Value }, DataLabels = true });
                }
            }
            else 
            {
                MaxFilter = list[COUNT-2].Value;
                foreach (ChartData x in list)
                {
                    Data.Add(new PieSeries() { Title = x.Key, Values = new ChartValues<int> { x.Value } });
                }
            }           
                        
            ShowChart = true;
        }

        private void DoFilterCommand(object obj)
        {
            Data.Clear();
            int index = list.FindIndex( (x) => 
            {
                if (x.Value < FilterValue)
                {
                    return true;
                }
                else 
                {
                    return false;
                }
            });
            if (index > -1)
            {
                for (int i = 0; i < index; i++)
                {
                    Data.Add(new PieSeries() { Title = list[i].Key, Values = new ChartValues<int> { list[i].Value }, DataLabels = index < COUNT });
                }

                int count = 0;
                for (; index < list.Count; index++)
                {
                    count += list[index].Value;
                }
                Data.Add(new PieSeries() { Title = "решта", Values = new ChartValues<int> { count }, DataLabels = index < COUNT, PushOut = 15 });
            }
            else
            {
                //МАЙЖЕ ДУБЛЯЖ
                if (!IsMore)
                {
                    foreach (ChartData x in list)
                    {
                        Data.Add(new PieSeries() { Title = x.Key, Values = new ChartValues<int> { x.Value }, DataLabels = true });
                    }
                }
                else
                {
                    //MaxFilter = list[COUNT - 2].Value;
                    foreach (ChartData x in list)
                    {
                        Data.Add(new PieSeries() { Title = x.Key, Values = new ChartValues<int> { x.Value } });
                    }
                }      
            }
        }

        private void DoChartHiddenCommand(object obj)
        {
            FilterValue = 1;
            ShowChart = false;
        }
    }

    public class ChartData
    {
        public string Key { get; set; }
        public int Value { get; set; }
    }
}