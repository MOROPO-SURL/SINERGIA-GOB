using MOROPO.SINERGIAGOB.GovCamaguey.Interop.API.Common.Models.Display;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Terminal.Models.Page.Configuration
{
    public class ProyectionConfigurationPageModel : Core.Presentation.Terminal.MAUI.Models.Page.Contract.AbstractConfigurationPageModel<Interop.API.Common.Models.Input.ProyectionInputModel>
    {
        public static string NameValidableProperty => nameof(Name);
        public static string IndexValidableProperty => nameof(Index);
        public static string ResponsabilityOrganizationsValidableProperty => nameof(ResponsabilityOrganizations);
        public static string DescriptionValidableProperty => nameof(Description);

        private string name;
        private string expectedResult;
        private ElementSourceDisplayModel elementSource;
        private string description;
        private int index = 1;

        public ProyectionConfigurationPageModel(IEnumerable<ElementSourceDisplayModel> availableElementSources) : this(null, availableElementSources) { }

        public ProyectionConfigurationPageModel(Interop.API.Common.Models.Input.ProyectionInputModel model, IEnumerable<ElementSourceDisplayModel> availableElementSources, ProyectionIndicatorDisplayModel[] indicators = null, ActionDisplayModel[] actions = null, ProyectionMonthlyClosureDisplayModel[] monthlyClosures = null) : base(model)
        {
            AvailableElementSources = [.. availableElementSources];

            if (model != null)
            {
                Id = model.Id;
                index = model.Index;
                name = model.Name;
                description = model.Description;
                expectedResult = model.ExpectedResult;

                elementSource = AvailableElementSources.SingleOrDefault(item => item.Id == model.ElementSourceId);

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

                if (monthlyClosures != null)
                {
                    MonthlyClosures = new ObservableCollection<ProyectionMonthlyClosureDisplayModel>(monthlyClosures.OrderBy(_model => _model.At));
                }
            }
        }

        public System.Guid Id { get; set; }

        public int Index { get => index; set => SetProperty(ref index, value); }

        public string Name { get => name; set => SetProperty(ref name, value); }

        public ElementSourceDisplayModel ElementSource { get => elementSource; set => SetProperty(ref elementSource, value); }

        public string ExpectedResult { get => expectedResult; set => SetProperty(ref expectedResult, value); }

        public string Description { get => description; set => SetProperty(ref description, value); }

        public ObservableCollection<GuidelineDisplayModel> Guidelines { get; set; } = [];

        public ObservableCollection<OrganizationDisplayModel> ResponsabilityOrganizations { get; set; } = [];

        public ObservableCollection<OrganizationDisplayModel> ParticipationOrganizations { get; set; } = [];

        public ObservableCollection<ProyectionIndicatorDisplayModel> Indicators { get; set; } = [];

        public ObservableCollection<ActionDisplayModel> Actions { get; set; } = [];

        public ObservableCollection<ProyectionMonthlyClosureDisplayModel> MonthlyClosures { get; set; } = [];

        public ObservableCollection<ElementSourceDisplayModel> AvailableElementSources { get; init; }
    }
}
