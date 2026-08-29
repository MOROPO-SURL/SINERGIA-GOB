namespace MOROPO.SINETGIAGOB.GovCamaguey.Interop.API.Connect.Clients
{
    public class ObjectivesAPIConnectClient : Framework.Guama.Core.Presentation.API.Connect.Clients.Contract.AbstractArchivableEnableableEntityManagementAPIConnectClient<GovCamaguey.Persistence.Entities.Objective, System.Guid, Common.Models.Input.ObjectiveInputModel, Common.Models.Display.ObjectiveDisplayModel>
    {
        public ObjectivesAPIConnectClient(Framework.Guama.Core.Presentation.API.Connect.Providers.Facade.IHttpClientHelperProviderFacade httpClientHelperProvider) : base(httpClientHelperProvider)
        {
        }

        protected override string TargetController => "api/governance/objectives";
    }
}