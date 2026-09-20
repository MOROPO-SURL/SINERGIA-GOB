namespace MOROPO.SINERGIAGOB.Core.Interop.API.Connect.Clients
{
    public class StatesAPIConnectClient : Framework.Guama.Core.Presentation.API.Connect.Clients.Contract.AbstractEntityConsultAPIConnectClient<Framework.Guama.Location.Persistence.Entities.State, System.Guid, Common.Models.Display.StateDisplayModel>
    {
        public StatesAPIConnectClient(Framework.Guama.Core.Presentation.API.Connect.Providers.Facade.IHttpClientHelperProviderFacade httpClientHelperProvider) : base(httpClientHelperProvider)
        {
        }

        protected override string TargetController => "api/fwrk/states";
    }
}
