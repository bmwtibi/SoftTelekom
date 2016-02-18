using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System;
using System.Globalization;
using Cirrious.CrossCore.Converters;
using Cirrious.MvvmCross.Localization;
using SoftTelekom.Core.Utils;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace SoftTelekom.Android.Converters
{
    public class EnumToStringValueConverter : MvxValueConverter<Enum, string>
    {
        protected override string Convert(Enum value, Type targetType, object parameter, CultureInfo culture)
        {
            var converter = parameter as IMvxLanguageBinder ?? new MvxLanguageBinder(Constants.GeneralNamespace, Constants.Shared);
            return converter.GetText(value.ToString());
        }
    }
}