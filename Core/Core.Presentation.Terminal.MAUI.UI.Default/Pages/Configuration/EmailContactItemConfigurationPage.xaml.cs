namespace MOROPO.SINERGIAGOB.Core.Presentation.Terminal.MAUI.UI.Default.Pages.Configuration;

public partial class EmailContactItemConfigurationPage : UI.Pages.Base.ConfigurationBaseContentPage<Interop.API.Common.Models.Display.EmailContactItemDisplayModel, Interop.API.Common.Models.Display.EmailContactItemDisplayModel, Models.Page.Configuration.EmailContactItemConfigurationPageModel>
{
    public EmailContactItemConfigurationPage(System.Action<Interop.API.Common.Models.Display.EmailContactItemDisplayModel> successAction, Models.Page.Configuration.EmailContactItemConfigurationPageModel model) : base(successAction, model, null)
    {
        InitializeComponent();
    }

    protected override string EntityTypeName => "Correo Electrónico";

    protected override System.Threading.Tasks.Task<Interop.API.Common.Models.Display.EmailContactItemDisplayModel> PerformCreationAsync()
    {
        return System.Threading.Tasks.Task.FromResult(new Interop.API.Common.Models.Display.EmailContactItemDisplayModel
        {
            Id = System.Guid.NewGuid(),
            Label = ConcreteBindingContext.Label,
            EmailAddress = ConcreteBindingContext.EmailAddress
        });
    }

    protected override System.Threading.Tasks.Task<Interop.API.Common.Models.Display.EmailContactItemDisplayModel> PerformEditionAsync()
    {
        return System.Threading.Tasks.Task.FromResult(new Interop.API.Common.Models.Display.EmailContactItemDisplayModel
        {
            Id = ConcreteBindingContext.Id,
            Label = ConcreteBindingContext.Label,
            EmailAddress = ConcreteBindingContext.EmailAddress
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