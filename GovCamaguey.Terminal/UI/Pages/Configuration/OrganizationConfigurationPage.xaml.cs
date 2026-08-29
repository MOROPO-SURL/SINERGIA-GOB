using Microsoft.Extensions.DependencyInjection;
using MOROPO.Framework.Caney.MVVM.Core.Extensions;
using MOROPO.SINETGIAGOB.Core.Interop.API.Common.Models.Display;
using MOROPO.SINETGIAGOB.Core.Interop.API.Common.Models.Input;
using MOROPO.SINETGIAGOB.GovCamaguey.Interop.API.Common.Models.Display;
using System.Collections.Generic;
using System.Linq;

namespace MOROPO.SINETGIAGOB.GovCamaguey.Terminal.UI.Pages.Configuration;

public partial class OrganizationConfigurationPage : Core.Presentation.Terminal.MAUI.UI.Pages.Base.ConfigurationBaseContentPage<OrganizationDisplayModel, OrganizationDisplayModel, Models.Page.Configuration.OrganizationConfigurationPageModel>
{
    private ImageInputModel imagePickedModel;

    private readonly List<PhoneContactItemInputModel> editionPhoneContactItems = [];
    private readonly List<PhoneContactItemInputModel> incomingPhoneContactItems = [];
    private readonly List<PhoneContactItemDisplayModel> outgoingPhoneContactItems = [];

    private readonly List<EmailContactItemInputModel> editionEmailContactItems = [];
    private readonly List<EmailContactItemInputModel> incomingEmailContactItems = [];
    private readonly List<EmailContactItemDisplayModel> outgoingEmailContactItems = [];

    public OrganizationConfigurationPage(System.Action<OrganizationDisplayModel> successAction, Models.Page.Configuration.OrganizationConfigurationPageModel model) : base(successAction, model, new Validators.OrganizationConfigurationValidator())
    {
        InitializeComponent();
    }

    protected override string EntityTypeName => "Organismo";

    protected override void DisposeUnmanagementResources()
    {
        base.DisposeUnmanagementResources();

        imagePickedModel = null;
    }

    protected override async System.Threading.Tasks.Task<OrganizationDisplayModel> PerformCreationAsync()
    {
        var apiConnectClient = new Interop.API.Connect.Clients.OrganizationsAPIConnectClient(Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Framework.Guama.Core.Presentation.API.Connect.Providers.Facade.IHttpClientHelperProviderFacade>());

        var model = new Interop.API.Common.Models.Input.OrganizationInputModel
        {
            Id = System.Guid.NewGuid(),
            Name = ConcreteBindingContext.Name,
            Alias = ConcreteBindingContext.Alias,
            About = ConcreteBindingContext.About,
            Priority = ConcreteBindingContext.Priority,
            IncomingPhoneContactItems = incomingPhoneContactItems,
            IncomingEmailContactItems = incomingEmailContactItems
        };

        if (imagePickedModel != null)
        {
            imagePickedModel.Id = System.Guid.NewGuid();
            imagePickedModel.About = $"{EntityTypeName} {model.Id}";

            model.Image = imagePickedModel;
        }

        await apiConnectClient.CreateAsync(model);

        return await apiConnectClient.GetAsync(model.Id);
    }

    protected override async System.Threading.Tasks.Task<OrganizationDisplayModel> PerformEditionAsync()
    {
        var apiConnectClient = new Interop.API.Connect.Clients.OrganizationsAPIConnectClient(Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Framework.Guama.Core.Presentation.API.Connect.Providers.Facade.IHttpClientHelperProviderFacade>());

        var model = new Interop.API.Common.Models.Input.OrganizationInputModel
        {
            Id = ConcreteBindingContext.Id,
            Name = ConcreteBindingContext.Name,
            Alias = ConcreteBindingContext.Alias,
            About = ConcreteBindingContext.About,
            Priority = ConcreteBindingContext.Priority,
            EditionPhoneContactItems = editionPhoneContactItems,
            IncomingPhoneContactItems = incomingPhoneContactItems,
            OutgoingPhoneContactItems = outgoingPhoneContactItems,
            EditionEmailContactItems = editionEmailContactItems,
            IncomingEmailContactItems = incomingEmailContactItems,
            OutgoingEmailContactItems = outgoingEmailContactItems
        };

        if (imagePickedModel != null)
        {
            imagePickedModel.Id = System.Guid.NewGuid();
            imagePickedModel.About = $"{EntityTypeName} {model.Id}";

            model.Image = imagePickedModel;
        }

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

    private void Picker_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        hasChangesFlag = true;
    }

