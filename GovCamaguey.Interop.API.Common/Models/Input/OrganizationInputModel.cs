using MOROPO.SINERGIAGOB.Core.Interop.API.Common.Models.Display;
using MOROPO.SINERGIAGOB.Core.Interop.API.Common.Models.Input;
using System.Collections.Generic;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Interop.API.Common.Models.Input
{
    public class OrganizationInputModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityInputModel<GovCamaguey.Persistence.Entities.Organization, System.Guid>
    {
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }

        public string Alias { get; set; }

        public string About { get; set; }

        public int Priority { get; set; }

        public ImageInputModel Image { get; set; }

        public List<PhoneContactItemInputModel> EditionPhoneContactItems { get; set; } = new List<PhoneContactItemInputModel>();
        public List<PhoneContactItemInputModel> IncomingPhoneContactItems { get; set; } = new List<PhoneContactItemInputModel>();
        public List<PhoneContactItemDisplayModel> OutgoingPhoneContactItems { get; set; } = new List<PhoneContactItemDisplayModel>();

        public List<EmailContactItemInputModel> EditionEmailContactItems { get; set; } = new List<EmailContactItemInputModel>();
        public List<EmailContactItemInputModel> IncomingEmailContactItems { get; set; } = new List<EmailContactItemInputModel>();
        public List<EmailContactItemDisplayModel> OutgoingEmailContactItems { get; set; } = new List<EmailContactItemDisplayModel>();

        public GovCamaguey.Persistence.Entities.Organization Export()
        {
            var entity = new GovCamaguey.Persistence.Entities.Organization { Id = Id, Enabled = true, Identity = new Framework.Guama.Identity.Persistence.Entities.OrganizationIdentity() };

            Mergue(entity);

            return entity;
        }

        public void Import(GovCamaguey.Persistence.Entities.Organization entity)
        {
            throw new System.NotImplementedException();
        }

        public void Mergue(GovCamaguey.Persistence.Entities.Organization entity)
        {
            entity.Identity.Name = Name;
            entity.Identity.Alias = Alias;
            entity.Identity.About = About;
            entity.Priority = Priority;
        }
    }
}
