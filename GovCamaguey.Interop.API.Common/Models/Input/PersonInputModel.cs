using MOROPO.SINERGIAGOB.Core.Interop.API.Common.Models.Display;
using MOROPO.SINERGIAGOB.Core.Interop.API.Common.Models.Input;
using System.Collections.Generic;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Interop.API.Common.Models.Input
{
    public class PersonInputModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityInputModel<GovCamaguey.Persistence.Entities.Person, System.Guid>
    {
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }

        public string About { get; set; }

        public int Priority { get; set; }

        public ImageInputModel Image { get; set; }

        public string LastName { get; set; }

        public string Alias { get; set; }

        public Framework.Guama.Identity.Persistence.Enumerations.Sex Sex { get; set; }

        public Framework.Guama.Identity.Persistence.Enumerations.CivilStatus CivilStatus { get; set; }

        public List<PostalContactItemInputModel> EditionPostalContactItems { get; set; } = new List<PostalContactItemInputModel>();
        public List<PostalContactItemInputModel> IncomingPostalContactItems { get; set; } = new List<PostalContactItemInputModel>();
        public List<PostalContactItemDisplayModel> OutgoingPostalContactItems { get; set; } = new List<PostalContactItemDisplayModel>();

        public List<PhoneContactItemInputModel> EditionPhoneContactItems { get; set; } = new List<PhoneContactItemInputModel>();
        public List<PhoneContactItemInputModel> IncomingPhoneContactItems { get; set; } = new List<PhoneContactItemInputModel>();
        public List<PhoneContactItemDisplayModel> OutgoingPhoneContactItems { get; set; } = new List<PhoneContactItemDisplayModel>();

        public List<EmailContactItemInputModel> EditionEmailContactItems { get; set; } = new List<EmailContactItemInputModel>();
        public List<EmailContactItemInputModel> IncomingEmailContactItems { get; set; } = new List<EmailContactItemInputModel>();
        public List<EmailContactItemDisplayModel> OutgoingEmailContactItems { get; set; } = new List<EmailContactItemDisplayModel>();

        public List<WebContactItemInputModel> EditionWebContactItems { get; set; } = new List<WebContactItemInputModel>();
        public List<WebContactItemInputModel> IncomingWebContactItems { get; set; } = new List<WebContactItemInputModel>();
        public List<WebContactItemDisplayModel> OutgoingWebContactItems { get; set; } = new List<WebContactItemDisplayModel>();

        public GovCamaguey.Persistence.Entities.Person Export()
        {
            var entity = new GovCamaguey.Persistence.Entities.Person { Id = Id, Enabled = true, Identity = new Framework.Guama.Identity.Persistence.Entities.PersonIdentity() };

            Mergue(entity);

            return entity;
        }

        public void Import(GovCamaguey.Persistence.Entities.Person entity)
        {
            throw new System.NotImplementedException();
        }

        public void Mergue(GovCamaguey.Persistence.Entities.Person entity)
        {
            entity.Identity.Name = Name;
            entity.Identity.About = About;
            entity.Priority = Priority;
            entity.Identity.LastName = LastName;
            entity.Identity.Alias = Alias;
            entity.Identity.Sex = Sex;
            entity.Identity.CivilStatus = CivilStatus;
        }
    }
}
