using Microsoft.Extensions.DependencyInjection;
using MOROPO.Framework.Caney.MVVM.Core.Extensions;
using System.Linq;

namespace MOROPO.SINETGIAGOB.GovCamaguey.Terminal.UI.Views.Card;

public partial class ProyectionIndicatorDeepHandlingCardView : Microsoft.Maui.Controls.ContentView
{
    public ProyectionIndicatorDeepHandlingCardView()
    {
        InitializeComponent();
    }

    private void ResultUpgradeButton_Clicked(object sender, System.EventArgs e)
    {
        var user = Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Core.Presentation.Terminal.MAUI.Managers.Facade.ISessionManagerFacade>().CurrentSession.User;

        if (!user.HasFullAccess && !user.Roles.Any(item => item.Permissions.Any(_item => _item.Code == Interop.API.Common.Constants.Security.Authorization.ProyectionIndicatorResultCreatorPermission)))
        {
            Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlert("Aviso", "Su usuario no cuenta con permisos para agregar nuevos elementos de este tipo. Póngase en contacto con los administradores del sistema para solicitar los permisos requeridos.", "Aceptar");

            return;
        }

        var concreteBindingContext = (sender as Microsoft.Maui.Controls.BindableObject).BindingContext as Interop.API.Common.Models.Display.ProyectionIndicatorDisplayModel;

        Microsoft.Maui.Controls.Shell.Current.CurrentPage.NavigationPushModalPageAsync(new Pages.Configuration.CommonIndicatorResultConfigurationPage(new Models.Page.Configuration.CommonIndicatorResultConfigurationPageModel(concreteBindingContext.Id, concreteBindingContext.Name, concreteBindingContext.UnitMeasurement), ResultConfigured_Callback));
    }

    private void LastResultDetailButton_Clicked(object sender, System.EventArgs e)
    {
        var senderBindingContext = (sender as Microsoft.Maui.Controls.BindableObject).BindingContext as Interop.API.Common.Models.Display.ProyectionIndicatorDisplayModel;

        if (senderBindingContext.ResultAt == null) return;

        Microsoft.Maui.Controls.Shell.Current.CurrentPage.NavigationPushModalPageAsync(new Pages.Detail.CommonIndicatorResultDetailPage(new Models.Data.CommonIndicatorResultDataModel(senderBindingContext.Id, senderBindingContext.Balance, senderBindingContext.ResultAt.Value, senderBindingContext.Context)));
    }

    private void ResultConfigured_Callback(Models.Data.CommonIndicatorResultDataModel model)
    {
        var pageModel = (Microsoft.Maui.Controls.Shell.Current.CurrentPage as Pages.ObjectiveIndicatorHandlingPage).BindingContext as Framework.Caney.MVVM.Core.Models.Page.BasePageModel;

        pageModel.IsBusy = true;

        var apiConnectClient = new Interop.API.Connect.Clients.ProyectionIndicatorResultsAPIConnectClient(Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Framework.Guama.Core.Presentation.API.Connect.Providers.Facade.IHttpClientHelperProviderFacade>());

        apiConnectClient.CreateAsync(new Interop.API.Common.Models.Input.ProyectionIndicatorResultInputModel { Amount = model.Amount, IndicatorId = model.IndicatorId, At = model.At, Description = model.Description }).ContinueWith(_task =>
        {
            Dispatcher.Dispatch(() =>
            {
                pageModel.IsBusy = false;

                if (_task.Exception != null || _task.IsCanceled)
                {
                    Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Core.Presentation.Terminal.MAUI.Helpers.Facade.IExceptionHandlingHelperFacade>().HandleException(_task.Exception, Microsoft.Maui.Controls.Shell.Current.CurrentPage);
                }
                else
                {
                    (BalanceSpan.BindingContext as Interop.API.Common.Models.Display.ProyectionIndicatorDisplayModel).Balance = model.Amount;

                    BalanceSpan.Text = model.Amount.ToString();

                    Microsoft.Maui.Controls.Shell.Current.CurrentPage.DisplayAlert("Aviso", "Cambios aplicados con éxito.", "Aceptar");
                }
            });
        });
    }
}