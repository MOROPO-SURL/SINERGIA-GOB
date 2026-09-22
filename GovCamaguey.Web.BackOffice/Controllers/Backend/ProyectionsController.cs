using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.Framework.Guama.Core.Presentation.Models.View;
using MOROPO.Framework.Guama.Core.Presentation.Web.Security.Attributes;
using MOROPO.Framework.Guama.Security.Personal.Persistence.Entities;
using MOROPO.SINERGIAGOB.GovCamaguey.Interop.API.Common.Models.Input;
using MOROPO.SINERGIAGOB.GovCamaguey.Persistence.Entities;
using MOROPO.SINERGIAGOB.GovCamaguey.Web.BackOffice.Models.View;
using MOROPO.SINERGIAGOB.GovCamaguey.Web.BackOffice.Models.View.Display;
using System.Linq;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Web.BackOffice.Controllers.Backend;

[Microsoft.AspNetCore.Authorization.Authorize]
[RequirePermissions(Framework.Guama.Core.Security.Presentation.Constants.Permissions.ShellManager)]
[Area("governance")]
public class ProyectionsController : Framework.Guama.Core.Presentation.Web.Backend.Controllers.Contract.AbstractArchivableEnableableEntityManagementController<Proyection, System.Guid, ProyectionInputModel, ProyectionDisplayViewModel>
{
    private readonly IEntityRepository<User, string> userRepository;
    private readonly IBridgeRepository<ProyectionUserBridge, Proyection, System.Guid, User, string> userBridgeRepository;

    public ProyectionsController(IBridgeRepository<ProyectionUserBridge, Proyection, System.Guid, User, string> userBridgeRepository, IEntityRepository<User, string> userRepository, IEntityRepository<Proyection, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<ProyectionsController> localizer, ILogger<ProyectionsController> logger) : base(repository, localizer, logger)
    {
        this.userRepository = userRepository;
        this.userBridgeRepository = userBridgeRepository;
    }

    protected override System.Func<Proyection, object> GetOrderSelector()
    {
        return entity => entity.Objective.Index * 1000 + entity.Index;
    }

    [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.ProyectionViewerPermission)]
    public override IActionResult Index(int sheet = 1, int limit = 25)
    {
        return base.Index(sheet, limit);
    }

    public override IActionResult Create()
    {
        return NotFound();
    }

    public override IActionResult Create(ProyectionInputModel model)
    {
        return NotFound();
    }

    public override IActionResult Edit(System.Guid key)
    {
        return NotFound();
    }

    public override IActionResult Edit(ProyectionInputModel model)
    {
        return NotFound();
    }

    [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.ProyectionEditorPermission)]
    public override IActionResult Enable(System.Guid key)
    {
        return base.Enable(key);
    }

    [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.ProyectionEditorPermission)]
    public override IActionResult Disable(System.Guid key)
    {
        return base.Disable(key);
    }

    [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.ProyectionEditorPermission)]
    public override IActionResult Archive(System.Guid key)
    {
        return base.Archive(key);
    }

    [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.ProyectionEditorPermission)]
    public override IActionResult Unarchive(System.Guid key)
    {
        return base.Unarchive(key);
    }

    public override IActionResult Delete(System.Guid key)
    {
        return NotFound();
    }

    [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.ProyectionRemoverPermission)]
    public override IActionResult Delete(ProyectionDisplayViewModel model)
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

            var model = new ProyectionModeratorsViewModel
            {
                Proyection = new ProyectionDisplayViewModel(),
                Moderators = userBridgeRepository.FindBy(bridge => bridge.ProwId == entity.Id).ToArray().Select(bridge =>
                {
                    var _model = new Models.View.Display.Bridge.ProyectionUserBridgeDisplayModel
                    {
                        User = new Framework.Guama.Security.Personal.Presentation.Web.Backend.Models.View.UserDisplayViewModel(),
                        IsActionsIncluded = bridge.IsActionsIncluded
                    };

                    _model.User.Import(bridge.Stern);

                    return _model;
                }).ToArray()
            };

            model.Proyection.Import(entity);

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

            var model = new Models.View.Input.Bridge.ProyectionUserBridgeInputModel
            {
                ProyectionId = entity.Id,
                AvailableUsers = [.. userRepository.GetAll().Select(_entity => new Framework.Guama.Security.Personal.Presentation.Web.Backend.Models.View.UserDisplayViewModel
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
    public IActionResult AddModerator(System.Guid key, Models.View.Input.Bridge.ProyectionUserBridgeInputModel model)
    {
        try
        {
            var entity = Repository.Get(key);

            if (ModelState.IsValid)
            {
                try
                {
                    var bridge = new ProyectionUserBridge
                    {
                        ProwId = entity.Id,
                        SternId = model.UserId,
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

            model.ProyectionId = entity.Id;
            model.AvailableUsers = [.. userRepository.GetAll().Select(_entity => new Framework.Guama.Security.Personal.Presentation.Web.Backend.Models.View.UserDisplayViewModel
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

            var model = new Models.View.Input.Bridge.ProyectionUserBridgeInputModel
            {
                ProyectionId = entity.Id,
                UserId = bridge.SternId,
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
    public IActionResult EditModerator(System.Guid key, Models.View.Input.Bridge.ProyectionUserBridgeInputModel model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var entity = Repository.Get(key);

                try
                {
                    var bridge = userBridgeRepository.Get(key, model.UserId);

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

                model.ProyectionId = entity.Id;

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
    [Route("edit/{key}/moderators/delete/{id}")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteModerator(System.Guid key, string id)
    {
        try
        {
            var entity = Repository.Get(key);

            var bridge = userBridgeRepository.Get(key, id);

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
