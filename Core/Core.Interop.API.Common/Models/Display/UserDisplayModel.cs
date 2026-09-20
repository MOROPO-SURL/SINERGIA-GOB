using System.Linq;

namespace MOROPO.SINERGIAGOB.Core.Interop.API.Common.Models.Display
{
    public class UserDisplayModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityDisplayModel<Framework.Guama.Security.Personal.Persistence.Entities.User, string>
    {
        public string Id { get; set; }

        public PersonIdentityDisplayModel Identity { get; set; }

        public bool Enabled { get; set; }

        public bool HasFullAccess { get; set; }

        public bool PasswordChangeRequired { get; set; }

        public System.DateTime Created { get; set; }

        public System.DateTime LastSessionDate { get; set; }

        public int FailAuthenticationCount { get; set; }

        public RoleDisplayModel[] Roles { get; set; }

        public void Import(Framework.Guama.Security.Personal.Persistence.Entities.User entity)
        {
            Id = entity.Id;
            Enabled = entity.Enabled;
            HasFullAccess = entity.HasFullAccess;
            PasswordChangeRequired = entity.PasswordChangeRequired;
            Created = entity.Created;
            LastSessionDate = entity.LastSessionDate;
            FailAuthenticationCount = entity.FailAuthenticationCount;
            Roles = entity.RoleBridges.Select(bridge =>
            {
                var model = new RoleDisplayModel();

                model.Import(bridge.Stern);

                return model;
            }).ToArray();

            if (entity.Identity != null)
            {
                Identity = new PersonIdentityDisplayModel();

                Identity.Import(entity.Identity);
            }
        }
    }
}
