using Microsoft.Extensions.DependencyInjection;
using MOROPO.SINERGIAGOB.Core.Presentation.Terminal.MAUI.UI.Extensions;
using MOROPO.SINERGIAGOB.GovCamaguey.Terminal.Models.Page.Configuration;
using System.Linq;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Terminal.UI.Pages.Manage;

public partial class ObjectivesManagePage : Core.Presentation.Terminal.MAUI.UI.Pages.Base.ManageBaseContentPage<Interop.API.Common.Models.Display.ObjectiveDisplayModel, Models.Page.Manage.ObjectivesManagePageModel, Models.Page.Configuration.ObjectiveConfigurationPageModel>
{
    public static double ITEM_VIEW_REQUEST_HEIGHT = 60;

    public ObjectivesManagePage()
    {
        InitializeComponent();

        IsEnabled = false;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (IsEnabled) return;

        IsEnabled = true;

        LoadItems(true);
    }

    protected override string ViewerPermissionCode => Interop.API.Common.Constants.Security.Authorization.ObjectiveViewerPermission;

    protected override string CreatorPermissionCode => Interop.API.Common.Constants.Security.Authorization.ObjectiveCreatorPermission;

    protected override string EditorPermissionCode => Interop.API.Common.Constants.Security.Authorization.ObjectiveEditorPermission;

    protected override Microsoft.Maui.Controls.CollectionView GetItemsCollectionView()
    {
        return ItemsCollectionView;
    }

    protected override double MeasureCollectionViewCellHeight(double width)
    {
        return ITEM_VIEW_REQUEST_HEIGHT;
    }

    protected override System.Threading.Tasks.Task<Framework.Guama.Core.Presentation.Models.PaginationModel<Interop.API.Common.Models.Display.ObjectiveDisplayModel>> FetchItemsAsync()
    {
        var apiConnectClient = new Interop.API.Connect.Clients.ObjectivesAPIConnectClient(Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Framework.Guama.Core.Presentation.API.Connect.Providers.Facade.IHttpClientHelperProviderFacade>());

        return apiConnectClient.GetPaginateAsync(CurrentItemsPageNumber, CurrentItemsPageLimit);
    }

    protected override void OnActionDelegationRequest(Interop.API.Common.Models.Display.ObjectiveDisplayModel target, int discriminator)
    {
        switch (discriminator)
        {
            case 0:
                var apiConnectClient = new Interop.API.Connect.Clients.ObjectivesAPIConnectClient(Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Framework.Guama.Core.Presentation.API.Connect.Providers.Facade.IHttpClientHelperProviderFacade>());

                apiConnectClient.ArchiveAsync(target.Id).ContinueWith(task =>
                {
                    Dispatcher.Dispatch(() => LoadItems(true));
                });

                break;

            default:
                throw new System.InvalidOperationException();
        }
    }

    protected override bool IsAllowedForEdition(Interop.API.Common.Models.Display.ObjectiveDisplayModel target, Core.Interop.API.Common.Models.Display.UserDisplayModel user)
    {
        if (target.IsModerable && (user.HasFullAccess || user.Roles.Any(item => item.Permissions.Any(_item => _item.Code == Interop.API.Common.Constants.Security.Authorization.ObjectiveEditorPermission)))) return true;

        if (target.Proyections.Any(item => item.IsModerable) && (user.HasFullAccess || user.Roles.Any(item => item.Permissions.Any(_item => _item.Code == Interop.API.Common.Constants.Security.Authorization.ProyectionEditorPermission)))) return true;

        if (target.Proyections.Any(item => item.Actions.Any(_item => _item.IsModerable)) && (user.HasFullAccess || user.Roles.Any(item => item.Permissions.Any(_item => _item.Code == Interop.API.Common.Constants.Security.Authorization.ActionEditorPermission)))) return true;

        return base.IsAllowedForEdition(target, user);
    }

    private void AddItemToolbarItems_Clicked(object sender, System.EventArgs eventArgs)
    {
        AddItem(sender, eventArgs);
    }

    private void ReloadToolbarItems_Clicked(object sender, System.EventArgs eventArgs)
    {
        OnReload(sender, eventArgs);
    }

    private void LoadMoreBurron_Clicked(object sender, System.EventArgs eventArgs)
    {
        OnLoadMore(sender, eventArgs);
    }

    private void ItemTapGestureRecognizer_Tapped(object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {
        if (!((sender as Microsoft.Maui.Controls.BindableObject).BindingContext as Interop.API.Common.Models.Display.ObjectiveDisplayModel).IsModerable)
        {
            DisplayAlert("Aviso", "Su usuario no ha sido asiganado como moderador de este objetivo, por lo que no podrá realizar operaciones sobre el mismo. Contacte con los administradores del sistema ante cualquier inquietud al respecto.", "Aceptar");

            return;
        }

        base.ItemDetailRequestControl_Invoked(sender, System.EventArgs.Empty);
    }

    protected override void ConfigurationSuccess_Callback(Interop.API.Common.Models.Display.ObjectiveDisplayModel model)
    {
        this.PresentToastNotification(Core.Presentation.Terminal.MAUI.Enumerations.NotificationKind.Success, "Cambios aplicados con éxito.", delay: System.TimeSpan.FromMilliseconds(500));
    }

    protected override void ComplementConfigurationPage(Core.Presentation.Terminal.MAUI.UI.Pages.Base.ConfigurationBaseContentPage<Interop.API.Common.Models.Display.ObjectiveDisplayModel, Interop.API.Common.Models.Display.ObjectiveDisplayModel, ObjectiveConfigurationPageModel> page, Interop.API.Common.Models.Display.ObjectiveDisplayModel model = null)
    {
        Dispatcher.Dispatch(() => IsEnabled = false);

        base.ComplementConfigurationPage(page, model);
    }
}