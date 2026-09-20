using Microsoft.AspNetCore.Mvc;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Web.BackOffice.Controllers.Frontend.API;

[Route("api/terminal/current")]
[ApiController]
public class CurrentTerminalReleaseController : ControllerBase
{
    private readonly Microsoft.Extensions.Options.IOptions<Configurations.CurrentTerminalReleaseDetailConfiguration> configurationOption;

    public CurrentTerminalReleaseController(Microsoft.Extensions.Options.IOptions<Configurations.CurrentTerminalReleaseDetailConfiguration> configurationOption)
    {
        this.configurationOption = configurationOption;
    }

    [HttpGet("[action]")]
    public ActionResult<Interop.API.Common.Models.CurrentTerminalReleaseDetailModel> Detail()
    {
        return new Interop.API.Common.Models.CurrentTerminalReleaseDetailModel
        {
            Version = configurationOption.Value.Version,
            Uri = configurationOption.Value.Uri,
            At = configurationOption.Value.At,
            Description = configurationOption.Value.Description
        };
    }
}