namespace MOROPO.SINETGIAGOB.GovCamaguey.Terminal.Factories.Configuration;

public class ObjectiveConfigurationPageFactory : Core.Presentation.Terminal.MAUI.UI.Factories.Facade.IConfigurationContentPageFactoryFacade<Interop.API.Common.Models.Display.ObjectiveDisplayModel, Models.Page.Configuration.ObjectiveConfigurationPageModel>
{
    public async System.Threading.Tasks.Task<Core.Presentation.Terminal.MAUI.UI.Pages.Base.ConfigurationBaseContentPage<Interop.API.Common.Models.Display.ObjectiveDisplayModel, Interop.API.Common.Models.Display.ObjectiveDisplayModel, Models.Page.Configuration.ObjectiveConfigurationPageModel>> BuildAsync(System.Action<Interop.API.Common.Models.Display.ObjectiveDisplayModel> configurationSuccessAction, Interop.API.Common.Models.Display.ObjectiveDisplayModel model)
    {
        return new UI.Pages.Configuration.ObjectiveConfigurationPage(configurationSuccessAction, new Models.Page.Configuration.ObjectiveConfigurationPageModel(model));
    }
}
