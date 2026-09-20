namespace MOROPO.SINERGIAGOB.Core.Presentation.Terminal.MAUI.UI.Converters
{
    public class DocumentDisplayModelToMaterialDesignFontIconTextConverter : Microsoft.Maui.Controls.IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || value is not Interop.API.Common.Models.Display.DocumentDisplayModel) return null;

            var target = value as Interop.API.Common.Models.Display.DocumentDisplayModel;

            return target.Extension switch
            {
                ".xml" => "\uF22E",
                ".txt" => "\uF219",
                ".xls" or ".xlsx" => "\uF21B",
                ".png" or ".bmp" or ".jpg" or ".gif" => "\uF21F",
                ".pdf" => "\uF225",
                ".ppt" or ".pptx" => "\uF227",
                ".doc" or ".docx" => "\uF22C",
                _ => "\uF214",
            };
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
