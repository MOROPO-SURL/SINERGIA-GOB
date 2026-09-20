using System.Linq;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Interop.API.Connect.Clients
{
    public class ProyectionIndicatorResultsAPIConnectClient : Framework.Guama.Core.Presentation.API.Connect.Clients.Contract.AbstractEntityManagementAPIConnectClient<GovCamaguey.Persistence.Entities.ProyectionIndicatorResult, long, Common.Models.Input.ProyectionIndicatorResultInputModel, Common.Models.Display.ProyectionIndicatorResultDisplayModel>
    {
        public ProyectionIndicatorResultsAPIConnectClient(Framework.Guama.Core.Presentation.API.Connect.Providers.Facade.IHttpClientHelperProviderFacade httpClientHelperProvider) : base(httpClientHelperProvider)
        {
        }

        protected override string TargetController => "api/governance/proyectionindicatorresults";

        public System.Threading.Tasks.Task<Common.Models.Display.ProyectionIndicatorResultDisplayModel[]> GetLastsByIndicators(Common.Models.Display.ProyectionIndicatorDisplayModel[] indicators)
        {
            return httpClientHelperProvider.HttpClientHelper.PostAsync<System.Guid[], Common.Models.Display.ProyectionIndicatorResultDisplayModel[]>($"{TargetController}/lasts/by/indicators", indicators.Select(model => model.Id).ToArray());
        }
    }
}
