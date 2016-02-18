using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Cirrious.CrossCore.Converters;
using Cirrious.MvvmCross.Localization;
using SoftTelekom.Core.Utils;

namespace SoftTelekom.Android.Converters
{
    public class BoolToSharedValueConverter : MvxValueConverter<bool, string>

    {
        protected override string Convert(bool value, Type targetType, object parameter, CultureInfo culture)
        {
            var textSource = new MvxLanguageBinder(Constants.GeneralNamespace, Constants.Shared);
            if (textSource != null)
            {
                return textSource.GetText(value ? "PaidText" : "NoPaidText");
            }
            return value.ToString();
        }
    }
}