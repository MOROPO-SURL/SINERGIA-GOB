namespace MOROPO.SINETGIAGOB.GovCamaguey.Interop.API.Connect.Clients
{
    public sealed class CurrentTerminalReleaseAPIConnectClient
    {
        private readonly Framework.Guama.Core.Presentation.API.Connect.Providers.Facade.IHttpClientHelperProviderFacade httpClientHelperProvider;

        public CurrentTerminalReleaseAPIConnectClient(Framework.Guama.Core.Presentation.API.Connect.Providers.Facade.IHttpClientHelperProviderFacade httpClientHelperProvider)
        {
            this.httpClientHelperProvider = httpClientHelperProvider;
        }

        public System.Threading.Tasks.Task<Common.Models.CurrentTerminalReleaseDetailModel> GetDetailAsync()
        {
            return httpClientHelperProvider.HttpClientHelper.GetAsync<Common.Models.CurrentTerminalReleaseDetailModel>("api/terminal/current/detail");
        }
    }
}
