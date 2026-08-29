namespace MOROPO.SINETGIAGOB.GovCamaguey.Interop.API.Connect.Clients
{
    public class GuidelinesAPIConnectClient : Framework.Guama.Core.Presentation.API.Connect.Clients.Contract.AbstractArchivableEnableableEntityManagementAPIConnectClient<GovCamaguey.Persistence.Entities.Guideline, System.Guid, Common.Models.Input.GuidelineInputModel, Common.Models.Display.GuidelineDisplayModel>
    {
        public GuidelinesAPIConnectClient(Framework.Guama.Core.Presentation.API.Connect.Providers.Facade.IHttpClientHelperProviderFacade httpClientHelperProvider) : base(httpClientHelperProvider)
        {
        }

        protected override string TargetController => "api/governance/guidelines";
    }
}