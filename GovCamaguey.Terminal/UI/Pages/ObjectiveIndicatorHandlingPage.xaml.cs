using Microsoft.Extensions.DependencyInjection;
using MOROPO.SINERGIAGOB.Core.Presentation.Terminal.MAUI.UI.Extensions;
using MOROPO.SINERGIAGOB.GovCamaguey.Interop.API.Common.Models.Display;
using MOROPO.SINERGIAGOB.GovCamaguey.Terminal.Factories;
using System.Collections.Generic;
using System.Linq;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Terminal.UI.Pages;

public partial class ObjectiveIndicatorHandlingPage : Microsoft.Maui.Controls.ContentPage
{
    public ObjectiveIndicatorHandlingPage()
    {
        InitializeComponent();

        BindingContext = new Models.Page.ObjectiveIndicatorHandlingPageModel();

        IsEnabled = false;
    }

    private Models.Page.ObjectiveIndicatorHandlingPageModel ConcreteBindingContext { get => BindingContext as Models.Page.ObjectiveIndicatorHandlingPageModel; }

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

        var httpClientHelperProvider = Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Framework.Guama.Core.Presentation.API.Connect.Providers.Facade.IHttpClientHelperProviderFacade>();

        ConcreteBindingContext.IsBusy = true;

        var httpClient = new System.Net.Http.HttpClient(HttpClientHandlerFactory.Build())
        {
            Timeout = System.TimeSpan.FromSeconds(30.0)
        };

        System.DateTime now = System.DateTime.Now;

        System.DateTime referenceAt = new System.DateTime(now.Year, now.Month, 1);

        referenceAt.AddDays(-1);

        httpClient.GetByteArrayAsync($"{httpClientHelperProvider.HttpClientHelper.ClientBaseAddress}report/indicators/stats/{ConcreteBindingContext.Objective.Id}/reference/{referenceAt.Ticks}/print").ContinueWith(task =>
        {
            Dispatcher.Dispatch(async () =>
            {
                if (task.IsCanceled || task.Exception != null)
                {
                    Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Core.Presentation.Terminal.MAUI.Helpers.Facade.IExceptionHandlingHelperFacade>().HandleException(task.Exception, this);
                }
                else
                {
                    var fileSaverResult = await CommunityToolkit.Maui.Storage.FileSaver.Default.SaveAsync($"Estado de Indicadores de Objetivo-{ConcreteBindingContext.Objective.Index} {now:dd-MM-yyyy}.pdf", new System.IO.MemoryStream(task.Result));

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

    private void ObjectivePicker_SelectedIndexChanged(object sender, System.EventArgs eventArgs)
    {
        LoadItems();
    }

    private void LoadItems()
    {
        if (ObjectivePicker.SelectedIndex <= -1) return;

        ConcreteBindingContext.IsBusy = true;

        var objectiveId = ConcreteBindingContext.AvaialbleObjetives[ObjectivePicker.SelectedIndex].Id;

        var httpClientHelperProvider = Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Framework.Guama.Core.Presentation.API.Connect.Providers.Facade.IHttpClientHelperProviderFacade>();

        var objectivesAPIConnectClient = new Interop.API.Connect.Clients.ObjectivesAPIConnectClient(httpClientHelperProvider);
        var objectiveIndicatorResultsAPIConnectClient = new Interop.API.Connect.Clients.ObjectiveIndicatorResultsAPIConnectClient(httpClientHelperProvider);
        var proyectionIndicatorResultsAPIConnectClient = new Interop.API.Connect.Clients.ProyectionIndicatorResultsAPIConnectClient(httpClientHelperProvider);
        var actionIndicatorResultsAPIConnectClient = new Interop.API.Connect.Clients.ActionIndicatorResultsAPIConnectClient(httpClientHelperProvider);

        objectivesAPIConnectClient.GetAsync(objectiveId).ContinueWith(async task =>
        {
            if (task.Exception != null || task.IsCanceled)
            {
                Dispatcher.Dispatch(() =>
                {
                    Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Core.Presentation.Terminal.MAUI.Helpers.Facade.IExceptionHandlingHelperFacade>().HandleException(task.Exception, this);

                    ConcreteBindingContext.Objective = null;
                    ConcreteBindingContext.IsBusy = false;
                });
            }
            else
            {
                try
                {
                    List<ObjectiveIndicatorDisplayModel> objectiveIndicators = [];
                    List<ProyectionIndicatorDisplayModel> proyectionIndicators = [];
                    List<ActionIndicatorDisplayModel> actionIndicators = [];

                    var model = task.Result;

                    foreach (var item in model.Indicators)
                    {
                        objectiveIndicators.Add(item);
                    }

                    foreach (var item in model.Proyections)
                    {
                        foreach (var __item in item.Indicators)
                        {
                            proyectionIndicators.Add(__item);
                        }

                        foreach (var _item in item.Actions)
                        {
                            foreach (var __item in _item.Indicators)
                            {
                                actionIndicators.Add(__item);
                            }
                        }
                    }

                    var objectiveIndicatorsResults = await objectiveIndicatorResultsAPIConnectClient.GetLastsByIndicators([.. objectiveIndicators]);
                    var proyectionIndicatorResults = await proyectionIndicatorResultsAPIConnectClient.GetLastsByIndicators([.. proyectionIndicators]);
                    var actionIndicatorResults = await actionIndicatorResultsAPIConnectClient.GetLastsByIndicators([.. actionIndicators]);

                    Dispatcher.Dispatch(() =>
                    {
                        foreach (var item in objectiveIndicators)
                        {
                            var target = objectiveIndicatorsResults.FirstOrDefault(_model => _model.IndicatorId == item.Id);

                            item.Balance = target?.Amount ?? 0;
                            item.ResultAt = target?.At;
                            item.Context = target?.Description;
                        }

                        foreach (var item in proyectionIndicators)
                        {
                            var target = proyectionIndicatorResults.FirstOrDefault(_model => _model.IndicatorId == item.Id);

                            item.Balance = target?.Amount ?? 0;
                            item.ResultAt = target?.At;
                            item.Context = target?.Description;
                        }

                        foreach (var item in actionIndicators)
                        {
                            var target = actionIndicatorResults.FirstOrDefault(_model => _model.IndicatorId == item.Id);

                            item.Balance = target?.Amount ?? 0;
                            item.ResultAt = target?.At;
                            item.Context = target?.Description;
                        }

                        ConcreteBindingContext.Objective = new Models.View.ObjectiveIndicatorHandlingViewModel(model);
                        ConcreteBindingContext.IsBusy = false;
                    });
                }
                catch
                {
                    Dispatcher.Dispatch(() =>
                    {
                        Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Core.Presentation.Terminal.MAUI.Helpers.Facade.IExceptionHandlingHelperFacade>().HandleException(task.Exception, this);

                        ConcreteBindingContext.Objective = null;
                        ConcreteBindingContext.IsBusy = false;
                    });
                }
            }
        });
    }
}