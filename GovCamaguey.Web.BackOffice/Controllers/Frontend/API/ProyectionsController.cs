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
public sealed class ProyectionsController : AbstractArchivableEntityAPIManagementController<Proyection, System.Guid, ProyectionInputModel, ProyectionDisplayModel>
{
    private readonly IBridgeRepository<ProyectionUserBridge, Proyection, System.Guid, User, string> proyectionUserBridgeRepository;
    private readonly IEntityRepository<ProyectionIndicator, System.Guid> proyectionIndicatorRepository;
    private readonly IEntityRepository<ProyectionIndicatorMontlyPlan, System.Guid> proyectionIndicatorMontlyPlanRepository;
    private readonly IEntityRepository<Action, System.Guid> actionRepository;
    private readonly IEntityRepository<ActionMonthlyClosure, System.Guid> actionMonthlyClosureRepository;
    private readonly IEntityRepository<ActionIndicator, System.Guid> actionIndicatorRepository;
    private readonly IEntityRepository<ActionIndicatorMontlyPlan, System.Guid> actionIndicatorMontlyPlanRepository;
    private readonly IBridgeRepository<ActionUserBridge, Action, System.Guid, User, string> actionUserBridgeRepository;

    public ProyectionsController(IBridgeRepository<ActionUserBridge, Action, System.Guid, User, string> actionUserBridgeRepository, IEntityRepository<ActionIndicatorMontlyPlan, System.Guid> actionIndicatorMontlyPlanRepository, IEntityRepository<ActionIndicator, System.Guid> actionIndicatorRepository, IEntityRepository<ActionMonthlyClosure, System.Guid> actionMonthlyClosureRepository, IEntityRepository<Action, System.Guid> actionRepository, IEntityRepository<ProyectionIndicatorMontlyPlan, System.Guid> proyectionIndicatorMontlyPlanRepository, IEntityRepository<ProyectionIndicator, System.Guid> proyectionIndicatorRepository, IBridgeRepository<ProyectionUserBridge, Proyection, System.Guid, User, string> proyectionUserBridgeRepository, IEntityRepository<Proyection, System.Guid> repository, IStringLocalizer<ProyectionsController> localizer, ILogger<ProyectionsController> logger) : base(repository, localizer, logger)
    {
        this.proyectionUserBridgeRepository = proyectionUserBridgeRepository;
        this.proyectionIndicatorRepository = proyectionIndicatorRepository;
        this.proyectionIndicatorMontlyPlanRepository = proyectionIndicatorMontlyPlanRepository;
        this.actionRepository = actionRepository;
        this.actionMonthlyClosureRepository = actionMonthlyClosureRepository;
        this.actionIndicatorRepository = actionIndicatorRepository;
        this.actionIndicatorMontlyPlanRepository = actionIndicatorMontlyPlanRepository;
        this.actionUserBridgeRepository = actionUserBridgeRepository;
    }

    public override ActionResult Delete(System.Guid key)
    {
        return NotFound();
    }

