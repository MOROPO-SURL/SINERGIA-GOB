namespace MOROPO.SINERGIAGOB.GovCamaguey.Interop.API.Connect.Clients
{
    public class OrganizationsAPIConnectClient : Framework.Guama.Core.Presentation.API.Connect.Clients.Contract.AbstractArchivableEnableableEntityManagementFilteredAPIConnectClient<GovCamaguey.Persistence.Entities.Organization, System.Guid, Common.Models.Input.OrganizationInputModel, Common.Models.Display.OrganizationDisplayModel, Common.Models.Filter.OrganizationFilterModel>
    {
        public OrganizationsAPIConnectClient(Framework.Guama.Core.Presentation.API.Connect.Providers.Facade.IHttpClientHelperProviderFacade httpClientHelperProvider) : base(httpClientHelperProvider)
        {
        }

        protected override string TargetController => "api/governance/organizations";
    }
}