    private void ImageLayoutTapGestureRecognizer_Tapped(object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {
        Core.Presentation.Terminal.MAUI.UI.Default.Presenters.ImagePickPresenter.Instance.BeginImagePickAsync(this, ImagePickSuccess_Callback);
    }

    private void PhoneContactItemAgregationButton_Clicked(object sender, System.EventArgs eventArgs)
    {
        this.NavigationPushPageAsync(new Core.Presentation.Terminal.MAUI.UI.Default.Pages.Configuration.PhoneContactItemConfigurationPage(PhoneContactItemCreateSuccess_Callback, new Core.Presentation.Terminal.MAUI.Models.Page.Configuration.PhoneContactItemConfigurationPageModel(null)));
    }

    private void EmailContactItemAgregationButton_Clicked(object sender, System.EventArgs eventArgs)
    {
        this.NavigationPushPageAsync(new Core.Presentation.Terminal.MAUI.UI.Default.Pages.Configuration.EmailContactItemConfigurationPage(EmailContactItemCreateSuccess_Callback, new Core.Presentation.Terminal.MAUI.Models.Page.Configuration.EmailContactItemConfigurationPageModel(null)));
    }

    private void PhoneContactItemEditionButton_Clicked(object sender, System.EventArgs eventArgs)
    {
        var target = ((Microsoft.Maui.Controls.BindableObject)sender).BindingContext as PhoneContactItemDisplayModel;

        this.NavigationPushPageAsync(new Core.Presentation.Terminal.MAUI.UI.Default.Pages.Configuration.PhoneContactItemConfigurationPage(PhoneContactItemEditSuccess_Callback, new Core.Presentation.Terminal.MAUI.Models.Page.Configuration.PhoneContactItemConfigurationPageModel(target)));
    }

    private void EmailContactItemEditionButton_Clicked(object sender, System.EventArgs eventArgs)
    {
        var target = ((Microsoft.Maui.Controls.BindableObject)sender).BindingContext as EmailContactItemDisplayModel;

        this.NavigationPushPageAsync(new Core.Presentation.Terminal.MAUI.UI.Default.Pages.Configuration.EmailContactItemConfigurationPage(EmailContactItemEditSuccess_Callback, new Core.Presentation.Terminal.MAUI.Models.Page.Configuration.EmailContactItemConfigurationPageModel(target)));
    }

    private void PhoneContactItemRemotionButton_Clicked(object sender, System.EventArgs eventArgs)
    {
        DisplayAlert("Confirmación", "Está seguro que desea retirar este teléfono?", "Confirmar", "Cancelar").ContinueWith(task =>
        {
            if (!task.Result) return;

            Dispatcher.Dispatch(() =>
            {
                var target = ((Microsoft.Maui.Controls.BindableObject)sender).BindingContext as PhoneContactItemDisplayModel;

                var handled = false;

                foreach (var item in incomingPhoneContactItems)
                {
                    if (item.Id != target.Id) continue;

                    incomingPhoneContactItems.Remove(incomingPhoneContactItems.First(model => model.Id == target.Id));

                    handled = true;

                    break;
                }

                if (!handled)
                {
                    foreach (var item in editionPhoneContactItems)
                    {
                        if (item.Id != target.Id) continue;

                        editionPhoneContactItems.Remove(editionPhoneContactItems.First(model => model.Id == target.Id));

                        break;
                    }
                }

                if (!handled)
                {
                    outgoingPhoneContactItems.Add(target);
                }

                ConcreteBindingContext.PhoneContactItems.Remove(target);

                hasChangesFlag = true;
            });
        });
    }

    private void EmailContactItemRemotionButton_Clicked(object sender, System.EventArgs eventArgs)
    {
        DisplayAlert("Confirmación", "Está seguro que desea retirar este correo electrónico?", "Confirmar", "Cancelar").ContinueWith(task =>
        {
            if (!task.Result) return;

            Dispatcher.Dispatch(() =>
            {
                var target = ((Microsoft.Maui.Controls.BindableObject)sender).BindingContext as EmailContactItemDisplayModel;

                var handled = false;

                foreach (var item in incomingEmailContactItems)
                {
                    if (item.Id != target.Id) continue;

                    incomingEmailContactItems.Remove(incomingEmailContactItems.First(model => model.Id == target.Id));

                    handled = true;

                    break;
                }

                if (!handled)
                {
                    foreach (var item in editionEmailContactItems)
                    {
                        if (item.Id != target.Id) continue;

                        editionEmailContactItems.Remove(editionEmailContactItems.First(model => model.Id == target.Id));

                        break;
                    }
                }

                if (!handled)
                {
                    outgoingEmailContactItems.Add(target);
                }

                ConcreteBindingContext.EmailContactItems.Remove(target);

                hasChangesFlag = true;
            });
        });
    }

    private void PhoneContactItemCreateSuccess_Callback(PhoneContactItemDisplayModel model)
    {
        incomingPhoneContactItems.Add(new PhoneContactItemInputModel
        {
            Id = model.Id,
            Label = model.Label,
            PhoneNumber = model.PhoneNumber
        });

        ConcreteBindingContext.PhoneContactItems.Add(model);

        hasChangesFlag = true;
    }

    private void EmailContactItemCreateSuccess_Callback(EmailContactItemDisplayModel model)
    {
        incomingEmailContactItems.Add(new EmailContactItemInputModel
        {
            Id = model.Id,
            Label = model.Label,
            EmailAddress = model.EmailAddress
        });

        ConcreteBindingContext.EmailContactItems.Add(model);

        hasChangesFlag = true;
    }

    private void PhoneContactItemEditSuccess_Callback(PhoneContactItemDisplayModel model)
    {
        var handled = false;

        foreach (var item in incomingPhoneContactItems)
        {
            if (item.Id != model.Id) continue;

            item.Label = model.Label;
            item.PhoneNumber = model.PhoneNumber;

            handled = true;

            break;
        }

        if (!handled)
        {
            foreach (var item in editionPhoneContactItems)
            {
                if (item.Id != model.Id) continue;

                item.Label = model.Label;
                item.PhoneNumber = model.PhoneNumber;

                handled = true;

                break;
            }
        }

        if (!handled)
        {
            editionPhoneContactItems.Add(new PhoneContactItemInputModel
            {
                Id = model.Id,
                Label = model.Label,
                PhoneNumber = model.PhoneNumber
            });
        }

        var index = -1;

        foreach (var item in ConcreteBindingContext.PhoneContactItems)
        {
            index++;

            if (item.Id != model.Id) continue;

            ConcreteBindingContext.PhoneContactItems[index] = model;

            break;
        }

        hasChangesFlag = true;
    }

    private void EmailContactItemEditSuccess_Callback(EmailContactItemDisplayModel model)
    {
        var handled = false;

        foreach (var item in incomingEmailContactItems)
        {
            if (item.Id != model.Id) continue;

            item.Label = model.Label;
            item.EmailAddress = model.EmailAddress;

            handled = true;

            break;
        }

        if (!handled)
        {
            foreach (var item in editionEmailContactItems)
            {
                if (item.Id != model.Id) continue;

                item.Label = model.Label;
                item.EmailAddress = model.EmailAddress;

                handled = true;

                break;
            }
        }

        if (!handled)
        {
            editionEmailContactItems.Add(new EmailContactItemInputModel
            {
                Id = model.Id,
                Label = model.Label,
                EmailAddress = model.EmailAddress
            });
        }

        var index = -1;

        foreach (var item in ConcreteBindingContext.EmailContactItems)
        {
            index++;

            if (item.Id != model.Id) continue;

            ConcreteBindingContext.EmailContactItems[index] = model;

            break;
        }

        hasChangesFlag = true;
    }

    private void ImagePickSuccess_Callback(ImageInputModel model)
    {
        imagePickedModel = model;

        ConcreteBindingContext.ImageSource = Microsoft.Maui.Controls.ImageSource.FromStream(() => new System.IO.MemoryStream(imagePickedModel.ContentData));

        hasChangesFlag = true;
    }
}