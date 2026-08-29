namespace MOROPO.SINETGIAGOB.Core.Presentation.Web.Frontend.Controllers.API
{
    [Microsoft.AspNetCore.Mvc.Route("api/fwrk/[controller]")]
    [Microsoft.AspNetCore.Mvc.ApiController]
    public class MunicipalitiesController : Framework.Guama.Core.Presentation.Web.API.Controllers.Contract.AbstractEntityAPIConsultFilteredController<Framework.Guama.Location.Persistence.Entities.Municipality, System.Guid, Interop.API.Common.Models.Display.MunicipalityDisplayModel, Interop.API.Common.Models.Filter.MunicipalityFilterModel>
    {
        public MunicipalitiesController(Framework.Guama.Core.Persistence.Repositories.IEntityRepository<Framework.Guama.Location.Persistence.Entities.Municipality, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<MunicipalitiesController> localizer, Microsoft.Extensions.Logging.ILogger<MunicipalitiesController> logger) : base(repository, localizer, logger)
        {
        }
    }
}