    [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.ProyectionViewerPermission)]
    public override ActionResult<ProyectionDisplayModel[]> Get()
    {
        return base.Get();
    }

    [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.ProyectionViewerPermission)]
    public override ActionResult<ProyectionDisplayModel> Get(System.Guid key)
    {
        return base.Get(key);
    }

    [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.ProyectionViewerPermission)]
    public override ActionResult<PaginationModel<ProyectionDisplayModel>> GetPaginate(int sheet = 1, int limit = 25)
    {
        return base.GetPaginate(sheet, limit);
    }

    public override ActionResult Create([FromBody] ProyectionInputModel model)
    {
        return NotFound();
    }

    [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.ProyectionEditorPermission)]
    public override ActionResult Edit([FromBody] ProyectionInputModel model)
    {
        if (proyectionUserBridgeRepository.Count(bridge => bridge.ProwId == model.Id && bridge.SternId == User.Identity.Name) < 0) return new ForbidResult();

        return base.Edit(model);
    }

    [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.ProyectionRemoverPermission)]
    public override ActionResult Archive(System.Guid key)
    {
        if (proyectionUserBridgeRepository.Count(bridge => bridge.ProwId == key && bridge.SternId == User.Identity.Name) < 0) return new ForbidResult();

        return base.Archive(key);
    }

    protected override void PreEdit(Proyection entity, ProyectionInputModel model)
    {
        base.PreEdit(entity, model);

        foreach (var _model in model.OutgoingIndicators)
        {
            var _entity = entity.Indicators.First(bridge => bridge.Id == _model.Id);

            _entity.Archived = true;

            proyectionIndicatorRepository.Update(_entity, User.Identity.Name, false);
        }

        foreach (var _model in model.EditionIndicators)
        {
            var _entity = entity.Indicators.First(bridge => bridge.Id == _model.Id);

            _model.Mergue(_entity);

            proyectionIndicatorRepository.Update(_entity, User.Identity.Name, false);

            ExtendEdit(_entity, _model);
        }

        foreach (var _model in model.IncomingIndicators)
        {
            var _entity = _model.Export();

            _entity.ProyectionId = entity.Id;

            proyectionIndicatorRepository.Add(_entity, User.Identity.Name, false);

            ExtendCreate(_entity, _model);
        }

        foreach (var _model in model.OutgoingActions)
        {
            if (!IsActionModerable(entity.ObjectiveId, entity.Id, _model.Id)) continue;

            var _entity = entity.Actions.First(bridge => bridge.Id == _model.Id);

            _entity.Archived = true;

            actionRepository.Update(_entity, User.Identity.Name, false);
        }

        foreach (var _model in model.EditionActions)
        {
            if (!IsActionModerable(entity.ObjectiveId, entity.Id, _model.Id)) continue;

            var _entity = entity.Actions.First(bridge => bridge.Id == _model.Id);

            _model.Mergue(_entity);

            actionRepository.Update(_entity, User.Identity.Name, false);

            ExtendEdit(_entity, _model);
        }

        foreach (var _model in model.IncomingActions)
        {
            var _entity = _model.Export();

            _entity.ProyectionId = entity.Id;

            actionRepository.Add(_entity, User.Identity.Name, false);

            ExtendCreate(_entity, _model);
        }
    }

    private void ExtendCreate(Action entity, ActionInputModel model)
    {
        foreach (var _model in model.IncomingIndicators)
        {
            var _entity = _model.Export();

            _entity.ActionId = entity.Id;

            actionIndicatorRepository.Add(_entity, User.Identity.Name, false);

            ExtendCreate(_entity, _model);
        }

        foreach (var _model in model.IncomingMonthlyClosures)
        {
            var _entity = _model.Export();

            _entity.ActionId = entity.Id;

            actionMonthlyClosureRepository.Add(_entity, User.Identity.Name, false);
        }
    }

    private void ExtendEdit(Action entity, ActionInputModel model)
    {
        foreach (var _model in model.OutgoingIndicators)
        {
            var _entity = entity.Indicators.First(bridge => bridge.Id == _model.Id);

            _entity.Archived = true;

            actionIndicatorRepository.Update(_entity, User.Identity.Name, false);
        }

        foreach (var _model in model.EditionIndicators)
        {
            var _entity = entity.Indicators.First(bridge => bridge.Id == _model.Id);

            _model.Mergue(_entity);

            actionIndicatorRepository.Update(_entity, User.Identity.Name, false);

            ExtendEdit(_entity, _model);
        }

        foreach (var _model in model.IncomingIndicators)
        {
            var _entity = _model.Export();

            _entity.ActionId = entity.Id;

            actionIndicatorRepository.Add(_entity, User.Identity.Name, false);

            ExtendCreate(_entity, _model);
        }

        foreach (var _model in model.OutgoingMonthlyClosures)
        {
            var _entity = entity.MonthlyClosures.First(bridge => bridge.Id == _model.Id);

            actionMonthlyClosureRepository.Delete(_entity, User.Identity.Name, false);
        }

        foreach (var _model in model.EditionMonthlyClosures)
        {
            var _entity = entity.MonthlyClosures.First(bridge => bridge.Id == _model.Id);

            _model.Mergue(_entity);

            actionMonthlyClosureRepository.Update(_entity, User.Identity.Name, false);
        }

        foreach (var _model in model.IncomingMonthlyClosures)
        {
            var _entity = _model.Export();

            _entity.ActionId = entity.Id;

            actionMonthlyClosureRepository.Add(_entity, User.Identity.Name, false);
        }
    }

    private void ExtendCreate(ProyectionIndicator entity, ProyectionIndicatorInputModel model)
    {
        foreach (var _model in model.IncomingMontlyPlans)
        {
            var _entity = _model.Export();

            _entity.IndicatorId = entity.Id;

            proyectionIndicatorMontlyPlanRepository.Add(_entity, User.Identity.Name, false);
        }
    }

    private void ExtendEdit(ProyectionIndicator entity, ProyectionIndicatorInputModel model)
    {
        foreach (var _model in model.OutgoingMontlyPlans)
        {
            var _entity = entity.MontlyPlans.First(bridge => bridge.Id == _model.Id);

            proyectionIndicatorMontlyPlanRepository.Delete(_entity, User.Identity.Name, false);
        }

        foreach (var _model in model.EditionMontlyPlans)
        {
            var _entity = entity.MontlyPlans.First(bridge => bridge.Id == _model.Id);

            _model.Mergue(_entity);

            proyectionIndicatorMontlyPlanRepository.Update(_entity, User.Identity.Name, false);
        }

        foreach (var _model in model.IncomingMontlyPlans)
        {
            var _entity = _model.Export();

            _entity.IndicatorId = entity.Id;

            proyectionIndicatorMontlyPlanRepository.Add(_entity, User.Identity.Name, false);
        }
    }

    private void ExtendCreate(ActionIndicator entity, ActionIndicatorInputModel model)
    {
        foreach (var _model in model.IncomingMontlyPlans)
        {
            var _entity = _model.Export();

            _entity.IndicatorId = entity.Id;

            actionIndicatorMontlyPlanRepository.Add(_entity, User.Identity.Name, false);
        }
    }

    private void ExtendEdit(ActionIndicator entity, ActionIndicatorInputModel model)
    {
        foreach (var _model in model.OutgoingMontlyPlans)
        {
            var _entity = entity.MontlyPlans.First(bridge => bridge.Id == _model.Id);

            actionIndicatorMontlyPlanRepository.Delete(_entity, User.Identity.Name, false);
        }

        foreach (var _model in model.EditionMontlyPlans)
        {
            var _entity = entity.MontlyPlans.First(bridge => bridge.Id == _model.Id);

            _model.Mergue(_entity);

            actionIndicatorMontlyPlanRepository.Update(_entity, User.Identity.Name, false);
        }

        foreach (var _model in model.IncomingMontlyPlans)
        {
            var _entity = _model.Export();

            _entity.IndicatorId = entity.Id;

            actionIndicatorMontlyPlanRepository.Add(_entity, User.Identity.Name, false);
        }
    }

    private bool IsActionModerable(System.Guid objectiveId, System.Guid proyectionId, System.Guid actionId)
    {
        if (proyectionUserBridgeRepository.Count(bridge => bridge.IsActionsIncluded && bridge.ProwId == proyectionId && bridge.SternId == User.Identity.Name) > 0) return true;

        if (actionUserBridgeRepository.Count(bridge => bridge.ProwId == actionId && bridge.SternId == User.Identity.Name) > 0) return true;

        return false;
    }
}
