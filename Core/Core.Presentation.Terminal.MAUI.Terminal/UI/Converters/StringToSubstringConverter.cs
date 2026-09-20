namespace MOROPO.SINERGIAGOB.Core.Presentation.Terminal.MAUI.UI.Converters;

public class StringToSubstringConverter : Microsoft.Maui.Controls.IValueConverter
{
    public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        if (value is not string || parameter is not int[] || ((int[])parameter).Length != 2) return value;

        var concreteParameter = (int[])parameter;

        return value.ToString().Substring(concreteParameter[0], concreteParameter[1]);
    }

    public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        return null;
    }
}
