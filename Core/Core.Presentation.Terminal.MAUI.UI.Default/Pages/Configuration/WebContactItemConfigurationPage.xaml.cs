namespace MOROPO.SINERGIAGOB.Core.Presentation.Terminal.MAUI.UI.Default.Pages.Configuration;

public partial class WebContactItemConfigurationPage : UI.Pages.Base.ConfigurationBaseContentPage<Interop.API.Common.Models.Display.WebContactItemDisplayModel, Interop.API.Common.Models.Display.WebContactItemDisplayModel, Models.Page.Configuration.WebContactItemConfigurationPageModel>
{
    public WebContactItemConfigurationPage(System.Action<Interop.API.Common.Models.Display.WebContactItemDisplayModel> successAction, Models.Page.Configuration.WebContactItemConfigurationPageModel model) : base(successAction, model, null)
    {
        InitializeComponent();
    }

    protected override string EntityTypeName => "Sitio de Internet";

    protected override System.Threading.Tasks.Task<Interop.API.Common.Models.Display.WebContactItemDisplayModel> PerformCreationAsync()
    {
        return System.Threading.Tasks.Task.FromResult(new Interop.API.Common.Models.Display.WebContactItemDisplayModel
        {
            Id = System.Guid.NewGuid(),
            Label = ConcreteBindingContext.Label,
            Url = ConcreteBindingContext.Url
        });
    }

    protected override System.Threading.Tasks.Task<Interop.API.Common.Models.Display.WebContactItemDisplayModel> PerformEditionAsync()
    {
        return System.Threading.Tasks.Task.FromResult(new Interop.API.Common.Models.Display.WebContactItemDisplayModel
        {
            Id = ConcreteBindingContext.Id,
            Label = ConcreteBindingContext.Label,
            Url = ConcreteBindingContext.Url
        });
    }

    private void ApplyToolbarItem_Clicked(object sender, System.EventArgs eventArgs)
    {
        OnApply(sender, eventArgs);
    }

    private void CancelToolbarItem_Clicked(object sender, System.EventArgs eventArgs)
    {
        OnCancel(sender, eventArgs);
    }
}