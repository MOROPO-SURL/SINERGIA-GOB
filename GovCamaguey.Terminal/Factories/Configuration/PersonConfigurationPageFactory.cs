namespace MOROPO.SINETGIAGOB.GovCamaguey.Terminal.Factories.Configuration;

public class PersonConfigurationPageFactory : Core.Presentation.Terminal.MAUI.UI.Factories.Facade.IConfigurationContentPageFactoryFacade<Interop.API.Common.Models.Display.PersonDisplayModel, Models.Page.Configuration.PersonConfigurationPageModel>
{
    public async System.Threading.Tasks.Task<Core.Presentation.Terminal.MAUI.UI.Pages.Base.ConfigurationBaseContentPage<Interop.API.Common.Models.Display.PersonDisplayModel, Interop.API.Common.Models.Display.PersonDisplayModel, Models.Page.Configuration.PersonConfigurationPageModel>> BuildAsync(System.Action<Interop.API.Common.Models.Display.PersonDisplayModel> configurationSuccessAction, Interop.API.Common.Models.Display.PersonDisplayModel model)
    {
        return new UI.Pages.Configuration.PersonConfigurationPage(configurationSuccessAction, new Models.Page.Configuration.PersonConfigurationPageModel(model));
    }
}