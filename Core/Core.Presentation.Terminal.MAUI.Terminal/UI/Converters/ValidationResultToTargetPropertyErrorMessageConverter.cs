using System.Linq;

namespace MOROPO.SINERGIAGOB.Core.Presentation.Terminal.MAUI.UI.Converters
{
    public class ValidationResultToTargetPropertyErrorMessageConverter : Microsoft.Maui.Controls.IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is not FluentValidation.Results.ValidationResult concreteValue || parameter == null || concreteValue.IsValid) return null;

            return concreteValue.Errors.FirstOrDefault(item => item.PropertyName.Split(".").LastOrDefault().Equals(parameter))?.ErrorMessage;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
