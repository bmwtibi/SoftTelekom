using System;
using System.Globalization;
using Cirrious.CrossCore.Converters;
using SoftTelekom.Core.Enums;
using UIKit;

namespace SoftTelekom.iOS.Converters
{
    public class ThemeToColorValueConverter : MvxValueConverter<ThemeEnum,UIColor>
    {
        protected override UIColor Convert(ThemeEnum value, Type targetType, object parameter, CultureInfo culture)
        {
            var type = (string)parameter;
            if (type!=null)
            {
                switch (type)
                {
                    case "news" :
                        return value == ThemeEnum.Magenta ? UIColor.FromRGB(177, 1, 156) : UIColor.FromRGB(62, 98, 209);
                }
            }
            return UIColor.Black;
        }
    }
}