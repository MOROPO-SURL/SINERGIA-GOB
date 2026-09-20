namespace MOROPO.SINERGIAGOB.GovCamaguey.Terminal.UI.Converters
{
    public class YearMonthKeyValuePairToStringConverter : Microsoft.Maui.Controls.IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is not System.Collections.Generic.KeyValuePair<int,int> concreteValue) return null;

            return $"{Interop.API.Common.Constants.Globals.MONTHS[concreteValue.Value - 1]} / {concreteValue.Key}";
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
