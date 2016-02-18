using System;
using System.Globalization;
using Cirrious.CrossCore.Converters;

namespace SoftTelekom.iOS.Converters
{
    public class BoolToStringValueConverter : MvxValueConverter<bool,string>
    {
        protected override string Convert(bool value, Type targetType, object parameter, CultureInfo culture)
        {
            var type = (string)parameter;

            switch (type)
            {
                case "bill":
                    return value ? "Fizetve" : "Nincs fizetve";
                default:
                    return "";
            }

            
        }
    }
}