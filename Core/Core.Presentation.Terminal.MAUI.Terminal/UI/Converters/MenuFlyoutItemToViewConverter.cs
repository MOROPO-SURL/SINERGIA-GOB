using Microsoft.Extensions.DependencyInjection;

namespace MOROPO.SINETGIAGOB.Core.Presentation.Terminal.MAUI.UI.Converters
{
    public class MenuFlyoutItemToViewConverter : Microsoft.Maui.Controls.IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;

            var concreteValue = value as Microsoft.Maui.Controls.ShellItem;

            var provider = Microsoft.Maui.IPlatformApplication.Current.Services.GetService<Helpers.Facade.IMenuFlyoutSectionSubtitleHelperFacade>();

            if (provider != null && provider.MostPresentedAsSubtitle(concreteValue)) return new Views.ModuleFlyoutSectionSubtitleContentView();
            
            return new Views.ModuleFlyoutSectionTitleContentView();
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
