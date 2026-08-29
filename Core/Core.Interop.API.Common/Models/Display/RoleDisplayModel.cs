using System.Linq;

namespace MOROPO.SINETGIAGOB.Core.Interop.API.Common.Models.Display
{
	public class RoleDisplayModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityDisplayModel<Framework.Guama.Core.Security.Common.Persistence.Entities.Role, System.Guid>
	{
		public System.Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public PermissionDisplayModel[] Permissions { get; set; }

        public void Import(Framework.Guama.Core.Security.Common.Persistence.Entities.Role entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Description = entity.Description;
            Permissions = entity.PermissionBridges.Select(bridge =>
            {
                var model = new PermissionDisplayModel();

                model.Import(bridge.Stern);

                return model;
            }).ToArray();
        }
    }
}

