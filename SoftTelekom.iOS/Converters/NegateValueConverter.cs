using Cirrious.CrossCore.Converters;

namespace SoftTelekom.iOS.Converters
{
    public class NegateValueConverter : MvxValueConverter<bool, bool>
    {
        protected override bool Convert(bool value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return !value;
        }
    }
}