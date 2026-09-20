namespace MOROPO.SINERGIAGOB.Core.Presentation.Terminal.MAUI.UI.Default.Pages.Configuration;

public partial class PhoneContactItemConfigurationPage : UI.Pages.Base.ConfigurationBaseContentPage<Interop.API.Common.Models.Display.PhoneContactItemDisplayModel, Interop.API.Common.Models.Display.PhoneContactItemDisplayModel, Models.Page.Configuration.PhoneContactItemConfigurationPageModel>
{
    public PhoneContactItemConfigurationPage(System.Action<Interop.API.Common.Models.Display.PhoneContactItemDisplayModel> successAction, Models.Page.Configuration.PhoneContactItemConfigurationPageModel model) : base(successAction, model, null)
    {
        InitializeComponent();
    }

    protected override string EntityTypeName => "Teléfono";

    protected override System.Threading.Tasks.Task<Interop.API.Common.Models.Display.PhoneContactItemDisplayModel> PerformCreationAsync()
    {
        return System.Threading.Tasks.Task.FromResult(new Interop.API.Common.Models.Display.PhoneContactItemDisplayModel
        {
            Id = System.Guid.NewGuid(),
            Label = ConcreteBindingContext.Label,
            PhoneNumber = ConcreteBindingContext.PhoneNumber
        });
    }

    protected override System.Threading.Tasks.Task<Interop.API.Common.Models.Display.PhoneContactItemDisplayModel> PerformEditionAsync()
    {
        return System.Threading.Tasks.Task.FromResult(new Interop.API.Common.Models.Display.PhoneContactItemDisplayModel
        {
            Id = ConcreteBindingContext.Id,
            Label = ConcreteBindingContext.Label,
            PhoneNumber = ConcreteBindingContext.PhoneNumber
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