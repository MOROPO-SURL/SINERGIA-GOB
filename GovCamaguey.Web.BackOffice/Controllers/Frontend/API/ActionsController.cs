using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.Framework.Guama.Core.Presentation.Models;
using MOROPO.Framework.Guama.Core.Presentation.Web.API.Controllers.Contract;
using MOROPO.Framework.Guama.Core.Presentation.Web.Security.Attributes;
using MOROPO.Framework.Guama.Security.Personal.Persistence.Entities;
using MOROPO.SINETGIAGOB.GovCamaguey.Interop.API.Common.Models.Display;
using MOROPO.SINETGIAGOB.GovCamaguey.Interop.API.Common.Models.Input;
using MOROPO.SINETGIAGOB.GovCamaguey.Persistence.Entities;
using System.Linq;

namespace MOROPO.SINETGIAGOB.GovCamaguey.Web.BackOffice.Controllers.Frontend.API;

[Microsoft.AspNetCore.Authorization.Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
[Route("api/governance/[controller]")]
[ApiController]
public sealed class ActionsController : AbstractArchivableEntityAPIManagementController<Action, System.Guid, ActionInputModel, ActionDisplayModel>
{
    private readonly IBridgeRepository<ActionUserBridge, Action, System.Guid, User, string> userBridgeRepository;
    private readonly IEntityRepository<ActionMonthlyClosure, System.Guid> monthlyClosureRepository;
    private readonly IEntityRepository<ActionIndicator, System.Guid> indicatorRepository;
    private readonly IEntityRepository<ActionIndicatorMontlyPlan, System.Guid> indicatorMontlyPlanRepository;

    public ActionsController(IEntityRepository<ActionIndicatorMontlyPlan, System.Guid> indicatorMontlyPlanRepository, IEntityRepository<ActionIndicator, System.Guid> indicatorRepository, IEntityRepository<ActionMonthlyClosure, System.Guid> monthlyClosureRepository, IBridgeRepository<ActionUserBridge, Action, System.Guid, User, string> userBridgeRepository, IEntityRepository<Action, System.Guid> repository, IStringLocalizer<ActionsController> localizer, ILogger<ActionsController> logger) : base(repository, localizer, logger)
    {
        this.userBridgeRepository = userBridgeRepository;
        this.monthlyClosureRepository = monthlyClosureRepository;
        this.indicatorRepository = indicatorRepository;
        this.indicatorMontlyPlanRepository = indicatorMontlyPlanRepository;
    }

    public override ActionResult Delete(System.Guid key)
    {
        return NotFound();
    }

    [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.ActionViewerPermission)]
    public override ActionResult<ActionDisplayModel[]> Get()
    {
        return base.Get();
    }

    [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.ActionViewerPermission)]
    public override ActionResult<ActionDisplayModel> Get(System.Guid key)
    {
        return base.Get(key);
    }

    [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.ActionViewerPermission)]
    public override ActionResult<PaginationModel<ActionDisplayModel>> GetPaginate(int sheet = 1, int limit = 25)
    {
        return base.GetPaginate(sheet, limit);
    }

    public override ActionResult Create([FromBody] ActionInputModel model)
    {
        return NotFound();
    }

    [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.ActionEditorPermission)]
    public override ActionResult Edit([FromBody] ActionInputModel model)
    {
        if (userBridgeRepository.Count(bridge => bridge.ProwId == model.Id && bridge.SternId == User.Identity.Name) < 0) return new ForbidResult();

        return base.Edit(model);
    }

    [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.ActionRemoverPermission)]
    public override ActionResult Archive(System.Guid key)
    {
        if (userBridgeRepository.Count(bridge => bridge.ProwId == key && bridge.SternId == User.Identity.Name) < 0) return new ForbidResult();

        return base.Archive(key);
    }

    protected override void PreEdit(Action entity, ActionInputModel model)
    {
        base.PreEdit(entity, model);

        foreach (var _model in model.OutgoingIndicators)
        {
            var _entity = entity.Indicators.First(bridge => bridge.Id == _model.Id);

            _entity.Archived = true;

            indicatorRepository.Update(_entity, User.Identity.Name, false);
        }

        foreach (var _model in model.EditionIndicators)
        {
            var _entity = entity.Indicators.First(bridge => bridge.Id == _model.Id);

            _model.Mergue(_entity);

            indicatorRepository.Update(_entity, User.Identity.Name, false);

            ExtendEdit(_entity, _model);
        }

        foreach (var _model in model.IncomingIndicators)
        {
            var _entity = _model.Export();

            _entity.ActionId = entity.Id;

            indicatorRepository.Add(_entity, User.Identity.Name, false);

            ExtendCreate(_entity, _model);
        }

        foreach (var _model in model.OutgoingMonthlyClosures)
        {
            var _entity = entity.MonthlyClosures.First(bridge => bridge.Id == _model.Id);

            monthlyClosureRepository.Delete(_entity, User.Identity.Name, false);
        }

        foreach (var _model in model.EditionMonthlyClosures)
        {
            var _entity = entity.MonthlyClosures.First(bridge => bridge.Id == _model.Id);

            _model.Mergue(_entity);

            monthlyClosureRepository.Update(_entity, User.Identity.Name, false);
        }

        foreach (var _model in model.IncomingMonthlyClosures)
        {
            var _entity = _model.Export();

            _entity.ActionId = entity.Id;

            monthlyClosureRepository.Add(_entity, User.Identity.Name, false);
        }
    }

    private void ExtendCreate(ActionIndicator entity, ActionIndicatorInputModel model)
    {
        foreach (var _model in model.IncomingMontlyPlans)
        {
            var _entity = _model.Export();

            _entity.IndicatorId = entity.Id;

            indicatorMontlyPlanRepository.Add(_entity, User.Identity.Name, false);
        }
    }

    private void ExtendEdit(ActionIndicator entity, ActionIndicatorInputModel model)
    {
        foreach (var _model in model.OutgoingMontlyPlans)
        {
            var _entity = entity.MontlyPlans.First(bridge => bridge.Id == _model.Id);

            indicatorMontlyPlanRepository.Delete(_entity, User.Identity.Name, false);
        }

        foreach (var _model in model.EditionMontlyPlans)
        {
            var _entity = entity.MontlyPlans.First(bridge => bridge.Id == _model.Id);

            _model.Mergue(_entity);

            indicatorMontlyPlanRepository.Update(_entity, User.Identity.Name, false);
        }

        foreach (var _model in model.IncomingMontlyPlans)
        {
            var _entity = _model.Export();

            _entity.IndicatorId = entity.Id;

            indicatorMontlyPlanRepository.Add(_entity, User.Identity.Name, false);
        }
    }
}
