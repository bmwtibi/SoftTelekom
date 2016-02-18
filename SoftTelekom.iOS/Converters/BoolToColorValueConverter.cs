using System;
using System.Globalization;
using Cirrious.CrossCore.Converters;
using UIKit;

namespace SoftTelekom.iOS.Converters
{
    public class BoolToColorValueConverter : MvxValueConverter<bool, UIColor>
    {
        protected override UIColor Convert(bool value, Type targetType, object parameter, CultureInfo culture)
        {
            var type = (string)parameter;

            switch (type)
            {
                case "bill":
                    return value ? UIColor.Green : UIColor.Red;
                default:
                    return UIColor.Black;
            }


        }
    }
}