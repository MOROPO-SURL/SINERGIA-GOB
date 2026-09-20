namespace MOROPO.SINERGIAGOB.GovCamaguey.Interop.API.Connect.Clients
{
    public class GlobalIndicatorsAPIConnectClient : Framework.Guama.Core.Presentation.API.Connect.Clients.Contract.AbstractArchivableEnableableEntityManagementFilteredAPIConnectClient<GovCamaguey.Persistence.Entities.GlobalIndicator, System.Guid, Common.Models.Input.GlobalIndicatorInputModel, Common.Models.Display.GlobalIndicatorDisplayModel, Common.Models.Filter.GlobalIndicatorFilterModel>
    {
        public GlobalIndicatorsAPIConnectClient(Framework.Guama.Core.Presentation.API.Connect.Providers.Facade.IHttpClientHelperProviderFacade httpClientHelperProvider) : base(httpClientHelperProvider)
        {
        }

        protected override string TargetController => "api/governance/globalindicators";
    }
}