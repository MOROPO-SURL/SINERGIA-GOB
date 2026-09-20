using Microsoft.AspNetCore.Mvc;
using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.Framework.Guama.Core.Presentation.Web.Security.Attributes;
using MOROPO.SINERGIAGOB.GovCamaguey.Persistence.Entities;
using System.Linq;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Web.BackOffice.Controllers.Frontend.API;

[Microsoft.AspNetCore.Authorization.Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
[Route("api/governance/[controller]")]
[ApiController]
public class GlobalIndicatorResultsController : Framework.Guama.Core.Presentation.Web.API.Controllers.Contract.AbstractEntityAPIManagementController<GlobalIndicatorResult, long, Interop.API.Common.Models.Input.GlobalIndicatorResultInputModel, Interop.API.Common.Models.Display.GlobalIndicatorResultDisplayModel>
{
    private readonly IEntityRepository<GlobalIndicator, System.Guid> globalIndicatorRepository;

    public GlobalIndicatorResultsController(IEntityRepository<GlobalIndicator, System.Guid> globalIndicatorRepository, IEntityRepository<GlobalIndicatorResult, long> repository, Microsoft.Extensions.Localization.IStringLocalizer<GlobalIndicatorResultsController> localizer, Microsoft.Extensions.Logging.ILogger<GlobalIndicatorResultsController> logger)
        : base(repository, localizer, logger)
    {
        this.globalIndicatorRepository = globalIndicatorRepository;
    }

    protected override System.Func<GlobalIndicatorResult, object> GetOrderSelector()
    {
        return entity => entity.At;
    }

    protected override bool IsOrderDescending()
    {
        return true;
    }

    public override ActionResult Delete(long key)
    {
        return NotFound();
    }

    [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.GlobalIndicatorResultCreatorPermission)]
    public override ActionResult Create([FromBody] Interop.API.Common.Models.Input.GlobalIndicatorResultInputModel model)
    {
        return base.Create(model);
    }

    [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.GlobalIndicatorResultEditorPermission)]
    public override ActionResult Edit([FromBody] Interop.API.Common.Models.Input.GlobalIndicatorResultInputModel model)
    {
        return base.Edit(model);
    }

    [HttpGet("[action]")]
    public ActionResult Lasts()
    {
        var result = new System.Collections.Generic.List<GlobalIndicatorResult>();

        var indicators = globalIndicatorRepository.GetAll().ToArray();

        foreach (var indicator in indicators)
        {
            var last = Repository.FindBy(entity => entity.IndicatorId == indicator.Id).OrderByDescending(entity => entity.At).FirstOrDefault();

            if (last == null) continue;

            result.Add(last);
        }

        return Ok(result.Select(entity => EntityToDisplayModel(entity)).ToArray());
    }
}
