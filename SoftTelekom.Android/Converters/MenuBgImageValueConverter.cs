using System;
using System.Globalization;
using Cirrious.CrossCore.Converters;
using SoftTelekom.Core.Enums;

namespace SoftTelekom.Android.Converters
{
	public class MenuBgImageValueConverter : MvxValueConverter<ThemeEnum, string>
	{
		protected override string Convert(ThemeEnum value, Type targetType, object parameter, CultureInfo culture)
		{
			//var sss = Resource.Drawable.MenuBg;
			return value == ThemeEnum.Magenta ? "@drawable/MenuBg" : "@drawable/MenuBlueBg";
		}
	}
}
