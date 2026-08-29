using MOROPO.SINETGIAGOB.GovCamaguey.Interop.API.Common.Models.Display;
using System.Collections.ObjectModel;
using System.Linq;

namespace MOROPO.SINETGIAGOB.GovCamaguey.Terminal.Models.Page.Configuration
{
    public class ProyectionConfigurationPageModel : Core.Presentation.Terminal.MAUI.Models.Page.Contract.AbstractConfigurationPageModel<Interop.API.Common.Models.Input.ProyectionInputModel>
    {
        public static string NameValidableProperty => nameof(Name);
        public static string IndexValidableProperty => nameof(Index);
        public static string ResponsabilityOrganizationsValidableProperty => nameof(ResponsabilityOrganizations);
        public static string DescriptionValidableProperty => nameof(Description);

        private string name;
        private string description;
        private int index = 1;

        public ProyectionConfigurationPageModel() : this(null) { }

        public ProyectionConfigurationPageModel(Interop.API.Common.Models.Input.ProyectionInputModel model, ProyectionIndicatorDisplayModel[] indicators = null, ActionDisplayModel[] actions = null) : base(model)
        {
            if (model != null)
            {
                Id = model.Id;
                Index = model.Index;
                Name = model.Name;
                Description = model.Description;
                Guidelines = new ObservableCollection<GuidelineDisplayModel>(model.Guidelines);
                ResponsabilityOrganizations = new ObservableCollection<OrganizationDisplayModel>(model.ResponsabilityOrganizations);
                ParticipationOrganizations = new ObservableCollection<OrganizationDisplayModel>(model.ParticipationOrganizations);

                if (indicators != null)
                {
                    Indicators = new ObservableCollection<ProyectionIndicatorDisplayModel>(indicators.OrderBy(_model => _model.Priority));
                }

                if (actions != null)
                {
                    Actions = new ObservableCollection<ActionDisplayModel>(actions.OrderBy(_model => _model.Index));
                }
            }
        }

        public System.Guid Id { get; set; }

        public int Index { get => index; set => SetProperty(ref index, value); }

        public string Name { get => name; set => SetProperty(ref name, value); }

        public string Description { get => description; set => SetProperty(ref description, value); }

        public ObservableCollection<GuidelineDisplayModel> Guidelines { get; set; } = [];

        public ObservableCollection<OrganizationDisplayModel> ResponsabilityOrganizations { get; set; } = [];

        public ObservableCollection<OrganizationDisplayModel> ParticipationOrganizations { get; set; } = [];

        public ObservableCollection<ProyectionIndicatorDisplayModel> Indicators { get; set; } = [];

        public ObservableCollection<ActionDisplayModel> Actions { get; set; } = [];
    }
}
