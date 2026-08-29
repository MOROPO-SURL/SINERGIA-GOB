using Microsoft.Extensions.DependencyInjection;

namespace MOROPO.SINETGIAGOB.Core.Presentation.Terminal.MAUI.UI.Default.Pages.Configuration;

public partial class PostalContactConfigurationPage : UI.Pages.Base.ConfigurationBaseContentPage<Interop.API.Common.Models.Display.PostalContactItemDisplayModel, Interop.API.Common.Models.Display.PostalContactItemDisplayModel, Models.Page.Configuration.PostalContactConfigurationPageModel>
{
    public PostalContactConfigurationPage(System.Action<Interop.API.Common.Models.Display.PostalContactItemDisplayModel> successAction, Models.Page.Configuration.PostalContactConfigurationPageModel model) : base(successAction, model, null)
    {
        IsEnabled = false;

        InitializeComponent();

        if (model.Municipality != null)
        {
            for (int i = 0; i < model.AvailableStates.Length; i++)
            {
                var current = model.AvailableStates[i];

                if (model.Municipality.State.Id != current.Id) continue;

                StatePicker.SelectedIndex = i;

                break;
            }

            for (int i = 0; i < model.AvailableMunicipalities.Count; i++)
            {
                var current = model.AvailableMunicipalities[i];

                if (model.Municipality.Id != current.Id) continue;

                MunicipalityPicker.SelectedIndex = i;

                break;
            }
        }

        IsEnabled = true;
    }

    protected override string EntityTypeName => "Dirección Postal";

    protected override System.Threading.Tasks.Task<Interop.API.Common.Models.Display.PostalContactItemDisplayModel> PerformCreationAsync()
    {
        return System.Threading.Tasks.Task.FromResult(new Interop.API.Common.Models.Display.PostalContactItemDisplayModel
        {
            Id = System.Guid.NewGuid(),
            Label = ConcreteBindingContext.Label,
            Address = ConcreteBindingContext.Address,
            ZipCode = ConcreteBindingContext.ZipCode,
            Municipality = ConcreteBindingContext.AvailableMunicipalities[MunicipalityPicker.SelectedIndex],
            Latitude = ConcreteBindingContext.Latitude,
            Longitude = ConcreteBindingContext.Longitude
        });
    }

    protected override System.Threading.Tasks.Task<Interop.API.Common.Models.Display.PostalContactItemDisplayModel> PerformEditionAsync()
    {
        return System.Threading.Tasks.Task.FromResult(new Interop.API.Common.Models.Display.PostalContactItemDisplayModel
        {
            Id = ConcreteBindingContext.Id,
            Label = ConcreteBindingContext.Label,
            Address = ConcreteBindingContext.Address,
            ZipCode = ConcreteBindingContext.ZipCode,
            Municipality = ConcreteBindingContext.AvailableMunicipalities[MunicipalityPicker.SelectedIndex],
            Latitude = ConcreteBindingContext.Latitude,
            Longitude = ConcreteBindingContext.Longitude
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

    private void StatePicker_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        if (!IsEnabled || ConcreteBindingContext == null) return;

        var selectedState = ConcreteBindingContext.AvailableStates[StatePicker.SelectedIndex];

        var municipalitiesAPIConnectClient = new Interop.API.Connect.Clients.MunicipalitiesAPIConnectClient(Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Framework.Guama.Core.Presentation.API.Connect.Providers.Facade.IHttpClientHelperProviderFacade>());

        ConcreteBindingContext.IsBusy = true;

        municipalitiesAPIConnectClient.FilterAsync(new Interop.API.Common.Models.Filter.MunicipalityFilterModel { StateId = selectedState.Id }).ContinueWith(task =>
        {
            if (task.IsCanceled) return;

            Dispatcher.Dispatch(() =>
            {
                ConcreteBindingContext.AvailableMunicipalities.Clear();

                foreach (var item in task.Result)
                {
                    ConcreteBindingContext.AvailableMunicipalities.Add(item);
                }

                ConcreteBindingContext.IsBusy = false;
            });
        });
    }

    private void MunicipalityPicker_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        hasChangesFlag = true;
    }
}