namespace MOROPO.SINETGIAGOB.Core.Interop.API.Common.Models.Display
{
    public class PhoneContactItemDisplayModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityDisplayModel<Framework.Guama.Identity.Persistence.Entities.PhoneContactItem, System.Guid>
    {
        public System.Guid Id { get; set; }

        public string Label { get; set; }

        public string PhoneNumber { get; set; }

        public void Import(Framework.Guama.Identity.Persistence.Entities.PhoneContactItem entity)
        {
            Id = entity.Id;
            Label = entity.Label;
            PhoneNumber = entity.PhoneNumber;
        }
    }
}
