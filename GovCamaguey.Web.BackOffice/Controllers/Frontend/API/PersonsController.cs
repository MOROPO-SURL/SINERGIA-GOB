using Microsoft.AspNetCore.Mvc;
using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.Framework.Guama.Core.Presentation.Models;
using MOROPO.Framework.Guama.Core.Presentation.Web.Security.Attributes;
using MOROPO.Framework.Guama.Identity.Persistence.Entities;
using MOROPO.Framework.Guama.Media.Persistence.Entities;
using MOROPO.Framework.Guama.Media.Presentation.Web.Common.Managers.Facade;
using MOROPO.SINERGIAGOB.GovCamaguey.Interop.API.Common.Models.Display;
using MOROPO.SINERGIAGOB.GovCamaguey.Interop.API.Common.Models.Filter;
using MOROPO.SINERGIAGOB.GovCamaguey.Interop.API.Common.Models.Input;
using MOROPO.SINERGIAGOB.GovCamaguey.Persistence.Entities;
using System.Linq;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Web.BackOffice.Controllers.Frontend.API
{
    [Microsoft.AspNetCore.Authorization.Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/governance/[controller]")]
    [ApiController]
    public class PersonsController : Framework.Guama.Core.Presentation.Web.API.Controllers.Contract.AbstractArchivableEnableableEntityAPIManagementFilteredController<Person, System.Guid, PersonInputModel, PersonDisplayModel, PersonFilterModel>
    {
        private readonly IEntityRepository<EmailContactItem, System.Guid> emailContactRepository;
        private readonly IEntityRepository<PhoneContactItem, System.Guid> phoneContactRepository;
        private readonly IEntityRepository<Image, System.Guid> imageRepository;
        private readonly IImageFileManagerFacade imageFileManager;

        public PersonsController(IImageFileManagerFacade imageFileManager, IEntityRepository<Image, System.Guid> imageRepository, IEntityRepository<PhoneContactItem, System.Guid> phoneContactRepository, IEntityRepository<EmailContactItem, System.Guid> emailContactRepository, IEntityRepository<Person, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<PersonsController> localizer, Microsoft.Extensions.Logging.ILogger<PersonsController> logger) : base(repository, localizer, logger)
        {
            this.emailContactRepository = emailContactRepository;
            this.phoneContactRepository = phoneContactRepository;
            this.imageRepository = imageRepository;
            this.imageFileManager = imageFileManager;
        }

        protected override System.Linq.Expressions.Expression<System.Func<Person, bool>> GetGlobalSelector()
        {
            return entity => !entity.Archived && entity.Enabled;
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

        protected override System.Func<Person, object> GetOrderSelector()
        {
            return entity => entity.Priority;
        }

        protected override PersonDisplayModel EntityToDisplayModel(Person entity)
        {
            var model = base.EntityToDisplayModel(entity);

            if (model.Identity.Image != null)
            {
                model.Identity.Image.Extension = MimeTypes.MimeTypeMap.GetExtension(entity.Identity.Image.ContentType);
            }

            return model;
        }

        [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.PersonViewerPermission)]
        public override ActionResult<PersonDisplayModel[]> Get()
        {
            return base.Get();
        }

        [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.PersonViewerPermission)]
        public override ActionResult<PersonDisplayModel> Get(System.Guid key)
        {
            return base.Get(key);
        }

        [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.PersonViewerPermission)]
        public override ActionResult<PaginationModel<PersonDisplayModel>> GetPaginate(int sheet = 1, int limit = 25)
        {
            return base.GetPaginate(sheet, limit);
        }

        [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.PersonCreatorPermission)]
        public override ActionResult Create([FromBody] PersonInputModel model)
        {
            return base.Create(model);
        }

        [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.PersonEditorPermission)]
        public override ActionResult Edit([FromBody] PersonInputModel model)
        {
            return base.Edit(model);
        }

        [RequirePermissions(Interop.API.Common.Constants.Security.Authorization.PersonRemoverPermission)]
        public override ActionResult Archive(System.Guid key)
        {
            return base.Archive(key);
        }

        protected override void PreCreate(Person entity, PersonInputModel model)
        {
            base.PreCreate(entity, model);

            if (model.Image != null)
            {
                using var imageMemoryStream = new System.IO.MemoryStream(model.Image.ContentData);

                using var imageSkData = SkiaSharp.SKData.Create(imageMemoryStream);

                using var imageSkBitmap = SkiaSharp.SKBitmap.Decode(imageSkData);

                entity.Identity.Image = imageRepository.Add(new Image
                {
                    ContentType = model.Image.ContentType,
                    Length = model.Image.ContentData.LongLength,
                    Width = imageSkBitmap.Width,
                    Height = imageSkBitmap.Height,
                    About = string.Format("Image for Person: {0}", model.Id)
                }, User.Identity.Name, false);

                imageFileManager.SaveImageFileContent(entity.Identity.Image.Id, model.Image.ContentData, model.Image.ContentType);
            }

            foreach (var _model in model.IncomingPhoneContactItems)
            {
                var _entity = phoneContactRepository.Add(new PhoneContactItem
                {
                    Id = System.Guid.NewGuid(),
                    Label = _model.Label,
                    PhoneNumber = _model.PhoneNumber
                }, User.Identity.Name, false);

                entity.Identity.PhoneContactItemBridges.Add(new PersonPhoneContactItemBridge
                {
                    ProwId = entity.Id,
                    SternId = _entity.Id
                });
            }

            foreach (var _model in model.IncomingEmailContactItems)
            {
                var _entity = emailContactRepository.Add(new EmailContactItem
                {
                    Id = System.Guid.NewGuid(),
                    Label = _model.Label,
                    EmailAddress = _model.EmailAddress
                }, User.Identity.Name, false);

                entity.Identity.EmailContactItemBridges.Add(new PersonEmailContactItemBridge
                {
                    ProwId = entity.Id,
                    SternId = _entity.Id
                });
            }
        }

        protected override void PreEdit(Person entity, PersonInputModel model)
        {
            base.PreEdit(entity, model);

            if (model.Image != null)
            {
                using var imageMemoryStream = new System.IO.MemoryStream(model.Image.ContentData);

                using var imageSkData = SkiaSharp.SKData.Create(imageMemoryStream);

                using var imageSkBitmap = SkiaSharp.SKBitmap.Decode(imageSkData);

                entity.Identity.Image = imageRepository.Add(new Image
                {
                    ContentType = model.Image.ContentType,
                    Length = model.Image.ContentData.LongLength,
                    Width = imageSkBitmap.Width,
                    Height = imageSkBitmap.Height,
                    About = string.Format("Image for Person: {0}", model.Id)
                }, User.Identity.Name, false);

                imageFileManager.SaveImageFileContent(entity.Identity.Image.Id, model.Image.ContentData, model.Image.ContentType);
            }

            #region Phone Contact Items

            foreach (var _model in model.OutgoingPhoneContactItems)
            {
                var _entity = entity.Identity.PhoneContactItemBridges.First(bridge => bridge.SternId == _model.Id).Stern;

                phoneContactRepository.Delete(_entity, User.Identity.Name, false);
            }

            foreach (var _model in model.EditionPhoneContactItems)
            {
                var _entity = entity.Identity.PhoneContactItemBridges.First(bridge => bridge.SternId == _model.Id).Stern;

                _entity.Label = _model.Label;
                _entity.PhoneNumber = _model.PhoneNumber;

                phoneContactRepository.Update(_entity, User.Identity.Name, false);
            }

            foreach (var _model in model.IncomingPhoneContactItems)
            {
                var _entity = phoneContactRepository.Add(new PhoneContactItem
                {
                    Id = System.Guid.NewGuid(),
                    Label = _model.Label,
                    PhoneNumber = _model.PhoneNumber
                }, User.Identity.Name, false);

                entity.Identity.PhoneContactItemBridges.Add(new PersonPhoneContactItemBridge
                {
                    ProwId = entity.Id,
                    SternId = _entity.Id
                });
            }

            #endregion

            #region Email Contact Items

            foreach (var _model in model.OutgoingEmailContactItems)
            {
                var _entity = entity.Identity.EmailContactItemBridges.First(bridge => bridge.SternId == _model.Id).Stern;

                emailContactRepository.Delete(_entity, User.Identity.Name, false);
            }

            foreach (var _model in model.EditionEmailContactItems)
            {
                var _entity = entity.Identity.EmailContactItemBridges.First(bridge => bridge.SternId == _model.Id).Stern;

                _entity.Label = _model.Label;
                _entity.EmailAddress = _model.EmailAddress;

                emailContactRepository.Update(_entity, User.Identity.Name, false);
            }

            foreach (var _model in model.IncomingEmailContactItems)
            {
                var _entity = emailContactRepository.Add(new EmailContactItem
                {
                    Id = System.Guid.NewGuid(),
                    Label = _model.Label,
                    EmailAddress = _model.EmailAddress
                }, User.Identity.Name, false);

                entity.Identity.EmailContactItemBridges.Add(new PersonEmailContactItemBridge
                {
                    ProwId = entity.Id,
                    SternId = _entity.Id
                });
            }

            #endregion
        }

        protected override void PostEdit(Person entity, PersonInputModel model)
        {
            base.PostEdit(entity, model);

            try
            {
                if (model.Image != null)
                {
                    var oldLogoImage = imageRepository.Get(model.Image.Id);

                    imageRepository.Delete(oldLogoImage, User.Identity.Name);

                    imageFileManager.DeleteImageFile(oldLogoImage);
                }
            }
            catch { }
        }

        protected override void PostDelete(Person entity)
        {
            base.PostDelete(entity);

            if (entity.Identity.ImageId != null)
            {
                var oldImage = imageRepository.Get(entity.Identity.ImageId.Value);

                imageRepository.Delete(oldImage, User.Identity.Name);

                imageFileManager.DeleteImageFile(oldImage);
            }
        }
    }
}
