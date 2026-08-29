namespace MOROPO.SINETGIAGOB.GovCamaguey.Terminal.UI.Converters
{
    public class MonthToStringConverter : Microsoft.Maui.Controls.IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is not int concreteValue) return null;

            return Interop.API.Common.Constants.Globals.MONTHS[concreteValue - 1];
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
