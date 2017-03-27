using LTEDatabase.ViewModel;
using LTEDatabase.Command;

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
        private const int LIMIT = 8;
        private IQueryable<ChartData> query;
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

        private bool showLabel = false;
        public bool ShowLabel 
        {
            get { return showLabel; }
            set
            {
                if (showLabel != value)
                {
                    showLabel = value;
                    OnPropertyChanged("ShowLabel");
                }
            }
        }

        private Func<ChartPoint, string> label = (chartPoint) => { return string.Format("({0} з {1} )", chartPoint.Y, chartPoint.Sum); };
        public Func<ChartPoint, string> Label
        {
            get { return label; }
            set
            {
                if (label != value)
                {
                    label = value;
                    OnPropertyChanged("Label");
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
            //ФІЛЬТРАЦІЯ, ЯКЩО ЗМІНЕНО  ЗНАЧЕННЯ В SLIDER (Thumb.DragCompleted)
            
            //ВІКНО ПОТРІБНО "ОБРІЗАТИ" (НОРМАЛЬНІ РОЗМІРИ), АКТИВАЦІЯ, ЯКЩО БУВ ПЕРЕХІД В ІНШУ ПРОГРАММУ
            //PROGRESSBAR
            
            //ЗНИКНЕННЯ ToolTip при розкритті/закритті діаграми
            //DATALABELS ПРОБЛЕМА СУМІСНОСТІ З TOOLTIP
            //ЗАПИТИ - SQL, а не - LINQ
            //EXCEPTION SQL
            
            //ГІСТОГРАММА (Basic Column)
            
            PieChartMotorCommand = new BaseCommand(DoPieChartMotorCommand);
            FilterCommand = new BaseCommand(DoFilterCommand);
            ChartHiddenCommand = new BaseCommand(DoChartHiddenCommand);
        }

        private void DoPieChartMotorCommand(object obj)
        {
            TitleChart = "Двигуни на об'єктах ЛМКП \"Львівтеплоенерго\"";
            query = from x in Database.GetContext().motors_lte.AsNoTracking()
                    group x by x.series into g
                    let count = g.Count()
                    orderby count descending
                    select new ChartData { Key = g.Key, Value = g.Count() };
            LoadData();
        }

        private void LoadData()
        {
            list.Clear();
            list = query.ToList();
            if (list.Count > LIMIT)
            {
                MaxFilter = list[LIMIT - 2].Value;
                FilterValue = MaxFilter;
            }
            else
            {
                MaxFilter = 0;
                FilterValue = 1;
            }
            DoFilterCommand(null);
            ShowChart = true;
        }

        private void DoFilterCommand(object obj)
        {
            Data.Clear();
            int count = 0;
            foreach(ChartData x in list)
            {
                if (x.Value >= FilterValue)
                {
                    Data.Add(new PieSeries() { Title = x.Key, Values = new ChartValues<int> { x.Value } });
                }
                else
                {
                    count += x.Value;
                }
            }
            if (count > 0)
            {
                Data.Add(new PieSeries() { Title = "решта", Values = new ChartValues<int> { count }, PushOut = 15 }); 
            }
            ShowLabel = Data.Count <= LIMIT; 
        }

        private void DoChartHiddenCommand(object obj)
        {
            ShowChart = false;
        }
    }

    public class ChartData
    {
        public string Key { get; set; }
        public int Value { get; set; }
    }
}