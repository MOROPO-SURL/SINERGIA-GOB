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
public class ProyectionIndicatorResultsController : Framework.Guama.Core.Presentation.Web.API.Controllers.Contract.AbstractEntityAPIManagementController<ProyectionIndicatorResult, long, Interop.API.Common.Models.Input.ProyectionIndicatorResultInputModel, Interop.API.Common.Models.Display.ProyectionIndicatorResultDisplayModel>
{
    private readonly IEntityRepository<ProyectionIndicator, System.Guid> indicatorRepository;
    private readonly IBridgeRepository<ObjectiveUserBridge, Objective, System.Guid, User, string> objectiveUserBridgeRepository;
    private readonly IBridgeRepository<ProyectionUserBridge, Proyection, System.Guid, User, string> proyectionUserBridgeRepository;

    public ProyectionIndicatorResultsController(IBridgeRepository<ProyectionUserBridge, Proyection, System.Guid, User, string> proyectionUserBridgeRepository, IBridgeRepository<ObjectiveUserBridge, Objective, System.Guid, User, string> objectiveUserBridgeRepository, IEntityRepository<ProyectionIndicator, System.Guid> indicatorRepository, IEntityRepository<ProyectionIndicatorResult, long> repository, Microsoft.Extensions.Localization.IStringLocalizer<ProyectionIndicatorResultsController> localizer, Microsoft.Extensions.Logging.ILogger<ProyectionIndicatorResultsController> logger)
        : base(repository, localizer, logger)
    {
        this.indicatorRepository = indicatorRepository;
        this.objectiveUserBridgeRepository = objectiveUserBridgeRepository;
        this.proyectionUserBridgeRepository = proyectionUserBridgeRepository;
    }

    protected override Expression<System.Func<ProyectionIndicatorResult, bool>> GetGlobalSelector()
    {
        return entity => !entity.Indicator.Archived;
    }

    protected override System.Func<ProyectionIndicatorResult, object> GetOrderSelector()
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

    [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.ProyectionIndicatorResultCreatorPermission)]
    public override ActionResult Create([FromBody] Interop.API.Common.Models.Input.ProyectionIndicatorResultInputModel model)
    {
        if (IsIndicatorModerable(model.IndicatorId)) return base.Create(model);

        return new ForbidResult();
    }

    [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.ProyectionIndicatorResultEditorPermission)]
    public override ActionResult Edit([FromBody] Interop.API.Common.Models.Input.ProyectionIndicatorResultInputModel model)
    {
        if (IsIndicatorModerable(model.IndicatorId)) return base.Edit(model);

        return new ForbidResult();
    }

    [HttpPost("lasts/by/indicators")]
    public ActionResult LastsByObjective([FromBody] System.Guid[] keys)
    {
        var rawResult = new List<ProyectionIndicatorResult>();

        foreach (var key in keys)
        {
            var last = Repository.GetAll().Where(entity => entity.IndicatorId == key).OrderByDescending(entity => entity.At).FirstOrDefault();

            if (last == null) continue;

            rawResult.Add(last);
        }

        return Ok(rawResult.Select(entity => EntityToDisplayModel(entity)).ToArray());
    }

    private bool IsIndicatorModerable(System.Guid indicatorId)
    {
        var indicator = indicatorRepository.Get(indicatorId);

        if (objectiveUserBridgeRepository.Count(bridge => bridge.IsProyectionsIncluded && bridge.ProwId == indicator.Proyection.ObjectiveId && bridge.SternId == User.Identity.Name) > 0) return true;

        if (proyectionUserBridgeRepository.Count(bridge => bridge.ProwId == indicator.ProyectionId && bridge.SternId == User.Identity.Name) > 0) return true;

        return false;
    }
}
