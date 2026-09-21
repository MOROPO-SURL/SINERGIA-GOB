using Microsoft.Extensions.DependencyInjection;
using MOROPO.SINERGIAGOB.Core.Presentation.Terminal.MAUI.UI.Extensions;
using MOROPO.SINERGIAGOB.GovCamaguey.Terminal.Factories;
using System.Threading.Tasks;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Terminal.UI.Pages;

public partial class ObjectiveSummaryPage : Microsoft.Maui.Controls.ContentPage
{
    private const string EXPORT_FORMAT_BOOK_OPTION = "Formato Libro";
    private const string EXPORT_FORMAT_TABLE_OPTION = "Formato Tabla";
    private const string CANCE_OPTION = "Cancelar";

	public ObjectiveSummaryPage()
	{
		InitializeComponent();

        BindingContext = new Models.Page.ObjectiveSummaryPageModel();

        IsEnabled = false;
    }

    private Models.Page.ObjectiveSummaryPageModel ConcreteBindingContext { get => BindingContext as Models.Page.ObjectiveSummaryPageModel; }

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

        ConcreteBindingContext.AvaialbleObjetives.Clear();
        ConcreteBindingContext.Objective = null;

        var apiConnectClient = new Interop.API.Connect.Clients.ObjectivesAPIConnectClient(Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Framework.Guama.Core.Presentation.API.Connect.Providers.Facade.IHttpClientHelperProviderFacade>());

