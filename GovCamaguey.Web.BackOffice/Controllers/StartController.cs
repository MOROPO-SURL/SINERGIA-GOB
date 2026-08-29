namespace MOROPO.SINETGIAGOB.GovCamaguey.Web.BackOffice.Controllers;

public class StartController : Microsoft.AspNetCore.Mvc.Controller
{
    public Microsoft.AspNetCore.Mvc.IActionResult Index()
    {
        return Redirect("management/dashboard");
    }
}
