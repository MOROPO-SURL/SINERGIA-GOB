using System.Linq;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Interop.API.Connect.Clients
{
    public class ObjectiveIndicatorResultsAPIConnectClient : Framework.Guama.Core.Presentation.API.Connect.Clients.Contract.AbstractEntityManagementAPIConnectClient<GovCamaguey.Persistence.Entities.ObjectiveIndicatorResult, long, Common.Models.Input.ObjectiveIndicatorResultInputModel, Common.Models.Display.ObjectiveIndicatorResultDisplayModel>
    {
        public ObjectiveIndicatorResultsAPIConnectClient(Framework.Guama.Core.Presentation.API.Connect.Providers.Facade.IHttpClientHelperProviderFacade httpClientHelperProvider) : base(httpClientHelperProvider)
        {
        }

        protected override string TargetController => "api/governance/objectiveindicatorresults";

        public System.Threading.Tasks.Task<Common.Models.Display.ObjectiveIndicatorResultDisplayModel[]> GetLastsByIndicators(Common.Models.Display.ObjectiveIndicatorDisplayModel[] indicators)
        {
            return httpClientHelperProvider.HttpClientHelper.PostAsync<System.Guid[], Common.Models.Display.ObjectiveIndicatorResultDisplayModel[]>($"{TargetController}/lasts/by/indicators", indicators.Select(model => model.Id).ToArray());
        }
    }
}