        apiConnectClient.GetAsync().ContinueWith(task =>
        {
            Dispatcher.Dispatch(() =>
            {
                if (task.Exception != null || task.IsCanceled)
                {
                    Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Core.Presentation.Terminal.MAUI.Helpers.Facade.IExceptionHandlingHelperFacade>().HandleException(task.Exception, this);
                }
                else
                {
                    foreach (var model in task.Result)
                    {
                        ConcreteBindingContext.AvaialbleObjetives.Add(model);
                    }
                }

                ConcreteBindingContext.IsBusy = false;
            });
        });
    }

    private void DownloadCurrentToolbarItems_Clicked(object sender, System.EventArgs eventArgs)
    {
        if (ConcreteBindingContext.IsBusy || ConcreteBindingContext.Objective == null) return;

        DisplayActionSheet("Modo de exportación", CANCE_OPTION, null, EXPORT_FORMAT_BOOK_OPTION, EXPORT_FORMAT_TABLE_OPTION).ContinueWith(async actionSheetTask =>
        {
            if (actionSheetTask.IsCanceled || actionSheetTask.Result == CANCE_OPTION) return;

            Dispatcher.Dispatch(() => ConcreteBindingContext.IsBusy = true);

            string action = actionSheetTask.Result switch
            {
                EXPORT_FORMAT_BOOK_OPTION => "book",
                EXPORT_FORMAT_TABLE_OPTION => "table",
                _ => "table"
            };

            var httpClientHelperProvider = Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Framework.Guama.Core.Presentation.API.Connect.Providers.Facade.IHttpClientHelperProviderFacade>();

            var httpClient = new System.Net.Http.HttpClient(HttpClientHandlerFactory.Build())
            {
                Timeout = System.TimeSpan.FromSeconds(30.0)
            };

            try
            {
                string modeTitleSection = actionSheetTask.Result switch
                {
                    EXPORT_FORMAT_BOOK_OPTION => "Libro",
                    EXPORT_FORMAT_TABLE_OPTION => "Tabla",
                    _ => "Tabla"
                };

                var result = await httpClient.GetByteArrayAsync($"{httpClientHelperProvider.HttpClientHelper.ClientBaseAddress}report/objectives/{action}/{ConcreteBindingContext.Objective.Id}/print");

                var fileSaverResult = await CommunityToolkit.Maui.Storage.FileSaver.Default.SaveAsync($"{modeTitleSection} Resumen de Objetivo-{ConcreteBindingContext.Objective.Index} {System.DateTime.Now:dd-MM-yyyy}.pdf", new System.IO.MemoryStream(result));

                Dispatcher.Dispatch(() =>
                {
                    ConcreteBindingContext.IsBusy = false;

                    if (fileSaverResult.IsSuccessful)
                    {
                        this.PresentToastNotification(Core.Presentation.Terminal.MAUI.Enumerations.NotificationKind.Success, "Archivo descargado con éxito.");
                    }
                    else if (fileSaverResult.FilePath != null)
                    {
                        this.PresentToastNotification(Core.Presentation.Terminal.MAUI.Enumerations.NotificationKind.Error, "No fue posible salvar el archivo.");
                    }
                });
            }
            catch (System.Exception exception)
            {
                Dispatcher.Dispatch(() =>
                {
                    ConcreteBindingContext.IsBusy = true;

                    Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Core.Presentation.Terminal.MAUI.Helpers.Facade.IExceptionHandlingHelperFacade>().HandleException(exception, this);
                });
            }
        });
    }

    private void DownloadAllToolbarItems_Clicked(object sender, System.EventArgs eventArgs)
    {
        if (ConcreteBindingContext.IsBusy) return;

        DisplayActionSheet("Modo de exportación", CANCE_OPTION, null, EXPORT_FORMAT_BOOK_OPTION, EXPORT_FORMAT_TABLE_OPTION).ContinueWith(async actionSheetTask =>
        {
            if (actionSheetTask.IsCanceled || actionSheetTask.Result == CANCE_OPTION) return;

            Dispatcher.Dispatch(() => ConcreteBindingContext.IsBusy = true);

            string action = actionSheetTask.Result switch
            {
                EXPORT_FORMAT_BOOK_OPTION => "book",
                EXPORT_FORMAT_TABLE_OPTION => "table",
                _ => "table"
            };

            var httpClientHelperProvider = Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Framework.Guama.Core.Presentation.API.Connect.Providers.Facade.IHttpClientHelperProviderFacade>();

            var httpClient = new System.Net.Http.HttpClient(HttpClientHandlerFactory.Build())
            {
                Timeout = System.TimeSpan.FromSeconds(30.0)
            };

            try
            {
                string modeTitleSection = actionSheetTask.Result switch
                {
                    EXPORT_FORMAT_BOOK_OPTION => "Libro",
                    EXPORT_FORMAT_TABLE_OPTION => "Tabla",
                    _ => "Tabla"
                };

                var result = await httpClient.GetByteArrayAsync($"{httpClientHelperProvider.HttpClientHelper.ClientBaseAddress}report/objectives/{action}/print");

                var fileSaverResult = await CommunityToolkit.Maui.Storage.FileSaver.Default.SaveAsync($"{modeTitleSection} Resumen de Objetivos {System.DateTime.Now:dd-MM-yyyy}.pdf", new System.IO.MemoryStream(result));

                Dispatcher.Dispatch(() =>
                {
                    ConcreteBindingContext.IsBusy = false;

                    if (fileSaverResult.IsSuccessful)
                    {
                        this.PresentToastNotification(Core.Presentation.Terminal.MAUI.Enumerations.NotificationKind.Success, "Archivo descargado con éxito.");
                    }
                    else if (fileSaverResult.FilePath != null)
                    {
                        this.PresentToastNotification(Core.Presentation.Terminal.MAUI.Enumerations.NotificationKind.Error, "No fue posible salvar el archivo.");
                    }
                });
            }
            catch (System.Exception exception)
            {
                Dispatcher.Dispatch(() =>
                {
                    ConcreteBindingContext.IsBusy = true;

                    Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Core.Presentation.Terminal.MAUI.Helpers.Facade.IExceptionHandlingHelperFacade>().HandleException(exception, this);
                });
            }
        });
    }

    private void ObjectivePicker_SelectedIndexChanged(object sender, System.EventArgs eventArgs)
    {
        LoadItems();
    }

    private void LoadItems()
    {
        if (ObjectivePicker.SelectedIndex <= -1) return;

        ConcreteBindingContext.IsBusy = true;

        var objectiveId = ConcreteBindingContext.AvaialbleObjetives[ObjectivePicker.SelectedIndex].Id;

        var objectivesAPIConnectClient = new Interop.API.Connect.Clients.ObjectivesAPIConnectClient(Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Framework.Guama.Core.Presentation.API.Connect.Providers.Facade.IHttpClientHelperProviderFacade>());

        objectivesAPIConnectClient.GetAsync(objectiveId).ContinueWith(task =>
        {
            Dispatcher.Dispatch(() =>
            {
                if (task.Exception != null || task.IsCanceled)
                {
                    Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Core.Presentation.Terminal.MAUI.Helpers.Facade.IExceptionHandlingHelperFacade>().HandleException(task.Exception, this);

                    ConcreteBindingContext.Objective = null;
                    ConcreteBindingContext.IsBusy = false;
                }
                else
                {
                    ConcreteBindingContext.Objective = new Models.View.ObjectiveSummaryViewModel(task.Result);
                    ConcreteBindingContext.IsBusy = false;
                }
            });
        });
    }
}