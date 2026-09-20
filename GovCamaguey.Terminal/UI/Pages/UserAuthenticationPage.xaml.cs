using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.ApplicationModel;
using MOROPO.Framework.Caney.MVVM.Core.Extensions;
using MOROPO.SINERGIAGOB.Core.Presentation.Terminal.MAUI.Models.Page;
using MOROPO.SINERGIAGOB.Core.Presentation.Terminal.MAUI.UI.Extensions;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Terminal.UI.Pages;

public partial class UserAuthenticationPage : Framework.Caney.MVVM.Core.Pages.BindingContextedContentPage<UserAuthenticationPageModel>
{
	public UserAuthenticationPage() : base(new UserAuthenticationPageModel
    {
        OwnerInformation = Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Core.Presentation.Terminal.MAUI.Providers.Facade.IRuntimeContextProviderFacade>().OwnerInformation
    })
    {
		InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        BindingContextPageModel.IsBusy = true;
        BindingContextPageModel.BusyProgressMessage = "Comprobando versión ...";

        var apiConnectClient = new Interop.API.Connect.Clients.CurrentTerminalReleaseAPIConnectClient(Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Framework.Guama.Core.Presentation.API.Connect.Providers.Facade.IHttpClientHelperProviderFacade>());

        apiConnectClient.GetDetailAsync().ContinueWith(async task =>
        {
            await System.Threading.Tasks.Task.Delay(1000);

            Dispatcher.Dispatch(() =>
            {
                if (task.IsFaulted || task.IsCanceled)
                {
                    DisplayAlert("Aviso", "No fue posible comprobar la compatibilidad esta versión de la aplicación.", "Salir", "Continuar").ContinueWith(_task =>
                    {
                        if (_task.Result)
                        {
                            Dispatcher.Dispatch(Microsoft.Maui.Controls.Application.Current.Quit);
                        }
                    });
                }
                else if (AppInfo.Current.Version < task.Result.Version)
                {
                    DisplayAlert("Aviso", "Existe una nueva versión de la aplicación compatible con el sistema, por lo que es necesario obtener la nueva versión.", "Salir", "Descargar").ContinueWith(_task =>
                    {
                        if (_task.Result)
                        {
                            Dispatcher.Dispatch(Microsoft.Maui.Controls.Application.Current.Quit);
                        }
                        else
                        {
                            Dispatcher.Dispatch(() =>
                            {
                                Launcher.OpenAsync(task.Result.Uri).ContinueWith(_ => Dispatcher.Dispatch(Microsoft.Maui.Controls.Application.Current.Quit));
                            });
                        }
                    });

                    return;
                }

                BindingContextPageModel.IsBusy = false;
                BindingContextPageModel.BusyProgressMessage = null;
            });
        });
    }

    protected override bool OnBackButtonPressed()
    {
        DisplayAlert("Aviso", "Está seguro que desea salir de la aplicación?", "Salir", "Cancelar").ContinueWith(task =>
        {
            if (task.Result)
            {
                Dispatcher.Dispatch(Microsoft.Maui.Controls.Application.Current.Quit);
            }
        });

        return true;
    }

    private void SignInButton_Clicked(object sender, System.EventArgs e)
    {
        BindingContextPageModel.IsBusy = true;

        Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Core.Presentation.Terminal.MAUI.Managers.Facade.ISessionManagerFacade>().SignInAsync(BindingContextPageModel.UserName, BindingContextPageModel.Password).ContinueWith(task =>
        {
            Dispatcher.Dispatch(() =>
            {
                if (task.IsCanceled || task.Exception != null)
                {
                    var message = "Problemas no esperados en el sistema.";

                    if (task.Exception == null)
                    {
                        message = "El sistema ha tardado demasiado tiempo en responder.";
                    }
                    else
                    {
                        System.Exception targetException = task.Exception;

                        while (targetException is System.AggregateException)
                        {
                            targetException = targetException.InnerException;
                        }

                        if (targetException is System.Net.Http.HttpRequestException || targetException is System.Net.WebException)
                        {
                            message = "No se puede acceder al servidor. Revise su conexión de red y/o la disponibilidad del servicio.";
                        }
                        else if (targetException is Framework.Guama.Core.Presentation.API.Connect.UnspectedHttpStatusException)
                        {
                            var unspectedHttpStatus = (targetException as Framework.Guama.Core.Presentation.API.Connect.UnspectedHttpStatusException).HttpStatusCode;

                            if (unspectedHttpStatus == System.Net.HttpStatusCode.BadRequest)
                            {
                                message = "Datos incorrectos.";
                            }
                            else if (unspectedHttpStatus == System.Net.HttpStatusCode.Forbidden)
                            {
                                message = "Usuario no autorizado.";
                            }
                            else if (unspectedHttpStatus == System.Net.HttpStatusCode.Unauthorized)
                            {
                                message = "Usuario y/o contraseña incorrectos.";
                            }
                            else if (unspectedHttpStatus == System.Net.HttpStatusCode.ServiceUnavailable)
                            {
                                message = "Servicio no disponible en este momento.";
                            }
                        }
                    }

                    BindingContextPageModel.IsBusy = false;

                    DisplayAlert("Error", message, "Aceptar");

                    return;
                }

                this.NavigationPopModalPageAsync();
            });
        });
    }

    private void OnPasswordEnterPressed(object sender, System.EventArgs e)
    {
        SignInButton_Clicked(sender, e);
    }

    private void SettingButton_Clicked(object sender, System.EventArgs e)
    {
        this.NavigationPushModalPageAsync(new Core.Presentation.Terminal.MAUI.UI.Default.Pages.SettingPage(SettingChange_Callback));
    }

    private void SettingChange_Callback()
    {
        (Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Framework.Guama.Core.Presentation.API.Connect.Providers.Facade.IHttpClientHelperProviderFacade>() as Core.Presentation.Terminal.MAUI.Providers.Facade.IReseableHttpClientHelperProviderFacade).Reset();

        this.PresentToastNotification(Core.Presentation.Terminal.MAUI.Enumerations.NotificationKind.Success, "Cambios aplicados con éxito.");
    }
}
