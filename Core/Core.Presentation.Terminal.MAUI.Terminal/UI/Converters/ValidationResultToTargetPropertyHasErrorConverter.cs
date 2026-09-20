using System.Linq;

namespace MOROPO.SINERGIAGOB.Core.Presentation.Terminal.MAUI.UI.Converters
{
    public class ValidationResultToTargetPropertyHasErrorConverter : Microsoft.Maui.Controls.IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is not FluentValidation.Results.ValidationResult concreteValue || parameter == null || concreteValue.IsValid) return false;

            return concreteValue.Errors.Any(item => item.PropertyName.Split(".").LastOrDefault().Equals(parameter));
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
