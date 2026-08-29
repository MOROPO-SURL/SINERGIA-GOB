namespace MOROPO.SINETGIAGOB.GovCamaguey.Interop.API.Connect.Clients
{
    public class UnitMeasurementsAPIConnectClient : Framework.Guama.Core.Presentation.API.Connect.Clients.Contract.AbstractArchivableEnableableEntityManagementAPIConnectClient<GovCamaguey.Persistence.Entities.UnitMeasurement, System.Guid, Common.Models.Input.UnitMeasurementInputModel, Common.Models.Display.UnitMeasurementDisplayModel>
    {
        public UnitMeasurementsAPIConnectClient(Framework.Guama.Core.Presentation.API.Connect.Providers.Facade.IHttpClientHelperProviderFacade httpClientHelperProvider) : base(httpClientHelperProvider)
        {
        }

        protected override string TargetController => "api/governance/unitmeasurements";
    }
}
