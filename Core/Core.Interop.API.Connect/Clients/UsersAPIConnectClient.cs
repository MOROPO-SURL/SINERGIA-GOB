namespace MOROPO.SINERGIAGOB.Core.Interop.API.Connect.Clients
{
    public class UsersAPIConnectClient : Framework.Guama.Core.Presentation.API.Connect.Clients.Contract.AbstractEntityConsultFilteredAPIConnectClient<Framework.Guama.Security.Personal.Persistence.Entities.User, string, Common.Models.Display.UserDisplayModel, Common.Models.Filter.UserFilterModel>
    {
        public UsersAPIConnectClient(Framework.Guama.Core.Presentation.API.Connect.Providers.Facade.IHttpClientHelperProviderFacade httpClientHelperProvider) : base(httpClientHelperProvider)
        {
        }

        protected override string TargetController => "api/fwrk/users";
    }
}
