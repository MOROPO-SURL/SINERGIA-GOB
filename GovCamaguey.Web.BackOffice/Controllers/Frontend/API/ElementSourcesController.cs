using Microsoft.AspNetCore.Mvc;
using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.Framework.Guama.Core.Presentation.Models;
using MOROPO.Framework.Guama.Core.Presentation.Web.Security.Attributes;
using MOROPO.SINERGIAGOB.GovCamaguey.Interop.API.Common.Models.Display;
using MOROPO.SINERGIAGOB.GovCamaguey.Interop.API.Common.Models.Input;
using MOROPO.SINERGIAGOB.GovCamaguey.Persistence.Entities;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Web.BackOffice.Controllers.Frontend.API
{
    [Microsoft.AspNetCore.Authorization.Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/governance/[controller]")]
    [ApiController]
    public class ElementSourcesController : Framework.Guama.Core.Presentation.Web.API.Controllers.Contract.AbstractArchivableEnableableEntityAPIManagementController<ElementSource, System.Guid, ElementSourceInputModel, ElementSourceDisplayModel>
    {
        public ElementSourcesController(IEntityRepository<ElementSource, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<ElementSourcesController> localizer, Microsoft.Extensions.Logging.ILogger<ElementSourcesController> logger)
            : base(repository, localizer, logger)
        {
        }

        protected override System.Linq.Expressions.Expression<System.Func<ElementSource, bool>> GetGlobalSelector()
        {
            return entity => !entity.Archived;
        }

        protected override System.Func<ElementSource, object> GetOrderSelector()
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

        [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.ElementSourceViewerPermission)]
        public override ActionResult<ElementSourceDisplayModel[]> Get()
        {
            return base.Get();
        }

        [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.ElementSourceViewerPermission)]
        public override ActionResult<ElementSourceDisplayModel> Get(System.Guid key)
        {
            return base.Get(key);
        }

        [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.ElementSourceViewerPermission)]
        public override ActionResult<PaginationModel<ElementSourceDisplayModel>> GetPaginate(int sheet = 1, int limit = 25)
        {
            return base.GetPaginate(sheet, limit);
        }

        [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.ElementSourceCreatorPermission)]
        public override ActionResult Create([FromBody] ElementSourceInputModel model)
        {
            return base.Create(model);
        }

        [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.ElementSourceEditorPermission)]
        public override ActionResult Edit([FromBody] ElementSourceInputModel model)
        {
            return base.Edit(model);
        }

        [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.ElementSourceRemoverPermission)]
        public override ActionResult Archive(System.Guid key)
        {
            return base.Archive(key);
        }
    }
}