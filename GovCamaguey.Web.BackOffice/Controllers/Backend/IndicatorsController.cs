using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOROPO.Framework.Guama.Core.Presentation.Web.Security.Attributes;
using MOROPO.SINETGIAGOB.GovCamaguey.Interop.API.Common.Models.Input;
using MOROPO.SINETGIAGOB.GovCamaguey.Persistence.Entities;
using MOROPO.SINETGIAGOB.GovCamaguey.Web.BackOffice.Models.View.Display;

namespace MOROPO.SINETGIAGOB.GovCamaguey.Web.BackOffice.Controllers.Backend
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [RequirePermissions(Framework.Guama.Core.Security.Presentation.Constants.Permissions.ShellManager)]
    [Area("governance")]
    public class IndicatorsController : Framework.Guama.Core.Presentation.Web.Backend.Controllers.Contract.AbstractArchivableEnableableEntityManagementController<ActionIndicator, System.Guid, ActionIndicatorInputModel, IndicatorDisplayViewModel>
    {
        public IndicatorsController(Framework.Guama.Core.Persistence.Repositories.IEntityRepository<ActionIndicator, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<IndicatorsController> localizer, ILogger<IndicatorsController> logger) : base(repository, localizer, logger)
        {
        }

        [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.ActionIndicatorViewerPermission)]
        public override IActionResult Index(int sheet = 1, int limit = 25)
        {
            return base.Index(sheet, limit);
        }

        public override IActionResult Create()
        {
            return NotFound();
        }

        public override IActionResult Create(ActionIndicatorInputModel model)
        {
            return NotFound();
        }

        public override IActionResult Edit(System.Guid key)
        {
            return NotFound();
        }

        public override IActionResult Edit(ActionIndicatorInputModel model)
        {
            return NotFound();
        }

        [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.ActionIndicatorEditorPermission)]
        public override IActionResult Enable(System.Guid key)
        {
            return base.Enable(key);
        }

        [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.ActionIndicatorEditorPermission)]
        public override IActionResult Disable(System.Guid key)
        {
            return base.Disable(key);
        }

        [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.ActionIndicatorEditorPermission)]
        public override IActionResult Archive(System.Guid key)
        {
            return base.Archive(key);
        }

        [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.ActionIndicatorEditorPermission)]
        public override IActionResult Unarchive(System.Guid key)
        {
            return base.Unarchive(key);
        }

        public override IActionResult Delete(System.Guid key)
        {
            return NotFound();
        }

        [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.ActionIndicatorRemoverPermission)]
        public override IActionResult Delete(IndicatorDisplayViewModel model)
        {
            return base.Delete(model);
        }
    }
}
