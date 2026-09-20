namespace MOROPO.SINERGIAGOB.GovCamaguey.Web.BackOffice.Services.Contract;

public interface IViewRenderService
{
    System.Threading.Tasks.Task<string> RenderViewToStringAsync(string viewName, object model);
    System.Threading.Tasks.Task<string> RenderPartialViewToStringAsync(string viewName, object model);
}