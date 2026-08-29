namespace MOROPO.SINETGIAGOB.Core.Presentation.Terminal.MAUI.UI.Converters
{
    public class IntToMonthNameStringConverter : Microsoft.Maui.Controls.IValueConverter
    {
        private static string[] MONTH_NAMES = ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"];

        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is not int concreteValue) return null;

            return MONTH_NAMES[concreteValue - 1];
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
