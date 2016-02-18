using System;
using System.Globalization;
using System.Windows.Data;
using Cirrious.CrossCore.Converters;

namespace SoftTelekom.WindowsPhone.Converters
{
    public class BoolToColorValueConverter : IValueConverter
    {
        //protected override UIColor Convert(bool value, Type targetType, object parameter, CultureInfo culture)
        //{
        //    var type = (string)parameter;

        //    switch (type)
        //    {
        //        case "bill":
        //            return value ? UIColor.Green : UIColor.Red;
        //        default:
        //            return UIColor.Black;
        //    }


        //}

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}