using System;
using System.Globalization;
using Cirrious.CrossCore.Converters;
using UIKit;

namespace SoftTelekom.iOS.Converters
{
    public class DateTimeValueConverter :MvxValueConverter<DateTime,string>
    {
        protected override string Convert(DateTime value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter != null)
            {
                var type = (string)parameter;
                if (type == "Date")
                {
                    return value.ToString("yyyy. MM. dd.");
                }
                if (type == "Time")
                {
                    return value.ToString("hh:mm");
                }
                if (type == "Date2")
                {
                    return value.ToString("yyyy. MMMM");
                }

            }
            return null;
        } 
    }
}