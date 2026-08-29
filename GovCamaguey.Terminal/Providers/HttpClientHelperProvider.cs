using Microsoft.Extensions.DependencyInjection;
using MOROPO.SINETGIAGOB.GovCamaguey.Terminal.Factories;

namespace MOROPO.SINETGIAGOB.GovCamaguey.Terminal.Providers
{
    public class HttpClientHelperProvider : Core.Presentation.Terminal.MAUI.Providers.Facade.IReseableHttpClientHelperProviderFacade
    {
        public Framework.Guama.Core.Presentation.API.Connect.Helpers.HttpClientHelper HttpClientHelper { get; private set; }

        public HttpClientHelperProvider()
        {
            Reset();
        }

        public void Reset()
        {
            var settingManager = Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Core.Presentation.Terminal.MAUI.Managers.Facade.ISettingManagerFacade>();

#if DEBUG
            var serverBaseUri = Microsoft.Maui.Devices.DeviceInfo.Platform == Microsoft.Maui.Devices.DevicePlatform.Android ? "https://10.0.2.2:7208" : "https://localhost:7208";
#else
            var serverBaseUri = "https://cmi.gpcmg.co.cu";
#endif

            HttpClientHelper = new Framework.Guama.Core.Presentation.API.Connect.Helpers.HttpClientHelper(HttpClientHandlerFactory.Build(), serverBaseUri, GetAuthorizeHeader);
        }

        private System.Collections.Generic.KeyValuePair<string, string> GetAuthorizeHeader()
        {
            var session = Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Core.Presentation.Terminal.MAUI.Managers.Facade.ISessionManagerFacade>().CurrentSession;

            if (session == null)
            {
                return new System.Collections.Generic.KeyValuePair<string, string>();
            }
            else
            {
                return new System.Collections.Generic.KeyValuePair<string, string>(session.Type, session.Token);
            }
        }
    }
 }
