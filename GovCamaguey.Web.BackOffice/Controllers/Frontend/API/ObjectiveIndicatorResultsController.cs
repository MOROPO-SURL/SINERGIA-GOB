using Microsoft.AspNetCore.Mvc;
using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.Framework.Guama.Core.Presentation.Web.Security.Attributes;
using MOROPO.Framework.Guama.Security.Personal.Persistence.Entities;
using MOROPO.SINERGIAGOB.GovCamaguey.Persistence.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Web.BackOffice.Controllers.Frontend.API;

[Microsoft.AspNetCore.Authorization.Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
[Route("api/governance/[controller]")]
[ApiController]
public class ObjectiveIndicatorResultsController : Framework.Guama.Core.Presentation.Web.API.Controllers.Contract.AbstractEntityAPIManagementController<ObjectiveIndicatorResult, long, Interop.API.Common.Models.Input.ObjectiveIndicatorResultInputModel, Interop.API.Common.Models.Display.ObjectiveIndicatorResultDisplayModel>
{
    private readonly IEntityRepository<ObjectiveIndicator, System.Guid> indicatorRepository;
    private readonly IBridgeRepository<ObjectiveUserBridge, Objective, System.Guid, User, string> userBridgeRepository;

    public ObjectiveIndicatorResultsController(IBridgeRepository<ObjectiveUserBridge, Objective, System.Guid, User, string> userBridgeRepository, IEntityRepository<ObjectiveIndicator, System.Guid> objectiveIndicatorRepository, IEntityRepository<ObjectiveIndicatorResult, long> repository, Microsoft.Extensions.Localization.IStringLocalizer<ObjectiveIndicatorResultsController> localizer, Microsoft.Extensions.Logging.ILogger<ObjectiveIndicatorResultsController> logger)
        : base(repository, localizer, logger)
    {
        indicatorRepository = objectiveIndicatorRepository;
        this.userBridgeRepository = userBridgeRepository;
    }

    protected override Expression<System.Func<ObjectiveIndicatorResult, bool>> GetGlobalSelector()
    {
        return entity => !entity.Indicator.Archived;
    }

    protected override System.Func<ObjectiveIndicatorResult, object> GetOrderSelector()
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

    [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.ObjectiveIndicatorResultCreatorPermission)]
    public override ActionResult Create([FromBody] Interop.API.Common.Models.Input.ObjectiveIndicatorResultInputModel model)
    {
        var indicator = indicatorRepository.Get(model.IndicatorId);

        if (userBridgeRepository.Count(bridge => bridge.ProwId == indicator.ObjectiveId && bridge.SternId == User.Identity.Name) > 0) return base.Create(model);

        return new ForbidResult();
    }

    [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.ObjectiveIndicatorResultEditorPermission)]
    public override ActionResult Edit([FromBody] Interop.API.Common.Models.Input.ObjectiveIndicatorResultInputModel model)
    {
        var indicator = indicatorRepository.Get(model.IndicatorId);

        if (userBridgeRepository.Count(bridge => bridge.ProwId == indicator.ObjectiveId && bridge.SternId == User.Identity.Name) > 0) return base.Edit(model);

        return new ForbidResult();
    }

    [HttpPost("lasts/by/indicators")]
    public ActionResult LastsByObjective([FromBody] System.Guid[] keys)
    {
        var rawResult = new List<ObjectiveIndicatorResult>();

        foreach (var key in keys)
        {
            var last = Repository.GetAll().Where(entity => entity.IndicatorId == key).OrderByDescending(entity => entity.At).FirstOrDefault();

            if (last == null) continue;

            rawResult.Add(last);
        }

        return Ok(rawResult.Select(entity => EntityToDisplayModel(entity)).ToArray());
    }
}
