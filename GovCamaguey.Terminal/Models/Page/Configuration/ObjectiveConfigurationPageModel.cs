using System.Collections.ObjectModel;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Terminal.Models.Page.Configuration;

public class ObjectiveConfigurationPageModel : Core.Presentation.Terminal.MAUI.Models.Page.Contract.AbstractConfigurationPageModel<Interop.API.Common.Models.Display.ObjectiveDisplayModel>
{
    public static string NameValidableProperty => nameof(Name);
    public static string IndexValidableProperty => nameof(Index);
    public static string ResponsibleValidableProperty => nameof(Responsible);
    public static string DescriptionValidableProperty => nameof(Description);

    private string name;
    private string description;
    private int index = 1;
    private Interop.API.Common.Models.Display.PersonDisplayModel responsible;

    public ObjectiveConfigurationPageModel() : this(null) { }

    public ObjectiveConfigurationPageModel(Interop.API.Common.Models.Display.ObjectiveDisplayModel model) : base(model)
    {
        if (model != null)
        {
            Id = model.Id;
            Index = model.Index;
            Name = model.Name;
            Description = model.Description;
            Responsible = model.Responsible;
            Indicators = [.. model.Indicators];
            Proyections = [.. model.Proyections];
        }
    }

    public System.Guid Id { get; set; }

    public int Index { get => index; set => SetProperty(ref index, value); }

    public string Name { get => name; set => SetProperty(ref name, value); }

    public string Description { get => description; set => SetProperty(ref description, value); }

    public Interop.API.Common.Models.Display.PersonDisplayModel Responsible { get => responsible; set => SetProperty(ref responsible, value); }

    public ObservableCollection<Interop.API.Common.Models.Display.ObjectiveIndicatorDisplayModel> Indicators { get; set; } = [];

    public System.Collections.ObjectModel.ObservableCollection<Interop.API.Common.Models.Display.ProyectionDisplayModel> Proyections { get; set; } = [];
}
