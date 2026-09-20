using Microsoft.Extensions.DependencyInjection;
using MOROPO.Framework.Caney.MVVM.Core.Extensions;
using System.Linq;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Terminal.UI.Pages.Detail;

public partial class PersonDetailPage : Core.Presentation.Terminal.MAUI.UI.Pages.Base.DetailBaseContentPage<Interop.API.Common.Models.Display.PersonDisplayModel>
{
    private readonly System.Action<Interop.API.Common.Models.Display.PersonDisplayModel, int?> delegationAction;

    public PersonDetailPage(Interop.API.Common.Models.Display.PersonDisplayModel bindingContextModel, System.Action<Interop.API.Common.Models.Display.PersonDisplayModel, int?> delegationAction) : base(bindingContextModel)
    {
        InitializeComponent();

        this.delegationAction = delegationAction;
    }

    private void EditButton_Clicked(object sender, System.EventArgs eventArgs)
    {
        var model = BindingContext as Interop.API.Common.Models.Display.PersonDisplayModel;

        this.NavigationPopModalPageAsync().ContinueWith(_ =>
        {
            // null for Edit
            Dispatcher.Dispatch(() => delegationAction.Invoke(model, null));
        });
    }

    private void DeleteButton_Clicked(object sender, System.EventArgs eventArgs)
    {
        var user = Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Core.Presentation.Terminal.MAUI.Managers.Facade.ISessionManagerFacade>().CurrentSession.User;

        if (!user.HasFullAccess && !user.Roles.Any(item => item.Permissions.Any(_item => _item.Code == Interop.API.Common.Constants.Security.Authorization.PersonRemoverPermission)))
        {
            DisplayAlert("Aviso", "Su usuario no cuenta con permisos para retirar elementos registrados. Póngase en contacto con los administradores del sistema para solicitar los permisos requeridos.", "Aceptar");

            return;
        }

        DisplayAlert("Confirmación", "Está seguro que desea eliminar este elemento?", "Eliminar", "Cancelar").ContinueWith(task =>
        {
            if (!task.Result) return;

            var model = BindingContext as Interop.API.Common.Models.Display.PersonDisplayModel;

            Dispatcher.Dispatch(() =>
            {
                this.NavigationPopModalPageAsync().ContinueWith(_ =>
                {
                    // 0 for Delete
                    Dispatcher.Dispatch(() => delegationAction.Invoke(model, 0));
                });
            });
        });
    }
}