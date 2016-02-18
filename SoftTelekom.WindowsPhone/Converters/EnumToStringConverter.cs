using System;
using System.Windows.Data;
using Cirrious.MvvmCross.Localization;
using SoftTelekom.Core.Utils;

namespace SoftTelekom.WindowsPhone.Converters
{
    public class EnumToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var textSource = new MvxLanguageBinder(Constants.GeneralNamespace, Constants.Shared);
            return textSource.GetText(value.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}