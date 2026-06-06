using System.Globalization;
using System.Windows.Data;

namespace FinanceApp.Converters;

[ValueConversion(typeof(bool), typeof(bool))]
public class InverseBool : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return !(bool)value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        //return DependencyProperty.UnsetValue;
        return Binding.DoNothing;
    }
}
