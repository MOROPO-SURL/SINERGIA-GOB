namespace MOROPO.SINETGIAGOB.GovCamaguey.Terminal.Factories.Configuration;

public class UnitMeasurementConfigurationPageFactory : Core.Presentation.Terminal.MAUI.UI.Factories.Facade.IConfigurationContentPageFactoryFacade<Interop.API.Common.Models.Display.UnitMeasurementDisplayModel, Models.Page.Configuration.UnitMeasurementConfigurationPageModel>
{
    public async System.Threading.Tasks.Task<Core.Presentation.Terminal.MAUI.UI.Pages.Base.ConfigurationBaseContentPage<Interop.API.Common.Models.Display.UnitMeasurementDisplayModel, Interop.API.Common.Models.Display.UnitMeasurementDisplayModel, Models.Page.Configuration.UnitMeasurementConfigurationPageModel>> BuildAsync(System.Action<Interop.API.Common.Models.Display.UnitMeasurementDisplayModel> configurationSuccessAction, Interop.API.Common.Models.Display.UnitMeasurementDisplayModel model)
    {
        return new UI.Pages.Configuration.UnitMeasurementConfigurationPage(configurationSuccessAction, new Models.Page.Configuration.UnitMeasurementConfigurationPageModel(model));
    }
}