using System.Linq;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Interop.API.Connect.Clients
{
    public class ActionIndicatorResultsAPIConnectClient : Framework.Guama.Core.Presentation.API.Connect.Clients.Contract.AbstractEntityManagementAPIConnectClient<GovCamaguey.Persistence.Entities.ActionIndicatorResult, long, Common.Models.Input.ActionIndicatorResultInputModel, Common.Models.Display.ActionIndicatorResultDisplayModel>
    {
        public ActionIndicatorResultsAPIConnectClient(Framework.Guama.Core.Presentation.API.Connect.Providers.Facade.IHttpClientHelperProviderFacade httpClientHelperProvider) : base(httpClientHelperProvider)
        {
        }

        protected override string TargetController => "api/governance/actionindicatorresults";

        public System.Threading.Tasks.Task<Common.Models.Display.ActionIndicatorResultDisplayModel[]> GetLastsByIndicators(Common.Models.Display.ActionIndicatorDisplayModel[] indicators)
        {
            return httpClientHelperProvider.HttpClientHelper.PostAsync<System.Guid[], Common.Models.Display.ActionIndicatorResultDisplayModel[]>($"{TargetController}/lasts/by/indicators", indicators.Select(model => model.Id).ToArray());
        }
    }
}