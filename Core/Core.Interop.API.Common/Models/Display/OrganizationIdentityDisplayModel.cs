using System.Linq;

namespace MOROPO.SINETGIAGOB.Core.Interop.API.Common.Models.Display
{
    public class OrganizationIdentityDisplayModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityDisplayModel<Framework.Guama.Identity.Persistence.Entities.OrganizationIdentity, System.Guid>
    {
        public System.Guid Id { get; set; }

        public string Name { get; set; }

        public string Alias { get; set; }

        public System.DateTime? BornDate { get; set; }

        public string About { get; set; }

        public ImageDisplayModel Image { get; set; }

        public EmailContactItemDisplayModel[] EmailContacts { get; set; }

        public PhoneContactItemDisplayModel[] PhoneContacts { get; set; }

        public PostalContactItemDisplayModel[] PostalContacts { get; set; }

        public WebContactItemDisplayModel[] WebContacts { get; set; }

        public void Import(Framework.Guama.Identity.Persistence.Entities.OrganizationIdentity entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Alias = entity.Alias;
            BornDate = entity.BornDate;
            About = entity.About;

            EmailContacts = entity.EmailContactItemBridges.Select(bridge =>
            {
                var model = new EmailContactItemDisplayModel();

                model.Import(bridge.Stern);

                return model;
            }).ToArray();
            PhoneContacts = entity.PhoneContactItemBridges.Select(bridge =>
            {
                var model = new PhoneContactItemDisplayModel();

                model.Import(bridge.Stern);

                return model;
            }).ToArray();
            WebContacts = entity.WebSiteContactItemBridges.Select(bridge =>
            {
                var model = new WebContactItemDisplayModel();

                model.Import(bridge.Stern);

                return model;
            }).ToArray();
            PostalContacts = entity.PostalContactItemBridges.Select(bridge =>
            {
                var model = new PostalContactItemDisplayModel();

                model.Import(bridge.Stern);

                return model;
            }).ToArray();

            if (entity.Image != null)
            {
                Image = new ImageDisplayModel();

                Image.Import(entity.Image);
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
