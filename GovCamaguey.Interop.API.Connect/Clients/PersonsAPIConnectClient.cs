namespace MOROPO.SINETGIAGOB.GovCamaguey.Interop.API.Connect.Clients
{
    public class PersonsAPIConnectClient : Framework.Guama.Core.Presentation.API.Connect.Clients.Contract.AbstractArchivableEnableableEntityManagementFilteredAPIConnectClient<GovCamaguey.Persistence.Entities.Person, System.Guid, Common.Models.Input.PersonInputModel, Common.Models.Display.PersonDisplayModel, Common.Models.Filter.PersonFilterModel>
    {
        public PersonsAPIConnectClient(Framework.Guama.Core.Presentation.API.Connect.Providers.Facade.IHttpClientHelperProviderFacade httpClientHelperProvider) : base(httpClientHelperProvider)
        {
        }

        protected override string TargetController => "api/governance/persons";
    }
}
