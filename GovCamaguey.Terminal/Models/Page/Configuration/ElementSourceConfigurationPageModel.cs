namespace MOROPO.SINERGIAGOB.GovCamaguey.Terminal.Models.Page.Configuration;

public class ElementSourceConfigurationPageModel : Core.Presentation.Terminal.MAUI.Models.Page.Contract.AbstractConfigurationPageModel<Interop.API.Common.Models.Display.ElementSourceDisplayModel>
{
    public static string NameValidableProperty => nameof(Name);

    private string name;
    private string alias;
    private string description;
    private int priority = 1;

    public ElementSourceConfigurationPageModel() : this(null) { }

    public ElementSourceConfigurationPageModel(Interop.API.Common.Models.Display.ElementSourceDisplayModel model) : base(model)
    {
        if (model != null)
        {
            Id = model.Id;
            name = model.Name;
            alias = model.Alias;
            description = model.Description;
            priority = model.Priority;
        }
    }

    public System.Guid Id { get; set; }

    public string Name { get => name; set => SetProperty(ref name, value); }

    public string Alias { get => alias; set => SetProperty(ref alias, value); }

    public string Description { get => description; set => SetProperty(ref description, value); }

    public int Priority { get => priority; set => SetProperty(ref priority, value); }
}