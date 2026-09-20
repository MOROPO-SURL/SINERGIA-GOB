using MOROPO.SINERGIAGOB.Core.Interop.API.Common.Models;

namespace MOROPO.SINERGIAGOB.Core.Interop.API.Connect.Clients
{
	public class SecurityAPIConnectClient
	{
        private readonly Framework.Guama.Core.Presentation.API.Connect.Helpers.HttpClientHelper httpClientHelper;

        public SecurityAPIConnectClient(Framework.Guama.Core.Presentation.API.Connect.Helpers.HttpClientHelper httpClientHelper)
        {
            this.httpClientHelper = httpClientHelper;
        }

        public async System.Threading.Tasks.Task<AuthenticatedSessionModel> AuthenticateAsync(AuthenticationIntentModel modelInput)
        {
            return await httpClientHelper.PostAsync<AuthenticationIntentModel, AuthenticatedSessionModel>("api/fwrk/security/authenticate", modelInput);
        }

        public async System.Threading.Tasks.Task ChangePasswordAsync(UserPasswordChangeRequestModel model)
        {
            await httpClientHelper.PostAsync("api/fwrk/security/changepassword", model);
        }
    }
}

