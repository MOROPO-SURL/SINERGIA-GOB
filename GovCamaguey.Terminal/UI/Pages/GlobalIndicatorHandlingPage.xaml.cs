using Microsoft.Extensions.DependencyInjection;
using MOROPO.Framework.Caney.MVVM.Core.Extensions;
using MOROPO.SINERGIAGOB.Core.Presentation.Terminal.MAUI.UI.Extensions;
using MOROPO.SINERGIAGOB.GovCamaguey.Terminal.Factories;
using System.Linq;
using static SkiaSharp.HarfBuzz.SKShaper;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Terminal.UI.Pages;

public partial class GlobalIndicatorHandlingPage : Microsoft.Maui.Controls.ContentPage
{
	public GlobalIndicatorHandlingPage()
	{
		InitializeComponent();

        BindingContext = new Models.Page.GlobalIndicatorHandlingPageModel();

        IsEnabled = false;
    }

    private Models.Page.GlobalIndicatorHandlingPageModel ConcreteBindingContext { get => BindingContext as Models.Page.GlobalIndicatorHandlingPageModel; }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (IsEnabled) return;

        IsEnabled = true;

        ReloadToolbarItem_Clicked(this, System.EventArgs.Empty);
    }

    private void ReloadToolbarItem_Clicked(object sender, System.EventArgs eventArgs)
    {
        if (ConcreteBindingContext.IsBusy) return;

        ConcreteBindingContext.IsBusy = true;

        ConcreteBindingContext.Indicators.Clear();

        var httpClientHelperProvider = Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Framework.Guama.Core.Presentation.API.Connect.Providers.Facade.IHttpClientHelperProviderFacade>();

        var indicatorsAPIConnectClient = new Interop.API.Connect.Clients.GlobalIndicatorsAPIConnectClient(httpClientHelperProvider);
        var resultsAPIConnectClient = new Interop.API.Connect.Clients.GlobalIndicatorResultsAPIConnectClient(httpClientHelperProvider);

        indicatorsAPIConnectClient.GetAsync().ContinueWith(task =>
        {
            Dispatcher.Dispatch(() =>
            {
                if (task.Exception != null || task.IsCanceled)
                {
                    Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Core.Presentation.Terminal.MAUI.Helpers.Facade.IExceptionHandlingHelperFacade>().HandleException(task.Exception, this);                    
                }
                else
                {
                    resultsAPIConnectClient.GetLasts().ContinueWith(_task =>
                    {
                        Dispatcher.Dispatch(() =>
                        {
                            if (_task.Exception != null || _task.IsCanceled)
                            {
                                Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Core.Presentation.Terminal.MAUI.Helpers.Facade.IExceptionHandlingHelperFacade>().HandleException(_task.Exception, this);
                            }
                            else
                            {
                                foreach (var item in task.Result)
                                {
                                    var target = _task.Result.FirstOrDefault(_model => _model.IndicatorId == item.Id);

                                    item.Balance = target?.Amount ?? 0;
                                    item.ResultAt = target?.At;
                                    item.Context = target?.Description;

                                    ConcreteBindingContext.Indicators.Add(item);
                                }
                            }
                        });
                    });
                }

                ConcreteBindingContext.IsBusy = false;
            });
        });
    }

    private void DownloadAllToolbarItems_Clicked(object sender, System.EventArgs eventArgs)
    {
        if (ConcreteBindingContext.IsBusy) return;

        var httpClientHelperProvider = Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Framework.Guama.Core.Presentation.API.Connect.Providers.Facade.IHttpClientHelperProviderFacade>();

        ConcreteBindingContext.IsBusy = true;

        var httpClient = new System.Net.Http.HttpClient(HttpClientHandlerFactory.Build())
        {
            Timeout = System.TimeSpan.FromSeconds(30.0)
        };

        System.DateTime now = System.DateTime.Now;

        System.DateTime referenceAt = new System.DateTime(now.Year, now.Month, 1);

        referenceAt.AddDays(-1);

        httpClient.GetByteArrayAsync($"{httpClientHelperProvider.HttpClientHelper.ClientBaseAddress}report/indicators/stats/reference/{referenceAt.Ticks}/print").ContinueWith(task =>
        {
            Dispatcher.Dispatch(async () =>
            {
                if (task.IsCanceled || task.Exception != null)
                {
                    Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Core.Presentation.Terminal.MAUI.Helpers.Facade.IExceptionHandlingHelperFacade>().HandleException(task.Exception, this);
                }
                else
                {
                    var fileSaverResult = await CommunityToolkit.Maui.Storage.FileSaver.Default.SaveAsync($"Estado de Indicadores de Objetivos {now:dd-MM-yyyy}.pdf", new System.IO.MemoryStream(task.Result));

                    if (fileSaverResult.IsSuccessful)
                    {
                        this.PresentToastNotification(Core.Presentation.Terminal.MAUI.Enumerations.NotificationKind.Success, "Archivo descargado con éxito.");
                    }
                    else if (fileSaverResult.FilePath != null)
                    {
                        this.PresentToastNotification(Core.Presentation.Terminal.MAUI.Enumerations.NotificationKind.Error, "No fue posible salvar el archivo.");
                    }
                }

                ConcreteBindingContext.IsBusy = false;
            });
        });
    }

    private void ResultUpgradeButton_Clicked(object sender, System.EventArgs e)
    {
        var user = Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Core.Presentation.Terminal.MAUI.Managers.Facade.ISessionManagerFacade>().CurrentSession.User;

        if (!user.HasFullAccess && !user.Roles.Any(item => item.Permissions.Any(_item => _item.Code == Interop.API.Common.Constants.Security.Authorization.GlobalIndicatorResultCreatorPermission)))
        {
            Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlert("Aviso", "Su usuario no cuenta con permisos para agregar nuevos elementos de este tipo. Póngase en contacto con los administradores del sistema para solicitar los permisos requeridos.", "Aceptar");

            return;
        }

        var senderBindingContext = (sender as Microsoft.Maui.Controls.BindableObject).BindingContext as Interop.API.Common.Models.Display.GlobalIndicatorDisplayModel;

        this.NavigationPushModalPageAsync(new Configuration.CommonIndicatorResultConfigurationPage(new Models.Page.Configuration.CommonIndicatorResultConfigurationPageModel(senderBindingContext.Id, senderBindingContext.Name, senderBindingContext.UnitMeasurement), ResultConfigured_Callback));
    }

    private void LastResultDetailButton_Clicked(object sender, System.EventArgs e)
    {
        var senderBindingContext = (sender as Microsoft.Maui.Controls.BindableObject).BindingContext as Interop.API.Common.Models.Display.GlobalIndicatorDisplayModel;

        if (senderBindingContext.ResultAt == null) return;

        this.NavigationPushModalPageAsync(new Detail.CommonIndicatorResultDetailPage(new Models.Data.CommonIndicatorResultDataModel(senderBindingContext.Id, senderBindingContext.Balance, senderBindingContext.ResultAt.Value, senderBindingContext.Context)));
    }

    private void ResultConfigured_Callback(Models.Data.CommonIndicatorResultDataModel model)
    {
        ConcreteBindingContext.IsBusy = true;

        var apiConnectClient = new Interop.API.Connect.Clients.GlobalIndicatorResultsAPIConnectClient(Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Framework.Guama.Core.Presentation.API.Connect.Providers.Facade.IHttpClientHelperProviderFacade>());

        apiConnectClient.CreateAsync(new Interop.API.Common.Models.Input.GlobalIndicatorResultInputModel { Amount = model.Amount, IndicatorId = model.IndicatorId, At = model.At, Description = model.Description }).ContinueWith(_task =>
        {
            Dispatcher.Dispatch(() =>
            {
                ConcreteBindingContext.IsBusy = false;

                if (_task.Exception != null || _task.IsCanceled)
                {
                    Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Core.Presentation.Terminal.MAUI.Helpers.Facade.IExceptionHandlingHelperFacade>().HandleException(_task.Exception, Microsoft.Maui.Controls.Shell.Current.CurrentPage);
                }
                else
                {
                    DisplayAlert("Aviso", "Cambios aplicados con éxito.", "Aceptar");

                    ReloadToolbarItem_Clicked(this, System.EventArgs.Empty);
                }
            });
        });
    }
}