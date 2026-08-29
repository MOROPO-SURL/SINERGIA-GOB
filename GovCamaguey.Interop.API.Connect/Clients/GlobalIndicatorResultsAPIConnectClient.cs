namespace MOROPO.SINETGIAGOB.GovCamaguey.Interop.API.Connect.Clients
{
    public class GlobalIndicatorResultsAPIConnectClient : Framework.Guama.Core.Presentation.API.Connect.Clients.Contract.AbstractEntityManagementAPIConnectClient<GovCamaguey.Persistence.Entities.GlobalIndicatorResult, long, Common.Models.Input.GlobalIndicatorResultInputModel, Common.Models.Display.GlobalIndicatorResultDisplayModel>
    {
        public GlobalIndicatorResultsAPIConnectClient(Framework.Guama.Core.Presentation.API.Connect.Providers.Facade.IHttpClientHelperProviderFacade httpClientHelperProvider) : base(httpClientHelperProvider)
        {
        }

        protected override string TargetController => "api/governance/globalindicatorresults";

        public System.Threading.Tasks.Task<Common.Models.Display.GlobalIndicatorResultDisplayModel[]> GetLasts()
        {
            return httpClientHelperProvider.HttpClientHelper.GetAsync<Common.Models.Display.GlobalIndicatorResultDisplayModel[]>($"{TargetController}/lasts");
        }
    }
}
