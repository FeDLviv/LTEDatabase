using System;

using System.Windows.Data;

namespace LTEDatabase.View.Converter
{
    class SpeedConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                int temp = (int) value;
                if (temp > 0 && temp <= 800)
                {
                    return 750;
                }
                else if (temp > 800 && temp <= 1100)
                {
                    return 1000;
                }
                else if (temp > 1100 && temp <= 2000)
                {
                    return 1500;
                }
                else if (temp > 2000 && temp <= 3100)
                {
                    return 3000;
                }
                else 
                {
                    return temp;
                }
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}