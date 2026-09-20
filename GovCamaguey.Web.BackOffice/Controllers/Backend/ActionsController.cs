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
public class ActionsController : Framework.Guama.Core.Presentation.Web.Backend.Controllers.Contract.AbstractArchivableEnableableEntityManagementController<Action, System.Guid, ActionInputModel, ActionDisplayViewModel>
{
    private readonly IEntityRepository<User, string> userRepository;
    private readonly IBridgeRepository<ActionUserBridge, Action, System.Guid, User, string> userBridgeRepository;

    public ActionsController(IBridgeRepository<ActionUserBridge, Action, System.Guid, User, string> userBridgeRepository, IEntityRepository<User, string> userRepository, IEntityRepository<Action, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<ActionsController> localizer, ILogger<ActionsController> logger) : base(repository, localizer, logger)
    {
        this.userRepository = userRepository;
        this.userBridgeRepository = userBridgeRepository;
    }

    protected override System.Func<Action, object> GetOrderSelector()
    {
        return entity => entity.Proyection.Objective.Index * 1000000 + entity.Proyection.Index * 1000 + entity.Index;
    }

    [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.ActionViewerPermission)]
    public override IActionResult Index(int sheet = 1, int limit = 25)
    {
        return base.Index(sheet, limit);
    }

    public override IActionResult Create()
    {
        return NotFound();
    }

    public override IActionResult Create(ActionInputModel model)
    {
        return NotFound();
    }

    public override IActionResult Edit(System.Guid key)
    {
        return NotFound();
    }

    public override IActionResult Edit(ActionInputModel model)
    {
        return NotFound();
    }

    [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.ActionEditorPermission)]
    public override IActionResult Enable(System.Guid key)
    {
        return base.Enable(key);
    }

    [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.ActionEditorPermission)]
    public override IActionResult Disable(System.Guid key)
    {
        return base.Disable(key);
    }

    [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.ActionEditorPermission)]
    public override IActionResult Archive(System.Guid key)
    {
        return base.Archive(key);
    }

    [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.ActionEditorPermission)]
    public override IActionResult Unarchive(System.Guid key)
    {
        return base.Unarchive(key);
    }

    public override IActionResult Delete(System.Guid key)
    {
        return NotFound();
    }

    [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.ActionRemoverPermission)]
    public override IActionResult Delete(ActionDisplayViewModel model)
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

            var model = new ActionModeratorsViewModel
            {
                Action = new ActionDisplayViewModel(),
                Moderators = userBridgeRepository.FindBy(bridge => bridge.ProwId == entity.Id).ToArray().Select(bridge =>
                {
                    var _model = new Models.View.Display.Bridge.ActionUserBridgeDisplayModel
                    {
                        User = new Framework.Guama.Security.Personal.Presentation.Web.Backend.Models.View.UserDisplayViewModel()
                    };

                    _model.User.Import(bridge.Stern);

                    return _model;
                }).ToArray()
            };

            model.Action.Import(entity);

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

            var model = new Models.View.Input.Bridge.ActionUserBridgeInputModel
            {
                ActionId = entity.Id,
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
    public IActionResult AddModerator(System.Guid key, Models.View.Input.Bridge.ActionUserBridgeInputModel model)
    {
        try
        {
            var entity = Repository.Get(key);

            if (ModelState.IsValid)
            {
                try
                {
                    var bridge = new ActionUserBridge
                    {
                        ProwId = entity.Id,
                        SternId = model.UserId
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

            model.ActionId = entity.Id;
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

    [HttpPost]
    [Route("edit/{key}/moderators/delete")]
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
