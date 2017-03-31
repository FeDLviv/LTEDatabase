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
        private const string QUERY_MOTOR = "SELECT motors_lte.series AS Name, COUNT(motors_lte.idMotorsLTE) AS Value FROM motors_lte JOIN objects USING(idObject) WHERE objects.type NOT IN ('склад', 'ТЕЦ', 'ТЦ') GROUP BY Name ORDER BY Value DESC;";
        private const string QUERY_METER = "SELECT meters.type AS Name, COUNT(meters_lte.idMLTE) AS Value FROM meters RIGHT JOIN meters_lte USING (idMeter) JOIN objects USING(idObject) WHERE objects.type NOT IN ('склад', 'ТЕЦ', 'ТЦ') GROUP BY Name ORDER BY Value DESC;";
        private const string QUERY_TC = "SELECT CONCAT_WS(' ', tc.type, tc.coefficient) AS Name, COUNT(tc_lte.idTCLTE) AS Value FROM tc RIGHT JOIN tc_lte USING (idTC) JOIN objects USING(idObject) WHERE objects.type NOT IN ('склад', 'ТЕЦ', 'ТЦ') GROUP BY Name ORDER BY Value DESC;";
        private const string QUERY_CONTRACT = "SELECT IFNULL(organization, 'без договору') AS Name, COUNT(*) AS Value FROM objects WHERE type NOT IN ('склад', 'ТЕЦ', 'ТЦ') GROUP BY Name ORDER BY organization IS NULL, Value DESC;";
        private const string QUERY_SUBABONENT = "SELECT subabonents.name AS Name, COUNT(subabonents_lte.idSubabonentsLTE) AS Value FROM subabonents RIGHT JOIN subabonents_lte USING(idSubabonents) GROUP BY Name ORDER BY Value DESC;";
        private const string QUERY_WETPUMP = "SELECT motors_lte.series AS Name, COUNT(motors_lte.idMotorsLTE) AS Value FROM motors_lte JOIN objects USING(idObject) WHERE objects.type NOT IN ('склад', 'ТЕЦ', 'ТЦ') AND motors_lte.type REGEXP '^DAB VA +|^DAB A +|^IMPPUMPS GHN +|^LFP LESZNO [0-9]{2}P+|^Lowara EV +|^Lowara TLC +|^Lowara TCR +|^Lowara TC +|^NOCCHI R2C +|Sprut GPD +|^Viessmann +|^Grundfos MAGNA +|^Grundfos UP +|^Grundfos UPBASIC +|^Grundfos UPS +|^Grundfos UPER +|^Wilo RS +|^Wilo Star+|^Wilo TOP-+' GROUP BY Name ORDER BY Value DESC";
        private const string QUERY_OBJECTREGION = "SELECT region AS Name, COUNT(*) AS Value FROM objects WHERE type NOT IN ('склад', 'ТЕЦ', 'ТЦ') GROUP BY Name ORDER BY Value DESC;";
        private const string QUERY_OBJECTCATEGORY = "SELECT	IFNULL(category, 'невідомо') AS Name, COUNT(*) AS Value FROM objects WHERE type NOT IN ('склад', 'ТЕЦ', 'ТЦ') GROUP BY Name ORDER BY Value DESC;";
        private const string QUERY_OBJECTTYPE = "SELECT	type AS Name, COUNT(*) AS Value FROM objects WHERE type NOT IN ('склад', 'ТЕЦ', 'ТЦ') GROUP BY Name ORDER BY Value DESC";
        private const string TITLE_MOTOR = "Двигуни на об'єктах ЛМКП \"Львівтеплоенерго\"";
        private const string TITLE_METER = "Лічильники на об'єктах ЛМКП \"Львівтеплоенерго\"";
        private const string TITLE_TC = "Трансформатори струму на об'єктах ЛМКП \"Львівтеплоенерго\"";
        private const string TITLE_CONTRACT = "Договори на електропостачання об'єктів ЛМКП \"Львівтеплоенерго\"";
        private const string TITLE_SUBABONENT = "Субабоненти на об'єктах ЛМКП \"Львівтеплоенерго\"";
        private const string TITLE_WETPUMP = "Двигуни з мокрим ротором на об'єктах ЛМКП \"Львівтеплоенерго\"";
        private const string TITLE_OBJECTREGION = "Об'єкти ЛМКП \"Львівтеплоенерго\" розділені по районах";
        private const string TITLE_OBJECTCATEGORY = "Об'єкти ЛМКП \"Львівтеплоенерго\" розділені по категоріях надійності електропостачання";
        private const string TITLE_OBJECTTYPE = "Об'єкти ЛМКП \"Львівтеплоенерго\" розділені по типах";

        private string query;
        private List<ChartData> list = new List<ChartData>();
        private int previousFilterValue;
        
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
        public ICommand PieChartMeterCommand { get; set; }
        public ICommand PieChartTCCommand { get; set; }
        public ICommand PieChartContractCommand { get; set; }
        public ICommand PieChartSubabonentCommand { get; set; }
        public ICommand PieChartWetPumpCommand { get; set; }
        public ICommand PieChartObjectRegionCommand { get; set; }
        public ICommand PieChartObjectCategoryCommand { get; set; }
        public ICommand PieChartObjectTypeCommand { get; set; }
        public ICommand FilterCommand { get; set; }
        public ICommand ChartHiddenCommand { get; set; }

        public ChartViewModel()
        {
            //PROGRESSBAR
            
            //ЗНИКНЕННЯ ToolTip при розкритті/закритті діаграми
            //DATALABELS ПРОБЛЕМА СУМІСНОСТІ З TOOLTIP
            //EXCEPTION SQL
            //ПРОКРУТКА КРУГОВОЇ ДІАГРАМИ
            
            //ГІСТОГРАММА (Basic Column)
            
            PieChartMotorCommand = new BaseCommand(DoPieChartMotorCommand);
            PieChartMeterCommand = new BaseCommand(DoPieChartMeterCommand);
            PieChartTCCommand = new BaseCommand(DoPieChartTCCommand);
            PieChartContractCommand = new BaseCommand(DoPieChartContractCommand);
            PieChartSubabonentCommand = new BaseCommand(DoPieChartSubabonentCommand);
            PieChartWetPumpCommand = new BaseCommand(DoPieChartWetPumpCommand);
            PieChartObjectRegionCommand = new BaseCommand(DoPieChartObjectRegionCommand);
            PieChartObjectCategoryCommand = new BaseCommand(DoPieChartObjectCategoryCommand);
            PieChartObjectTypeCommand = new BaseCommand(DoPieChartObjectTypeCommand);
            FilterCommand = new BaseCommand(DoFilterCommand);
            ChartHiddenCommand = new BaseCommand(DoChartHiddenCommand);
        }

        private void DoPieChartMotorCommand(object obj)
        {
            TitleChart = TITLE_MOTOR;
            query = QUERY_MOTOR;
            LoadData();
        }

        private void DoPieChartMeterCommand(object obj)
        {
            TitleChart = TITLE_METER;
            query = QUERY_METER;
            LoadData();
        }

        private void DoPieChartTCCommand(object obj)
        {
            TitleChart = TITLE_TC;
            query = QUERY_TC;
            LoadData();
        }

        private void DoPieChartContractCommand(object obj)
        {
            TitleChart = TITLE_CONTRACT;
            query = QUERY_CONTRACT;
            LoadData();
        }

        private void DoPieChartSubabonentCommand(object obj)
        {
            TitleChart = TITLE_SUBABONENT;
            query = QUERY_SUBABONENT;
            LoadData();
        }

        private void DoPieChartWetPumpCommand(object obj)
        {
            TitleChart = TITLE_WETPUMP;
            query = QUERY_WETPUMP;
            LoadData();
        }

        private void DoPieChartObjectRegionCommand(object obj)
        {
            TitleChart = TITLE_OBJECTREGION;
            query = QUERY_OBJECTREGION;
            LoadData();
        }

        private void DoPieChartObjectCategoryCommand(object obj)
        {
            TitleChart = TITLE_OBJECTCATEGORY;
            query = QUERY_OBJECTCATEGORY;
            LoadData();
        }

        private void DoPieChartObjectTypeCommand(object obj)
        {
            TitleChart = TITLE_OBJECTTYPE;
            query = QUERY_OBJECTTYPE;
            LoadData();
        }

        private void DoFilterCommand(object obj)
        {
            if (FilterValue != previousFilterValue)
            {
                previousFilterValue = FilterValue;
                Data.Clear();
                int count = 0;
                foreach (ChartData x in list)
                {
                    if (x.Value >= FilterValue)
                    {
                        Data.Add(new PieSeries() { Title = x.Name, Values = new ChartValues<int> { x.Value } });
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
        }

        private void DoChartHiddenCommand(object obj)
        {
            ShowChart = false;
        }

        private void LoadData()
        {
            list.Clear();
            list = Database.GetContext().Database.SqlQuery<ChartData>(query).ToList();
            previousFilterValue = 0;
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
    }

    public class ChartData
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }
}