using MOROPO.SINERGIAGOB.Core.Interop.API.Common.Models.Display;
using MOROPO.SINERGIAGOB.Core.Presentation.Terminal.MAUI.Extensions;
using MOROPO.Framework.Guama.Identity.Persistence.Enumerations;
using MOROPO.SINERGIAGOB.GovCamaguey.Interop.API.Common.Models.Display;
using System.Collections.ObjectModel;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Terminal.Models.Page.Configuration
{
    public class PersonConfigurationPageModel : Core.Presentation.Terminal.MAUI.Models.Page.Contract.AbstractConfigurationPageModel<PersonDisplayModel>
    {
        public static string NameValidableProperty => nameof(Name);
        public static string SexValidableProperty => nameof(Sex);

        private string name;
        private string lastName;
        private string alias;
        private string about;
        private Sex sex;
        private int priority = 1;
        private Microsoft.Maui.Controls.ImageSource imageSource;

        public PersonConfigurationPageModel() : this(null) { }

        public PersonConfigurationPageModel(PersonDisplayModel model) : base(model)
        {
            if (model != null)
            {
                Id = model.Id;
                Name = model.Identity.Name;
                LastName = model.Identity.LastName;
                Alias = model.Identity.Alias;
                About = model.Identity.About;
                Sex = model.Identity.Sex;
                Priority = model.Priority;

                foreach (var item in model.Identity.EmailContacts) EmailContactItems.Add(item);

                foreach (var item in model.Identity.PhoneContacts) PhoneContactItems.Add(item);

                if (model.Identity.Image != null)
                {
                    ImageId = model.Identity.Image.Id;
                    ImageSource = model.Identity.Image.ToImageSource();
                }
            }
        }

        public System.Guid Id { get; set; }

        public System.Guid? ImageId { get; set; }

        public string Name { get => name; set => SetProperty(ref name, value); }

        public string LastName { get => lastName; set => SetProperty(ref lastName, value); }

        public string Alias { get => alias; set => SetProperty(ref alias, value); }

        public string About { get => about; set => SetProperty(ref about, value); }

        public Sex Sex { get => sex; set => SetProperty(ref sex, value); }

        public int Priority { get => priority; set => SetProperty(ref priority, value); }

        public Microsoft.Maui.Controls.ImageSource ImageSource { get => imageSource; set => SetProperty(ref imageSource, value); }

        public ObservableCollection<EmailContactItemDisplayModel> EmailContactItems { get; set; } = [];

        public ObservableCollection<PhoneContactItemDisplayModel> PhoneContactItems { get; set; } = [];

        public Sex[] AvailableSexs { get; private set; } = [Sex.Unknown, Sex.Male, Sex.Famale];
    }
}
