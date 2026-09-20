using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.Framework.Guama.Core.Presentation.Web.Security.Attributes;
using MOROPO.Framework.Guama.Media.Presentation.Web.Common.Managers.Facade;
using MOROPO.SINERGIAGOB.GovCamaguey.Interop.API.Common.Models.Input;
using MOROPO.SINERGIAGOB.GovCamaguey.Persistence.Entities;
using MOROPO.SINERGIAGOB.GovCamaguey.Web.BackOffice.Models.View.Display;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Web.BackOffice.Controllers.Backend
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [RequirePermissions(Framework.Guama.Core.Security.Presentation.Constants.Permissions.ShellManager)]
    [Area("governance")]
    public class OrganizationsController : Framework.Guama.Core.Presentation.Web.Backend.Controllers.Contract.AbstractArchivableEnableableEntityManagementController<Organization, System.Guid, OrganizationInputModel, OrganizationDisplayViewModel>
    {
        private readonly IEntityRepository<Framework.Guama.Media.Persistence.Entities.Image, System.Guid> imageRepository;
        private readonly IImageFileManagerFacade imageFileManager;

        public OrganizationsController(IImageFileManagerFacade imageFileManager, IEntityRepository<Framework.Guama.Media.Persistence.Entities.Image, System.Guid> imageRepository, IEntityRepository<Organization, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<OrganizationsController> localizer, ILogger<OrganizationsController> logger) : base(repository, localizer, logger)
        {
            this.imageRepository = imageRepository;
            this.imageFileManager = imageFileManager;
        }

        [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.OrganizationViewerPermission)]
        public override IActionResult Index(int sheet = 1, int limit = 25)
        {
            return base.Index(sheet, limit);
        }

        public override IActionResult Create()
        {
            return NotFound();
        }

        public override IActionResult Create(OrganizationInputModel model)
        {
            return NotFound();
        }

        public override IActionResult Edit(System.Guid key)
        {
            return NotFound();
        }

        public override IActionResult Edit(OrganizationInputModel model)
        {
            return NotFound();
        }

        [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.OrganizationEditorPermission)]
        public override IActionResult Enable(System.Guid key)
        {
            return base.Enable(key);
        }

        [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.OrganizationEditorPermission)]
        public override IActionResult Disable(System.Guid key)
        {
            return base.Disable(key);
        }

        [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.OrganizationEditorPermission)]
        public override IActionResult Archive(System.Guid key)
        {
            return base.Archive(key);
        }

        [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.OrganizationEditorPermission)]
        public override IActionResult Unarchive(System.Guid key)
        {
            return base.Unarchive(key);
        }

        public override IActionResult Delete(System.Guid key)
        {
            return NotFound();
        }

        [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.OrganizationRemoverPermission)]
        public override IActionResult Delete(OrganizationDisplayViewModel model)
        {
            return base.Delete(model);
        }

        protected override void PostDelete(Organization entity)
        {
            base.PostDelete(entity);

            if (entity.Identity.Image != null)
            {
                imageRepository.Delete(entity.Identity.Image, User.Identity.Name);

                imageFileManager.DeleteImageFile(entity.Identity.Image);
            }
        }
    }
}
