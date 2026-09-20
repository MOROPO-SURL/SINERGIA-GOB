namespace MOROPO.SINERGIAGOB.Core.Presentation.Web.Frontend.Controllers.API
{
    [Microsoft.AspNetCore.Mvc.Route("api/fwrk/[controller]")]
    [Microsoft.AspNetCore.Mvc.ApiController]
    public class StatesController : Framework.Guama.Core.Presentation.Web.API.Controllers.Contract.AbstractEntityAPIConsultController<Framework.Guama.Location.Persistence.Entities.State, System.Guid, Interop.API.Common.Models.Display.StateDisplayModel>
    {
        public StatesController(Framework.Guama.Core.Persistence.Repositories.IEntityRepository<Framework.Guama.Location.Persistence.Entities.State, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<StatesController> localizer, Microsoft.Extensions.Logging.ILogger<StatesController> logger) : base(repository, localizer, logger)
        {
        }
    }
}
