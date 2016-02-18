using System;
using System.Globalization;
using Cirrious.CrossCore.Converters;
using CoreGraphics;
using SoftTelekom.Core.Enums;
using UIKit;

namespace SoftTelekom.iOS.Converters
{
    public class MenuBgImageValueConverter : MvxValueConverter<ThemeEnum, CGImage>
    {
        protected override CGImage Convert(ThemeEnum value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == ThemeEnum.Magenta ? UIImage.FromBundle("SoftTelekomResources/Images/MenuBg").CGImage : UIImage.FromBundle("SoftTelekomResources/Images/MenuBlueBg").CGImage;
        }
    }
}