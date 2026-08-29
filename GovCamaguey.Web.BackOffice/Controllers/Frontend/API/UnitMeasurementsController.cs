using Microsoft.AspNetCore.Mvc;
using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.Framework.Guama.Core.Presentation.Models;
using MOROPO.Framework.Guama.Core.Presentation.Web.Security.Attributes;
using MOROPO.SINETGIAGOB.GovCamaguey.Interop.API.Common.Models.Display;
using MOROPO.SINETGIAGOB.GovCamaguey.Interop.API.Common.Models.Input;
using MOROPO.SINETGIAGOB.GovCamaguey.Persistence.Entities;

namespace MOROPO.SINETGIAGOB.GovCamaguey.Web.BackOffice.Controllers.Frontend.API
{
    [Microsoft.AspNetCore.Authorization.Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/governance/[controller]")]
    [ApiController]
    public class UnitMeasurementsController : Framework.Guama.Core.Presentation.Web.API.Controllers.Contract.AbstractArchivableEnableableEntityAPIManagementController<UnitMeasurement, System.Guid, UnitMeasurementInputModel, UnitMeasurementDisplayModel>
    {
        public UnitMeasurementsController(IEntityRepository<UnitMeasurement, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<UnitMeasurementsController> localizer, Microsoft.Extensions.Logging.ILogger<UnitMeasurementsController> logger)
            : base(repository, localizer, logger)
        {
        }

        protected override System.Linq.Expressions.Expression<System.Func<UnitMeasurement, bool>> GetGlobalSelector()
        {
            return entity => !entity.Archived;
        }

        protected override System.Func<UnitMeasurement, object> GetOrderSelector()
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

        [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.UnitMeasurementViewerPermission)]
        public override ActionResult<UnitMeasurementDisplayModel[]> Get()
        {
            return base.Get();
        }

        [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.UnitMeasurementViewerPermission)]
        public override ActionResult<UnitMeasurementDisplayModel> Get(System.Guid key)
        {
            return base.Get(key);
        }

        [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.UnitMeasurementViewerPermission)]
        public override ActionResult<PaginationModel<UnitMeasurementDisplayModel>> GetPaginate(int sheet = 1, int limit = 25)
        {
            return base.GetPaginate(sheet, limit);
        }

        [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.UnitMeasurementCreatorPermission)]
        public override ActionResult Create([FromBody] UnitMeasurementInputModel model)
        {
            return base.Create(model);
        }

        [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.UnitMeasurementEditorPermission)]
        public override ActionResult Edit([FromBody] UnitMeasurementInputModel model)
        {
            return base.Edit(model);
        }

        [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.UnitMeasurementRemoverPermission)]
        public override ActionResult Archive(System.Guid key)
        {
            return base.Archive(key);
        }
    }
}