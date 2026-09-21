using Microsoft.Extensions.DependencyInjection;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Terminal.UI.Pages.Configuration;

public partial class ElementSourceConfigurationPage : Core.Presentation.Terminal.MAUI.UI.Pages.Base.ConfigurationBaseContentPage<Interop.API.Common.Models.Display.ElementSourceDisplayModel, Interop.API.Common.Models.Display.ElementSourceDisplayModel, Models.Page.Configuration.ElementSourceConfigurationPageModel>
{
    public ElementSourceConfigurationPage(System.Action<Interop.API.Common.Models.Display.ElementSourceDisplayModel> successAction, Models.Page.Configuration.ElementSourceConfigurationPageModel model) : base(successAction, model, new Validators.ElementSourceConfigurationValidator())
    {
        InitializeComponent();
    }

    protected override string EntityTypeName => "Origen";

    protected override async System.Threading.Tasks.Task<Interop.API.Common.Models.Display.ElementSourceDisplayModel> PerformCreationAsync()
    {
        var apiConnectClient = new Interop.API.Connect.Clients.ElementSourcesAPIConnectClient(Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Framework.Guama.Core.Presentation.API.Connect.Providers.Facade.IHttpClientHelperProviderFacade>());

        var model = new Interop.API.Common.Models.Input.ElementSourceInputModel
        {
            Id = System.Guid.NewGuid(),
            Name = ConcreteBindingContext.Name,
            Alias = ConcreteBindingContext.Alias,
            Priority = ConcreteBindingContext.Priority
        };

        await apiConnectClient.CreateAsync(model);

        return await apiConnectClient.GetAsync(model.Id);
    }

    protected override async System.Threading.Tasks.Task<Interop.API.Common.Models.Display.ElementSourceDisplayModel> PerformEditionAsync()
    {
        var apiConnectClient = new Interop.API.Connect.Clients.ElementSourcesAPIConnectClient(Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Framework.Guama.Core.Presentation.API.Connect.Providers.Facade.IHttpClientHelperProviderFacade>());

        var model = new Interop.API.Common.Models.Input.ElementSourceInputModel
        {
            Id = ConcreteBindingContext.Id,
            Name = ConcreteBindingContext.Name,
            Alias = ConcreteBindingContext.Alias,
            Priority = ConcreteBindingContext.Priority
        };

        await apiConnectClient.EditAsync(model);

        return await apiConnectClient.GetAsync(model.Id);
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