using System;
using System.Globalization;
using System.Windows.Data;
using Cirrious.CrossCore.Converters;

namespace SoftTelekom.WindowsPhone.Converters
{
    public class BoolToStringConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isIt = (bool) value;
            return isIt ? "Fizetve" : "Nincs fizetve";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}