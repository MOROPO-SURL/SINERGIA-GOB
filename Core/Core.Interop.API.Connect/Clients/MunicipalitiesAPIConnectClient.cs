namespace MOROPO.SINERGIAGOB.Core.Interop.API.Connect.Clients
{
    public class MunicipalitiesAPIConnectClient : Framework.Guama.Core.Presentation.API.Connect.Clients.Contract.AbstractEntityConsultFilteredAPIConnectClient<Framework.Guama.Location.Persistence.Entities.Municipality, System.Guid, Common.Models.Display.MunicipalityDisplayModel, Common.Models.Filter.MunicipalityFilterModel>
    {
        public MunicipalitiesAPIConnectClient(Framework.Guama.Core.Presentation.API.Connect.Providers.Facade.IHttpClientHelperProviderFacade httpClientHelperProvider) : base(httpClientHelperProvider)
        {
        }

        protected override string TargetController => "api/fwrk/municipalities";
    }
}
