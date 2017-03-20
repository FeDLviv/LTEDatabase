using System;

using System.Windows.Data;

namespace LTEDatabase.View.Converter
{
    class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                DateTime temp = (DateTime) value;
                if (temp != null)
                {
                    return temp.ToString(parameter == null ? "dd-MM-yyyy" : parameter.ToString());
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
