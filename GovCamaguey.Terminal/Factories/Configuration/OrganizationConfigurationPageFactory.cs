namespace MOROPO.SINETGIAGOB.GovCamaguey.Terminal.Factories.Configuration;

public class OrganizationConfigurationPageFactory : Core.Presentation.Terminal.MAUI.UI.Factories.Facade.IConfigurationContentPageFactoryFacade<Interop.API.Common.Models.Display.OrganizationDisplayModel, Models.Page.Configuration.OrganizationConfigurationPageModel>
{
    public async System.Threading.Tasks.Task<Core.Presentation.Terminal.MAUI.UI.Pages.Base.ConfigurationBaseContentPage<Interop.API.Common.Models.Display.OrganizationDisplayModel, Interop.API.Common.Models.Display.OrganizationDisplayModel, Models.Page.Configuration.OrganizationConfigurationPageModel>> BuildAsync(System.Action<Interop.API.Common.Models.Display.OrganizationDisplayModel> configurationSuccessAction, Interop.API.Common.Models.Display.OrganizationDisplayModel model)
    {
        return new UI.Pages.Configuration.OrganizationConfigurationPage(configurationSuccessAction, new Models.Page.Configuration.OrganizationConfigurationPageModel(model));
    }
}
