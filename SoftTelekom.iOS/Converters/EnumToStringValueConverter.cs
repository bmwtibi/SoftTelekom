using System;
using SoftTelekom.Core.Utils;
using Cirrious.CrossCore.Converters;
using Cirrious.MvvmCross.Localization;
using System.Globalization;

namespace SoftTelekom.iOS.Converters
{
    class EnumToStringValueConverter : MvxValueConverter<Enum, string>
    {
        protected override string Convert(Enum value, Type targetType, object parameter, CultureInfo culture)
        {
            var textSource = new MvxLanguageBinder(Constants.GeneralNamespace, Constants.Shared);
            if (textSource != null)
            {
                return textSource.GetText(value.ToString());
            }
            return string.Empty;
        }
    }
}