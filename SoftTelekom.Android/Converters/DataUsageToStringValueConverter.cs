using System;
using System.Globalization;
using Cirrious.CrossCore.Converters;

namespace SoftTelekom.Android.Converters
{
	public class DataUsageToStringValueConverter : MvxValueConverter<int, string>
	{
		protected override string Convert(int value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value < 1024) return value.ToString() + " MB";
			return (value / 1024.0).ToString("F1") + " GB";


		}
	}
}
