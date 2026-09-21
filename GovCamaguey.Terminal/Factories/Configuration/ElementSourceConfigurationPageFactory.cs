namespace MOROPO.SINERGIAGOB.GovCamaguey.Terminal.Factories.Configuration;

public class ElementSourceConfigurationPageFactory : Core.Presentation.Terminal.MAUI.UI.Factories.Facade.IConfigurationContentPageFactoryFacade<Interop.API.Common.Models.Display.ElementSourceDisplayModel, Models.Page.Configuration.ElementSourceConfigurationPageModel>
{
    public async System.Threading.Tasks.Task<Core.Presentation.Terminal.MAUI.UI.Pages.Base.ConfigurationBaseContentPage<Interop.API.Common.Models.Display.ElementSourceDisplayModel, Interop.API.Common.Models.Display.ElementSourceDisplayModel, Models.Page.Configuration.ElementSourceConfigurationPageModel>> BuildAsync(System.Action<Interop.API.Common.Models.Display.ElementSourceDisplayModel> configurationSuccessAction, Interop.API.Common.Models.Display.ElementSourceDisplayModel model)
    {
        return new UI.Pages.Configuration.ElementSourceConfigurationPage(configurationSuccessAction, new Models.Page.Configuration.ElementSourceConfigurationPageModel(model));
    }
}