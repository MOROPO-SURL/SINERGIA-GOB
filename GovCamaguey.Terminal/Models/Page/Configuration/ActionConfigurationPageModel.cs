using MOROPO.SINERGIAGOB.GovCamaguey.Interop.API.Common.Models.Display;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Terminal.Models.Page.Configuration;

public class ActionConfigurationPageModel : Core.Presentation.Terminal.MAUI.Models.Page.Contract.AbstractConfigurationPageModel<Interop.API.Common.Models.Input.ActionInputModel>
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

    public ActionConfigurationPageModel(IEnumerable<ElementSourceDisplayModel> availableElementSources) : this(null, availableElementSources) { }

    public ActionConfigurationPageModel(Interop.API.Common.Models.Input.ActionInputModel model, IEnumerable<ElementSourceDisplayModel> availableElementSources, ActionIndicatorDisplayModel[] indicators = null, ActionMonthlyClosureDisplayModel[] monthlyClosures = null) : base(model)
    {
        AvailableElementSources = [.. availableElementSources];

        if (model != null)
        {
            Id = model.Id;
            Index = model.Index;
            Name = model.Name;
            Description = model.Description;
            expectedResult = model.ExpectedResult;

            elementSource = AvailableElementSources.SingleOrDefault(item => item.Id == model.ElementSourceId);

            ResponsabilityOrganizations = new ObservableCollection<OrganizationDisplayModel>(model.ResponsabilityOrganizations);
            ParticipationOrganizations = new ObservableCollection<OrganizationDisplayModel>(model.ParticipationOrganizations);

            if (indicators != null)
            {
                Indicators = new ObservableCollection<ActionIndicatorDisplayModel>(indicators.OrderBy(_model => _model.Priority));
            }
            
            if (monthlyClosures != null)
            {
                MonthlyClosures = new ObservableCollection<ActionMonthlyClosureDisplayModel>(monthlyClosures.OrderBy(_model => _model.At));
            }
        }
    }

    public System.Guid Id { get; set; }

    public int Index { get => index; set => SetProperty(ref index, value); }

    public string Name { get => name; set => SetProperty(ref name, value); }

    public ElementSourceDisplayModel ElementSource { get => elementSource; set => SetProperty(ref elementSource, value); }

    public string ExpectedResult { get => expectedResult; set => SetProperty(ref expectedResult, value); }

    public string Description { get => description; set => SetProperty(ref description, value); }

    public ObservableCollection<OrganizationDisplayModel> ResponsabilityOrganizations { get; set; } = [];

    public ObservableCollection<OrganizationDisplayModel> ParticipationOrganizations { get; set; } = [];

    public ObservableCollection<ActionIndicatorDisplayModel> Indicators { get; set; } = [];

    public ObservableCollection<ActionMonthlyClosureDisplayModel> MonthlyClosures { get; set; } = [];

    public ObservableCollection<ElementSourceDisplayModel> AvailableElementSources { get; init; }
}
