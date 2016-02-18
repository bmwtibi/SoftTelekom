using System;
using System.Globalization;
using Cirrious.CrossCore.Converters;
using UIKit;

namespace SoftTelekom.iOS.Converters
{
    public class MenuIndexToImageValueConverter : MvxValueConverter<int, UIImage>
    {
        protected override UIImage Convert(int value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter != null)
            {
                var type = (string)parameter;
                if (type == "LeftMenuImage")
                {
                    switch (value)
                    {
                        case 0:
                            return UIImage.FromBundle("SoftTelekomResources/Images/Menu/news");
                            break;
                        case 1:
                            return UIImage.FromBundle("SoftTelekomResources/Images/Menu/order");
                            break;
                        case 2:
                            return UIImage.FromBundle("SoftTelekomResources/Images/Menu/contact");
                            break;
                    }
                }

            }
            return null;
        }
    }
}