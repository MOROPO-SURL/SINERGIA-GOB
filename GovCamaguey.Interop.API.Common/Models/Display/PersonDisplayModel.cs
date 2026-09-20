namespace MOROPO.SINERGIAGOB.GovCamaguey.Interop.API.Common.Models.Display
{
    public class PersonDisplayModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityDisplayModel<GovCamaguey.Persistence.Entities.Person, System.Guid>
    {
        public System.Guid Id { get; set; }

        public int Priority { get; set; }

        public Core.Interop.API.Common.Models.Display.PersonIdentityDisplayModel Identity { get; set; }

        public string FullName => string.IsNullOrEmpty(Identity.LastName) ? Identity.Name : $"{Identity.Name} {Identity.LastName}";

        public virtual void Import(GovCamaguey.Persistence.Entities.Person entity)
        {
            Id = entity.Id;
            Priority = entity.Priority;

            if (entity.Identity != null)
            {
                Identity = new Core.Interop.API.Common.Models.Display.PersonIdentityDisplayModel();

                Identity.Import(entity.Identity);
            }
        }
    }
}
