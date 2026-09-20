using Microsoft.Extensions.DependencyInjection;

namespace MOROPO.SINERGIAGOB.Core.Presentation.Terminal.MAUI.Managers
{
    public class DefaultSessionManager : Facade.ISessionManagerFacade
	{
        public Interop.API.Common.Models.AuthenticatedSessionModel CurrentSession { get; private set; }

        public async System.Threading.Tasks.Task SignInAsync(string login, string password)
        {
            var securityAPIConnectClient = new Interop.API.Connect.Clients.SecurityAPIConnectClient(Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Framework.Guama.Core.Presentation.API.Connect.Providers.Facade.IHttpClientHelperProviderFacade>().HttpClientHelper);

            CurrentSession = await securityAPIConnectClient.AuthenticateAsync(new Interop.API.Common.Models.AuthenticationIntentModel
            {
                User = login,
                Password = password
            });
        }

        public async System.Threading.Tasks.Task ChangePasswordAsync(string oldPassword, string newPassword)
        {
            var securityAPIConnectClient = new Interop.API.Connect.Clients.SecurityAPIConnectClient(Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Framework.Guama.Core.Presentation.API.Connect.Providers.Facade.IHttpClientHelperProviderFacade>().HttpClientHelper);

            await securityAPIConnectClient.ChangePasswordAsync(new Interop.API.Common.Models.UserPasswordChangeRequestModel
            {
                OldPassword = oldPassword,
                NewPassword = newPassword
            });
        }

        public System.Threading.Tasks.Task SignOutAsync()
        {
            CurrentSession = null;

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}

