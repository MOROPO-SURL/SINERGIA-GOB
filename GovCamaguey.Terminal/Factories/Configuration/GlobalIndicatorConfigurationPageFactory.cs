using Microsoft.Extensions.DependencyInjection;

namespace MOROPO.SINETGIAGOB.GovCamaguey.Terminal.Factories.Configuration;

public class GlobalIndicatorConfigurationPageFactory : Core.Presentation.Terminal.MAUI.UI.Factories.Facade.IConfigurationContentPageFactoryFacade<Interop.API.Common.Models.Display.GlobalIndicatorDisplayModel, Models.Page.Configuration.GlobalIndicatorConfigurationPageModel>
{
    public async System.Threading.Tasks.Task<Core.Presentation.Terminal.MAUI.UI.Pages.Base.ConfigurationBaseContentPage<Interop.API.Common.Models.Display.GlobalIndicatorDisplayModel, Interop.API.Common.Models.Display.GlobalIndicatorDisplayModel, Models.Page.Configuration.GlobalIndicatorConfigurationPageModel>> BuildAsync(System.Action<Interop.API.Common.Models.Display.GlobalIndicatorDisplayModel> configurationSuccessAction, Interop.API.Common.Models.Display.GlobalIndicatorDisplayModel model)
    {
        var _model = new Models.Page.Configuration.GlobalIndicatorConfigurationPageModel(model);

        var apiConnectClient = new Interop.API.Connect.Clients.UnitMeasurementsAPIConnectClient(Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Framework.Guama.Core.Presentation.API.Connect.Providers.Facade.IHttpClientHelperProviderFacade>());

        _model.AvailableUnitMeasurements = await apiConnectClient.GetEnabledAsync();

        return new UI.Pages.Configuration.GlobalIndicatorConfigurationPage(configurationSuccessAction, _model, new Validators.GlobalIndicatorConfigurationValidator()) ;
    }
}
