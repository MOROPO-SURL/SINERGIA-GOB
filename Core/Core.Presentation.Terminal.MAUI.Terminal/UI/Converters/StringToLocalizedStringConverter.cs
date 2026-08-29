using Microsoft.Extensions.DependencyInjection;

namespace MOROPO.SINETGIAGOB.Core.Presentation.Terminal.MAUI.UI.Converters;

public class StringToLocalizedStringConverter : Microsoft.Maui.Controls.IValueConverter
{
    public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        if (value == null) return null;

        return Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Helpers.Facade.ILocalizationHelperFacade>()?.GetString(value.ToString(), culture) ?? value.ToString();
    }

    public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        return null;
    }
}
