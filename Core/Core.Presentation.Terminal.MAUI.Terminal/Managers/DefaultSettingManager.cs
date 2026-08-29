namespace MOROPO.SINETGIAGOB.Core.Presentation.Terminal.MAUI.Managers
{
    public class DefaultSettingManager : Facade.ISettingManagerFacade
    {
        private const string USE_PROXY_SERVER_KEY = "use_proxy_server";
        private const string PROXY_SERVER_ADDRESS_KEY = "proxy_server_address";
        private const string USE_PROXY_AUTHENTICATION_KEY = "use_proxy_authentication";
        private const string PROXY_USERNAME_KEY = "proxy_username";
        private const string PROXY_PASSWORD_KEY = "proxy_password";

        public bool UseProxyServer 
        { 
            get => Microsoft.Maui.Storage.Preferences.Get(USE_PROXY_SERVER_KEY, false); 
            set => Microsoft.Maui.Storage.Preferences.Set(USE_PROXY_SERVER_KEY, value);
        }

        public string ProxyServerAddress 
        {
            get => Microsoft.Maui.Storage.Preferences.Get(PROXY_SERVER_ADDRESS_KEY, string.Empty);
            set => Microsoft.Maui.Storage.Preferences.Set(PROXY_SERVER_ADDRESS_KEY, value);
        }

        public bool UseProxyAuthentication
        {
            get => Microsoft.Maui.Storage.Preferences.Get(USE_PROXY_AUTHENTICATION_KEY, false);
            set => Microsoft.Maui.Storage.Preferences.Set(USE_PROXY_AUTHENTICATION_KEY, value);
        }

        public string ProxyUsername 
        {
            get => Microsoft.Maui.Storage.Preferences.Get(PROXY_USERNAME_KEY, string.Empty);
            set => Microsoft.Maui.Storage.Preferences.Set(PROXY_USERNAME_KEY, value);
        }

        public string ProxyPassword 
        {
            get => Microsoft.Maui.Storage.Preferences.Get(PROXY_PASSWORD_KEY, string.Empty);
            set => Microsoft.Maui.Storage.Preferences.Set(PROXY_PASSWORD_KEY, value);
        }
    }
}
