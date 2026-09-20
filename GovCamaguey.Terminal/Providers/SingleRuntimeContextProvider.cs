namespace MOROPO.SINERGIAGOB.GovCamaguey.Terminal.Providers
{
    public class SingleRuntimeContextProvider : Core.Presentation.Terminal.MAUI.Providers.Facade.IRuntimeContextProviderFacade
    {
        internal static Core.Presentation.Terminal.MAUI.Models.View.OwnerInformationViewModel OWNER_INFORMATION = new Core.Presentation.Terminal.MAUI.Models.View.OwnerInformationViewModel
        {
            Name = "SINERGIA GOB Camagüey",
            LogoImageViewStyle = (Microsoft.Maui.Controls.Style)Microsoft.Maui.Controls.Application.Current.Resources["LogoImageViewStyle"]
        };

        public Core.Presentation.Terminal.MAUI.Models.View.OwnerInformationViewModel OwnerInformation => OWNER_INFORMATION;
    }
}