using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.Framework.Guama.Core.Presentation.Models.View;
using MOROPO.Framework.Guama.Core.Presentation.Web.Security.Attributes;
using MOROPO.Framework.Guama.Security.Personal.Persistence.Entities;
using MOROPO.Framework.Guama.Security.Personal.Presentation.Web.Backend.Models.View;
using MOROPO.SINETGIAGOB.GovCamaguey.Interop.API.Common.Models.Input;
using MOROPO.SINETGIAGOB.GovCamaguey.Persistence.Entities;
using MOROPO.SINETGIAGOB.GovCamaguey.Web.BackOffice.Models.View;
using MOROPO.SINETGIAGOB.GovCamaguey.Web.BackOffice.Models.View.Display;
using System.Linq;

namespace MOROPO.SINETGIAGOB.GovCamaguey.Web.BackOffice.Controllers.Backend
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [RequirePermissions(Framework.Guama.Core.Security.Presentation.Constants.Permissions.ShellManager)]
    [Area("governance")]
    public class ObjectivesController : Framework.Guama.Core.Presentation.Web.Backend.Controllers.Contract.AbstractArchivableEnableableEntityManagementController<Objective, System.Guid, ObjectiveInputModel, ObjectiveDisplayViewModel>
    {
        private readonly IEntityRepository<User, string> userRepository;
        private readonly IBridgeRepository<ObjectiveUserBridge, Objective, System.Guid, User, string> userBridgeRepository;

        public ObjectivesController(IBridgeRepository<ObjectiveUserBridge, Objective, System.Guid, User, string> userBridgeRepository, IEntityRepository<User, string> userRepository, IEntityRepository<Objective, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<ObjectivesController> localizer, ILogger<ObjectivesController> logger) : base(repository, localizer, logger)
        {
            this.userRepository = userRepository;
            this.userBridgeRepository = userBridgeRepository;
        }

        protected override System.Func<Objective, object> GetOrderSelector()
        {
            return entity => entity.Index;
        }

        [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.ObjectiveViewerPermission)]
        public override IActionResult Index(int sheet = 1, int limit = 25)
        {
            return base.Index(sheet, limit);
        }

        public override IActionResult Create()
        {
            return NotFound();
        }

        public override IActionResult Create(ObjectiveInputModel model)
        {
            return NotFound();
        }

        public override IActionResult Edit(System.Guid key)
        {
            return NotFound();
        }

        public override IActionResult Edit(ObjectiveInputModel model)
        {
            return NotFound();
        }

        [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.ObjectiveEditorPermission)]
        public override IActionResult Enable(System.Guid key)
        {
            return base.Enable(key);
        }

        [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.ObjectiveEditorPermission)]
        public override IActionResult Disable(System.Guid key)
        {
            return base.Disable(key);
        }

        [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.ObjectiveEditorPermission)]
        public override IActionResult Archive(System.Guid key)
        {
            return base.Archive(key);
        }

        [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.ObjectiveEditorPermission)]
        public override IActionResult Unarchive(System.Guid key)
        {
            return base.Unarchive(key);
        }

        public override IActionResult Delete(System.Guid key)
        {
            return NotFound();
        }

        [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.ObjectiveRemoverPermission)]
        public override IActionResult Delete(ObjectiveDisplayViewModel model)
        {
            return base.Delete(model);
        }

        #region Moderators Management

        [HttpGet]
        [Route("edit/{key}/moderators")]
        public IActionResult Moderators(System.Guid key)
        {
            try
            {
                var entity = Repository.Get(key);

                var model = new ObjectiveModeratorationViewModel
                {
                    Objective = new ObjectiveDisplayViewModel(),
                    Moderators = userBridgeRepository.FindBy(bridge => bridge.ProwId == entity.Id).ToArray().Select(bridge =>
                    {
                        var _model = new Models.View.Display.Bridge.ObjectiveUserBridgeDisplayModel
                        {
                            User = new UserDisplayViewModel(),
                            IsProyectionsIncluded = bridge.IsProyectionsIncluded,
                            IsActionsIncluded = bridge.IsActionsIncluded
                        };

                        _model.User.Import(bridge.Stern);

                        return _model;
                    }).ToArray()
                };

                model.Objective.Import(entity);

                return View(model);
            }
            catch (System.Exception e)
            {
                logger.LogError(e, e.Message);
            }

            SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

            return GetDefaultActionResult();
        }

        [HttpGet]
        [Route("edit/{key}/moderators/add")]
        public IActionResult AddModerator(System.Guid key)
        {
            try
            {
                var entity = Repository.Get(key);

                var model = new Models.View.Input.Bridge.ObjectiveUserBridgeInputModel
                {
                    ObjectiveId = entity.Id,
                    AvailableUsers = [.. userRepository.GetAll().Select(_entity => new UserDisplayViewModel
                    {
                        Id = _entity.Id,
                        Name = _entity.Identity.Name,
                        LastName = _entity.Identity.LastName
                    })]
                };

                return View(model);
            }
            catch (System.Exception e)
            {
                logger.LogError(e, e.Message);
            }

            SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

            return GetDefaultActionResult();
        }

        [HttpPost]
        [Route("edit/{key}/moderators/add")]
        [ValidateAntiForgeryToken]
        public IActionResult AddModerator(System.Guid key, Models.View.Input.Bridge.ObjectiveUserBridgeInputModel model)
        {
            try
            {
                var entity = Repository.Get(key);

                if (ModelState.IsValid)
                {
                    try
                    {
                        var bridge = new ObjectiveUserBridge
                        {
                            ProwId = entity.Id,
                            SternId = model.UserId,
                            IsProyectionsIncluded = model.IsProyectionsIncluded,
                            IsActionsIncluded = model.IsActionsIncluded
                        };

                        userBridgeRepository.Add(bridge, User.Identity.Name);

                        SetDefaultNotification(NotificationType.Success, GetCreationSuccessMessage());

                        return RedirectToAction("moderators", new { key = entity.Id });
                    }
                    catch (Framework.Guama.Core.Presentation.Web.Frontend.InvalidModelException e)
                    {
                        ModelState.AddModelError(e.ModelKey, e.ErrorMessage);
                    }
                }

                SetDefaultNotification(NotificationType.Danger, GetInvalidMessage());

                model.ObjectiveId = entity.Id;
                model.AvailableUsers = [.. userRepository.GetAll().Select(_entity => new UserDisplayViewModel
                {
                    Id = _entity.Id,
                    Name = _entity.Identity.Name,
                    LastName = _entity.Identity.LastName
                })];

                return View(model);
            }
            catch (System.Exception e)
            {
                logger.LogError(e, e.Message);
            }

            SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

            return GetDefaultActionResult();
        }

        [HttpGet]
        [Route("edit/{key}/moderators/edit/{id}")]
        public IActionResult EditModerator(System.Guid key, string id)
        {
            try
            {
                var entity = Repository.Get(key);

                var bridge = userBridgeRepository.Get(key, id);

                var model = new Models.View.Input.Bridge.ObjectiveUserBridgeInputModel
                {
                    ObjectiveId = entity.Id,
                    UserId = bridge.SternId,
                    IsProyectionsIncluded = bridge.IsProyectionsIncluded,
                    IsActionsIncluded = bridge.IsActionsIncluded
                };

                return View(model);
            }
            catch (System.Exception e)
            {
                logger.LogError(e, e.Message);
            }

            SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

            return GetDefaultActionResult();
        }

        [HttpPost]
        [Route("edit/{key}/moderators/edit")]
        [ValidateAntiForgeryToken]
        public IActionResult EditModerator(System.Guid key, Models.View.Input.Bridge.ObjectiveUserBridgeInputModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var entity = Repository.Get(key);

                    try
                    {
                        var bridge = userBridgeRepository.Get(key, model.UserId);

                        bridge.IsProyectionsIncluded = model.IsProyectionsIncluded;
                        bridge.IsActionsIncluded = model.IsActionsIncluded;

                        userBridgeRepository.Update(bridge, User.Identity.Name);

                        SetDefaultNotification(NotificationType.Success, GetEditionSuccessMessage());

                        return RedirectToAction("moderators", new { key = entity.Id });
                    }
                    catch (Framework.Guama.Core.Presentation.Web.Frontend.InvalidModelException e)
                    {
                        ModelState.AddModelError(e.ModelKey, e.ErrorMessage);
                    }

                    SetDefaultNotification(NotificationType.Danger, GetInvalidMessage());

                    model.ObjectiveId = entity.Id;

                    return View(model);
                }
                catch (System.Exception e)
                {
                    logger.LogError(e, e.Message);
                }
            }

            SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

            return GetDefaultActionResult();
        }

        [HttpPost]
        [Route("edit/{key}/moderators/delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteModerator(System.Guid key, Models.View.Display.Bridge.ObjectiveUserBridgeDisplayModel model)
        {
            try
            {
                var entity = Repository.Get(key);

                var bridge = userBridgeRepository.Get(key, model.User.Id);

                var image = bridge.Stern.Identity.Image;

                userBridgeRepository.Delete(bridge, User.Identity.Name);

                SetDefaultNotification(NotificationType.Success, GetDeletionSuccessMessage());

                return RedirectToAction("moderators", new { key = entity.Id });
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
            {
                logger.LogError(e, e.Message);

                SetDefaultNotification(NotificationType.Warning, GetOnDeletionDependecyConstrainErrorMessage());

                return RedirectToAction("moderators", new { key });
            }
            catch (System.Exception e)
            {
                logger.LogError(e, e.Message);
            }

            SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

            return GetSuccessDeleteActionResult();
        }

        #endregion
    }
}
