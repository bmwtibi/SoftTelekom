using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Cirrious.CrossCore.Converters;
using CoreGraphics;
using Foundation;
using UIKit;

namespace SoftTelekom.iOS.Converters
{
    public class UiImageValueConverter : MvxValueConverter<string, UIImage>
    {
        protected override UIImage Convert(string value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.Contains("MagentaDark"))
            {
                return
                    UIImage.FromBundle("SoftTelekomResources/Images/MagentaDark").Scale(new CGSize(100,1))
                        .CreateResizableImage(new UIEdgeInsets(0, 0, 0, 0));
            }
            if (value.Contains("MagentaLight"))
            {
                return UIImage.FromBundle("SoftTelekomResources/Images/MagentaLight").Scale(new CGSize(100,1))
                    .CreateResizableImage(new UIEdgeInsets(0, 0, 0, 0));
            }
            if (value.Contains("BlueDark"))
            {
                return
                    UIImage.FromBundle("SoftTelekomResources/Images/BlueDark").Scale(new CGSize(100, 1))
                        .CreateResizableImage(new UIEdgeInsets(0, 0, 0, 0));
            }
            if (value.Contains("BlueLight"))
            {
                return UIImage.FromBundle("SoftTelekomResources/Images/BlueLight").Scale(new CGSize(100, 1))
                    .CreateResizableImage(new UIEdgeInsets(0, 0, 0, 0));
            }
            return UIImage.FromBundle("SoftTelekomResources/Images/" + value);
        }
    }
}