using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOROPO.Framework.Guama.Core.Presentation.Web.Security.Attributes;
using MOROPO.SINERGIAGOB.GovCamaguey.Interop.API.Common.Models.Input;
using MOROPO.SINERGIAGOB.GovCamaguey.Persistence.Entities;
using MOROPO.SINERGIAGOB.GovCamaguey.Web.BackOffice.Models.View.Display;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Web.BackOffice.Controllers.Backend
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [RequirePermissions(Framework.Guama.Core.Security.Presentation.Constants.Permissions.ShellManager)]
    [Area("governance")]
    public class ActionIndicatorResultsController : Framework.Guama.Core.Presentation.Web.Backend.Controllers.Contract.AbstractEntityManagementController<ActionIndicatorResult, long, ActionIndicatorResultInputModel, ActionIndicatorResultDisplayViewModel>
    {
        public ActionIndicatorResultsController(Framework.Guama.Core.Persistence.Repositories.IEntityRepository<ActionIndicatorResult, long> repository, Microsoft.Extensions.Localization.IStringLocalizer<ActionIndicatorResultsController> localizer, ILogger<ActionIndicatorResultsController> logger) : base(repository, localizer, logger)
        {
        }

        [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.ActionIndicatorResultViewerPermission)]
        public override IActionResult Index(int sheet = 1, int limit = 25)
        {
            return base.Index(sheet, limit);
        }

        public override IActionResult Create()
        {
            return NotFound();
        }

        public override IActionResult Create(ActionIndicatorResultInputModel model)
        {
            return NotFound();
        }

        public override IActionResult Edit(long key)
        {
            return NotFound();
        }

        public override IActionResult Edit(ActionIndicatorResultInputModel model)
        {
            return NotFound();
        }

        public override IActionResult Delete(long key)
        {
            return NotFound();
        }

        [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.ActionIndicatorResultRemoverPermission)]
        public override IActionResult Delete(ActionIndicatorResultDisplayViewModel model)
        {
            return base.Delete(model);
        }
    }
}
