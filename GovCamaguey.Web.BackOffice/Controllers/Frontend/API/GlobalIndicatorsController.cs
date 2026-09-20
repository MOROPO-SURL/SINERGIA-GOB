using Microsoft.AspNetCore.Mvc;
using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.Framework.Guama.Core.Presentation.Models;
using MOROPO.Framework.Guama.Core.Presentation.Web.Security.Attributes;
using MOROPO.SINERGIAGOB.GovCamaguey.Interop.API.Common.Models.Display;
using MOROPO.SINERGIAGOB.GovCamaguey.Interop.API.Common.Models.Filter;
using MOROPO.SINERGIAGOB.GovCamaguey.Interop.API.Common.Models.Input;
using MOROPO.SINERGIAGOB.GovCamaguey.Persistence.Entities;
using System.Linq;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Web.BackOffice.Controllers.Frontend.API;

[Microsoft.AspNetCore.Authorization.Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
[Route("api/governance/[controller]")]
[ApiController]
public class GlobalIndicatorsController : Framework.Guama.Core.Presentation.Web.API.Controllers.Contract.AbstractArchivableEnableableEntityAPIManagementFilteredController<GlobalIndicator, System.Guid, GlobalIndicatorInputModel, GlobalIndicatorDisplayModel, GlobalIndicatorFilterModel>
{
    private readonly IEntityRepository<GlobalIndicatorMontlyPlan, System.Guid> indicatorMontlyPlanRepository;

    public GlobalIndicatorsController(IEntityRepository<GlobalIndicatorMontlyPlan, System.Guid> indicatorMontlyPlanRepository, IEntityRepository<GlobalIndicator, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<GlobalIndicatorsController> localizer, Microsoft.Extensions.Logging.ILogger<GlobalIndicatorsController> logger) : base(repository, localizer, logger)
    {
        this.indicatorMontlyPlanRepository = indicatorMontlyPlanRepository;
    }

    protected override System.Linq.Expressions.Expression<System.Func<GlobalIndicator, bool>> GetGlobalSelector()
    {
        return entity => !entity.Archived;
    }

    protected override System.Func<GlobalIndicator, object> GetOrderSelector()
    {
        return entity => entity.Priority;
    }

    public override ActionResult Delete(System.Guid key)
    {
        return NotFound();
    }

    public override ActionResult Enable(System.Guid key)
    {
        return NotFound(key);
    }

    public override ActionResult Disable(System.Guid key)
    {
        return NotFound();
    }

    [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.GlobalIndicatorViewerPermission)]
    public override ActionResult<GlobalIndicatorDisplayModel[]> Get()
    {
        return base.Get();
    }

    [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.GlobalIndicatorViewerPermission)]
    public override ActionResult<GlobalIndicatorDisplayModel> Get(System.Guid key)
    {
        return base.Get(key);
    }

    [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.GlobalIndicatorViewerPermission)]
    public override ActionResult<PaginationModel<GlobalIndicatorDisplayModel>> GetPaginate(int sheet = 1, int limit = 25)
    {
        return base.GetPaginate(sheet, limit);
    }

    [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.GlobalIndicatorCreatorPermission)]
    public override ActionResult Create([FromBody] GlobalIndicatorInputModel model)
    {
        return base.Create(model);
    }

    [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.GlobalIndicatorEditorPermission)]
    public override ActionResult Edit([FromBody] GlobalIndicatorInputModel model)
    {
        return base.Edit(model);
    }

    [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.GlobalIndicatorRemoverPermission)]
    public override ActionResult Archive(System.Guid key)
    {
        return base.Archive(key);
    }

    protected override void PreCreate(GlobalIndicator entity, GlobalIndicatorInputModel model)
    {
        foreach (var _model in model.IncomingMontlyPlans)
        {
            var _entity = _model.Export();

            _entity.IndicatorId = entity.Id;

            indicatorMontlyPlanRepository.Add(_entity, User.Identity.Name, false);
        }
    }

    protected override void PreEdit(GlobalIndicator entity, GlobalIndicatorInputModel model)
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