using MOROPO.SINETGIAGOB.Core.Presentation.Terminal.MAUI.Extensions;

namespace MOROPO.SINETGIAGOB.Core.Presentation.Terminal.MAUI.UI.Converters
{
    public class ImageInputModelToImageSourceConverter : Microsoft.Maui.Controls.IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;

            var model = value as Interop.API.Common.Models.Input.ImageInputModel;

            return model.ToImageSource();
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
