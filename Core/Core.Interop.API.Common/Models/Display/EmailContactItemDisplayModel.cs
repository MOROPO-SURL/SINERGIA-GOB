namespace MOROPO.SINETGIAGOB.Core.Interop.API.Common.Models.Display
{
    public class EmailContactItemDisplayModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityDisplayModel<Framework.Guama.Identity.Persistence.Entities.EmailContactItem, System.Guid>
    {
        public System.Guid Id { get; set; }

        public string Label { get; set; }

        public string EmailAddress { get; set; }

        public void Import(Framework.Guama.Identity.Persistence.Entities.EmailContactItem entity)
        {
            Id = entity.Id;
            Label = entity.Label;
            EmailAddress = entity.EmailAddress;
        }
    }
}
