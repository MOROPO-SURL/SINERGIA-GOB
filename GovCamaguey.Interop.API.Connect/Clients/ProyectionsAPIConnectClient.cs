namespace MOROPO.SINETGIAGOB.GovCamaguey.Interop.API.Connect.Clients
{
    public class ProyectionsAPIConnectClient : Framework.Guama.Core.Presentation.API.Connect.Clients.Contract.AbstractArchivableEntityManagementAPIConnectClient<GovCamaguey.Persistence.Entities.Proyection, System.Guid, Common.Models.Input.ProyectionInputModel, Common.Models.Display.ProyectionDisplayModel>
    {
        public ProyectionsAPIConnectClient(Framework.Guama.Core.Presentation.API.Connect.Providers.Facade.IHttpClientHelperProviderFacade httpClientHelperProvider) : base(httpClientHelperProvider)
        {
        }

        protected override string TargetController => "api/governance/proyections";
    }
}