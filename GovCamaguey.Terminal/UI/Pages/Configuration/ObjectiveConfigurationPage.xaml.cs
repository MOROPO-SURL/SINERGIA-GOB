using Microsoft.Extensions.DependencyInjection;
using MOROPO.Framework.Caney.MVVM.Core.Extensions;
using MOROPO.SINERGIAGOB.Core.Presentation.Terminal.MAUI.UI.Extensions;
using MOROPO.SINERGIAGOB.GovCamaguey.Interop.API.Common.Models.Display;
using MOROPO.SINERGIAGOB.GovCamaguey.Interop.API.Common.Models.Input;
using System.Collections.Generic;
using System.Linq;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Terminal.UI.Pages.Configuration;

public partial class ObjectiveConfigurationPage : Core.Presentation.Terminal.MAUI.UI.Pages.Base.ConfigurationBaseContentPage<ObjectiveDisplayModel, ObjectiveDisplayModel, Models.Page.Configuration.ObjectiveConfigurationPageModel>
{
    private readonly List<ObjectiveIndicatorInputModel> editionIndicators = [];
    private readonly List<ObjectiveIndicatorInputModel> incomingIndicators = [];
    private readonly List<ObjectiveIndicatorDisplayModel> outgoingIndicators = [];

    private readonly List<ProyectionInputModel> editionProyections = [];
    private readonly List<ProyectionInputModel> incomingProyections = [];
    private readonly List<ProyectionDisplayModel> outgoingProyections = [];

    public ObjectiveConfigurationPage(System.Action<ObjectiveDisplayModel> successAction, Models.Page.Configuration.ObjectiveConfigurationPageModel model) : base(successAction, model, new Validators.ObjectiveConfigurationValidator())
    {
        InitializeComponent();
    }

    protected override string EntityTypeName => "Objetivo";

    protected override string CreatorPermissionCode => Interop.API.Common.Constants.Security.Authorization.ObjectiveCreatorPermission;

    protected override string EditorPermissionCode => Interop.API.Common.Constants.Security.Authorization.ObjectiveEditorPermission;

    protected override async System.Threading.Tasks.Task<ObjectiveDisplayModel> PerformCreationAsync()
    {
        var apiConnectClient = new Interop.API.Connect.Clients.ObjectivesAPIConnectClient(Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Framework.Guama.Core.Presentation.API.Connect.Providers.Facade.IHttpClientHelperProviderFacade>());

        var model = new ObjectiveInputModel
        {
            Id = System.Guid.NewGuid(),
            Index = ConcreteBindingContext.Index,
            Name = ConcreteBindingContext.Name,
            ResponsibleId = ConcreteBindingContext.Responsible.Id,
            Description = ConcreteBindingContext.Description,
            IncomingIndicators = incomingIndicators,
            IncomingProyections = incomingProyections
        };

        await apiConnectClient.CreateAsync(model);

        return await apiConnectClient.GetAsync(model.Id);
    }

    protected override async System.Threading.Tasks.Task<ObjectiveDisplayModel> PerformEditionAsync()
    {
        var apiConnectClient = new Interop.API.Connect.Clients.ObjectivesAPIConnectClient(Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Framework.Guama.Core.Presentation.API.Connect.Providers.Facade.IHttpClientHelperProviderFacade>());

        var model = new ObjectiveInputModel
        {
            Id = ConcreteBindingContext.Id,
            Index = ConcreteBindingContext.Index,
            Name = ConcreteBindingContext.Name,
            ResponsibleId = ConcreteBindingContext.Responsible.Id,
            Description = ConcreteBindingContext.Description,
            IncomingIndicators = incomingIndicators,
            EditionIndicators = editionIndicators,
            OutgoingIndicators = outgoingIndicators,
            IncomingProyections = incomingProyections,
            EditionProyections = editionProyections,
            OutgoingProyections = outgoingProyections
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

    private void ResponsiblePickButton_Clicked(object sender, System.EventArgs e)
    {
        if (ConcreteBindingContext.OnEditionMode)
        {
            var user = Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Core.Presentation.Terminal.MAUI.Managers.Facade.ISessionManagerFacade>().CurrentSession.User;

            if (!user.HasFullAccess && !user.Roles.Any(item => item.Permissions.Any(_item => _item.Code == EditorPermissionCode)))
            {
                DisplayAlert("Aviso", "Su usuario no cuenta con permisos para modificar elementos registrados. Póngase en contacto con los administradores del sistema para solicitar los permisos requeridos.", "Aceptar");

                return;
            }
        }

        this.NavigationPushModalPageAsync(new Microsoft.Maui.Controls.NavigationPage(new Pick.PersonPickPage(ResponsiblePickComplete_Callback)));
    }

    private void IndicatorAgregationButton_Clicked(object sender, System.EventArgs e)
    {
        var user = Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Core.Presentation.Terminal.MAUI.Managers.Facade.ISessionManagerFacade>().CurrentSession.User;

        if (ConcreteBindingContext.OnEditionMode)
        {
            if (!user.HasFullAccess && !user.Roles.Any(item => item.Permissions.Any(_item => _item.Code == EditorPermissionCode)))
            {
                DisplayAlert("Aviso", "Su usuario no cuenta con permisos para modificar elementos registrados. Póngase en contacto con los administradores del sistema para solicitar los permisos requeridos.", "Aceptar");

                return;
            }
        }

        if (!user.HasFullAccess && !user.Roles.Any(item => item.Permissions.Any(_item => _item.Code == Interop.API.Common.Constants.Security.Authorization.ObjectiveIndicatorCreatorPermission)))
        {
            DisplayAlert("Aviso", "Su usuario no cuenta con permisos para agregar nuevos elementos de este tipo. Póngase en contacto con los administradores del sistema para solicitar los permisos requeridos.", "Aceptar");

            return;
        }

        ConcreteBindingContext.IsBusy = true;

        System.Threading.Tasks.Task.Run(async () =>
        {
            try
            {
                var unitMeasurementsAPIConnectClient = new Interop.API.Connect.Clients.UnitMeasurementsAPIConnectClient(Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Framework.Guama.Core.Presentation.API.Connect.Providers.Facade.IHttpClientHelperProviderFacade>());

                var model = new Models.Page.Configuration.ObjectiveIndicatorConfigurationPageModel(null)
                {
                    AvailableUnitMeasurements = await unitMeasurementsAPIConnectClient.GetEnabledAsync()
                };

                Dispatcher.Dispatch(() =>
                {
                    ConcreteBindingContext.IsBusy = false;

                    this.NavigationPushPageAsync(new ObjectiveIndicatorConfigurationPage(IndicatorCreateSuccess_Callback, model));
                });
            }
            catch (System.Exception exception)
            {
                Dispatcher.Dispatch(() =>
                {
                    Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Core.Presentation.Terminal.MAUI.Helpers.Facade.IExceptionHandlingHelperFacade>().HandleException(exception, this);
                });
            }
        });
    }

    private void IndicatorEditionButton_Clicked(object sender, System.EventArgs e)
    {
        var user = Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Core.Presentation.Terminal.MAUI.Managers.Facade.ISessionManagerFacade>().CurrentSession.User;

        if (ConcreteBindingContext.OnEditionMode)
        {
            if (!user.HasFullAccess && !user.Roles.Any(item => item.Permissions.Any(_item => _item.Code == EditorPermissionCode)))
            {
                DisplayAlert("Aviso", "Su usuario no cuenta con permisos para modificar elementos registrados. Póngase en contacto con los administradores del sistema para solicitar los permisos requeridos.", "Aceptar");

                return;
            }
        }

        if (!user.HasFullAccess && !user.Roles.Any(item => item.Permissions.Any(_item => _item.Code == Interop.API.Common.Constants.Security.Authorization.ObjectiveIndicatorEditorPermission)))
        {
            DisplayAlert("Aviso", "Su usuario no cuenta con permisos para modificar elementos registrados de este tipo. Póngase en contacto con los administradores del sistema para solicitar los permisos requeridos.", "Aceptar");

            return;
        }

        var target = ((Microsoft.Maui.Controls.BindableObject)sender).BindingContext as ObjectiveIndicatorDisplayModel;

        ConcreteBindingContext.IsBusy = true;

        System.Threading.Tasks.Task.Run(async () =>
        {
            try
            {
                var model = incomingIndicators.FirstOrDefault(_model => _model.Id == target.Id) ?? editionIndicators.FirstOrDefault(_model => _model.Id == target.Id);

                model ??= new ObjectiveIndicatorInputModel
                {
                    Id = target.Id,
                    Name = target.Name,
                    Amount = target.Amount,
                    Priority = target.Priority,
                    Description = target.Description,
                    UnitMeasurementId = target.UnitMeasurement.Id
                };

                var unitMeasurementsAPIConnectClient = new Interop.API.Connect.Clients.UnitMeasurementsAPIConnectClient(Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Framework.Guama.Core.Presentation.API.Connect.Providers.Facade.IHttpClientHelperProviderFacade>());

                var configurationModel = new Models.Page.Configuration.ObjectiveIndicatorConfigurationPageModel(model, target.MontlyPlans)
                {
                    AvailableUnitMeasurements = await unitMeasurementsAPIConnectClient.GetEnabledAsync()
                };

                Dispatcher.Dispatch(() =>
                {
                    ConcreteBindingContext.IsBusy = false;

                    this.NavigationPushPageAsync(new ObjectiveIndicatorConfigurationPage(IndicatorEditSuccess_Callback, configurationModel, model.EditionMontlyPlans, model.IncomingMontlyPlans, model.OutgoingMontlyPlans));
                });
            }
            catch (System.Exception exception)
            {
                Dispatcher.Dispatch(() =>
                {
                    Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Core.Presentation.Terminal.MAUI.Helpers.Facade.IExceptionHandlingHelperFacade>().HandleException(exception, this);
                });
            }
        });
    }

    private void IndicatorRemotionButton_Clicked(object sender, System.EventArgs e)
    {
        var user = Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Core.Presentation.Terminal.MAUI.Managers.Facade.ISessionManagerFacade>().CurrentSession.User;

        if (ConcreteBindingContext.OnEditionMode)
        {
            if (!user.HasFullAccess && !user.Roles.Any(item => item.Permissions.Any(_item => _item.Code == EditorPermissionCode)))
            {
                DisplayAlert("Aviso", "Su usuario no cuenta con permisos para modificar elementos registrados. Póngase en contacto con los administradores del sistema para solicitar los permisos requeridos.", "Aceptar");

                return;
            }
        }

        if (!user.HasFullAccess && !user.Roles.Any(item => item.Permissions.Any(_item => _item.Code == Interop.API.Common.Constants.Security.Authorization.ObjectiveIndicatorRemoverPermission)))
        {
            DisplayAlert("Aviso", "Su usuario no cuenta con permisos para retirar elementos registrados de este tipo. Póngase en contacto con los administradores del sistema para solicitar los permisos requeridos.", "Aceptar");

            return;
        }

        DisplayAlert("Confirmación", "Está seguro que desea retirar este indicador?", "Confirmar", "Cancelar").ContinueWith(task =>
        {
            if (!task.Result) return;

            Dispatcher.Dispatch(() =>
            {
                var target = ((Microsoft.Maui.Controls.BindableObject)sender).BindingContext as ObjectiveIndicatorDisplayModel;

                var handled = false;

                foreach (var item in incomingIndicators)
                {
                    if (item.Id != target.Id) continue;

                    incomingIndicators.Remove(incomingIndicators.First(model => model.Id == target.Id));

                    handled = true;

                    break;
                }

                if (!handled)
                {
                    foreach (var item in editionIndicators)
                    {
                        if (item.Id != target.Id) continue;

                        editionIndicators.Remove(editionIndicators.First(model => model.Id == target.Id));

                        break;
                    }
                }

                if (!handled)
                {
                    outgoingIndicators.Add(target);
                }

                ConcreteBindingContext.Indicators.Remove(target);

                hasChangesFlag = true;
            });
        });
    }

    private void ProyectionAgregationButton_Clicked(object sender, System.EventArgs e)
    {
        var user = Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Core.Presentation.Terminal.MAUI.Managers.Facade.ISessionManagerFacade>().CurrentSession.User;

        if (ConcreteBindingContext.OnEditionMode)
        {
            if (!user.HasFullAccess && !user.Roles.Any(item => item.Permissions.Any(_item => _item.Code == EditorPermissionCode)))
            {
                DisplayAlert("Aviso", "Su usuario no cuenta con permisos para modificar elementos registrados. Póngase en contacto con los administradores del sistema para solicitar los permisos requeridos.", "Aceptar");

                return;
            }
        }

        if (!user.HasFullAccess && !user.Roles.Any(item => item.Permissions.Any(_item => _item.Code == Interop.API.Common.Constants.Security.Authorization.ProyectionCreatorPermission)))
        {
            DisplayAlert("Aviso", "Su usuario no cuenta con permisos para agregar nuevos elementos de este tipo. Póngase en contacto con los administradores del sistema para solicitar los permisos requeridos.", "Aceptar");

            return;
        }

        this.NavigationPushPageAsync(new ProyectionConfigurationPage(ProyectionCreateSuccess_Callback, new Models.Page.Configuration.ProyectionConfigurationPageModel(null)));
    }

    private void ProyectionEditionButton_Clicked(object sender, System.EventArgs e)
    {
        var user = Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Core.Presentation.Terminal.MAUI.Managers.Facade.ISessionManagerFacade>().CurrentSession.User;

        var target = ((Microsoft.Maui.Controls.BindableObject)sender).BindingContext as ProyectionDisplayModel;

        if (!IsPermissionByPassForChildModeration(target, user))
        {
            if (!user.HasFullAccess && !user.Roles.Any(item => item.Permissions.Any(_item => _item.Code == Interop.API.Common.Constants.Security.Authorization.ProyectionEditorPermission)))
            {
                DisplayAlert("Aviso", "Su usuario no cuenta con permisos para modificar elementos registrados de este tipo. Póngase en contacto con los administradores del sistema para solicitar los permisos requeridos.", "Aceptar");

                return;
            }

            if (!target.IsModerable)
            {
                DisplayAlert("Aviso", "Su usuario no ha sido asiganado como moderador de este objetivo específico, por lo que no podrá realizar operaciones sobre el mismo. Contacte con los administradores del sistema ante cualquier inquietud al respecto.", "Aceptar");

                return;
            }
        }

        var model = incomingProyections.FirstOrDefault(_model => _model.Id == target.Id) ?? editionProyections.FirstOrDefault(_model => _model.Id == target.Id);

        model ??= new ProyectionInputModel
        {
            Id = target.Id,
            Index = target.Index,
            Name = target.Name,
            Description = target.Description,
            Guidelines = target.Guidelines,
            ResponsabilityOrganizations = target.ResponsabilityOrganizations,
            ParticipationOrganizations = target.ParticipationOrganizations
        };
        
        this.NavigationPushPageAsync(new ProyectionConfigurationPage(ProyectionEditSuccess_Callback, new Models.Page.Configuration.ProyectionConfigurationPageModel(model, target.Indicators, target.Actions, target.MonthlyClosures), model.EditionIndicators, model.IncomingIndicators, model.OutgoingIndicators, model.EditionActions, model.IncomingActions, model.OutgoingActions, model.EditionMonthlyClosures, model.IncomingMonthlyClosures, model.OutgoingMonthlyClosures));
    }

    private bool IsPermissionByPassForChildModeration(ProyectionDisplayModel target, Core.Interop.API.Common.Models.Display.UserDisplayModel user)
    {
        if (ConcreteBindingContext.OnEditionMode && target.Actions.Any(_item => _item.IsModerable) && (user.HasFullAccess || user.Roles.Any(item => item.Permissions.Any(_item => _item.Code == Interop.API.Common.Constants.Security.Authorization.ActionEditorPermission)))) return true;

        return false;
    }

    private void ProyectionRemotionButton_Clicked(object sender, System.EventArgs e)
    {
        var user = Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Core.Presentation.Terminal.MAUI.Managers.Facade.ISessionManagerFacade>().CurrentSession.User;

        if (ConcreteBindingContext.OnEditionMode)
        {
            if (!user.HasFullAccess && !user.Roles.Any(item => item.Permissions.Any(_item => _item.Code == EditorPermissionCode)))
            {
                DisplayAlert("Aviso", "Su usuario no cuenta con permisos para modificar elementos registrados. Póngase en contacto con los administradores del sistema para solicitar los permisos requeridos.", "Aceptar");

                return;
            }
        }

        if (!user.HasFullAccess && !user.Roles.Any(item => item.Permissions.Any(_item => _item.Code == Interop.API.Common.Constants.Security.Authorization.ProyectionRemoverPermission)))
        {
            DisplayAlert("Aviso", "Su usuario no cuenta con permisos para retirar elementos registrados de este tipo. Póngase en contacto con los administradores del sistema para solicitar los permisos requeridos.", "Aceptar");

            return;
        }

        var target = ((Microsoft.Maui.Controls.BindableObject)sender).BindingContext as ProyectionDisplayModel;

        if (!target.IsModerable)
        {
            DisplayAlert("Aviso", "Su usuario no ha sido asiganado como moderador de este objetivo específico, por lo que no podrá realizar operaciones sobre el mismo. Contacte con los administradores del sistema ante cualquier inquietud al respecto.", "Aceptar");

            return;
        }

        DisplayAlert("Confirmación", "Está seguro que desea retirar esta proyección?", "Confirmar", "Cancelar").ContinueWith(task =>
        {
            if (!task.Result) return;

            Dispatcher.Dispatch(() =>
            {
                var handled = false;

                foreach (var item in incomingProyections)
                {
                    if (item.Id != target.Id) continue;

                    incomingProyections.Remove(incomingProyections.First(model => model.Id == target.Id));

                    handled = true;

                    break;
                }

                if (!handled)
                {
                    foreach (var item in editionProyections)
                    {
                        if (item.Id != target.Id) continue;

                        editionProyections.Remove(editionProyections.First(model => model.Id == target.Id));

                        break;
                    }
                }

                if (!handled)
                {
                    outgoingProyections.Add(target);
                }

                ConcreteBindingContext.Proyections.Remove(target);

                hasChangesFlag = true;
            });
        });
    }

    private void ProyectionCreateSuccess_Callback(ProyectionInputModel model)
    {
        var user = Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Core.Presentation.Terminal.MAUI.Managers.Facade.ISessionManagerFacade>().CurrentSession.User;

        incomingProyections.Add(model);

        hasChangesFlag = true;

        ConcreteBindingContext.Proyections.Add(new ProyectionDisplayModel
        {
            Id = model.Id,
            IsModerable = true,
            Index = model.Index,
            Name = model.Name,
            Description = model.Description,
            Guidelines = model.Guidelines,
            ResponsabilityOrganizations = model.ResponsabilityOrganizations,
            ParticipationOrganizations = model.ParticipationOrganizations
        });
    }

    private void ProyectionEditSuccess_Callback(ProyectionInputModel model)
    {
        var user = Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Core.Presentation.Terminal.MAUI.Managers.Facade.ISessionManagerFacade>().CurrentSession.User;

        if (ConcreteBindingContext.OnEditionMode && !user.HasFullAccess && !user.Roles.Any(item => item.Permissions.Any(_item => _item.Code == Interop.API.Common.Constants.Security.Authorization.ProyectionEditorPermission)))
        {
            ConcreteBindingContext.IsBusy = true;

            var apiConnectClient = new Interop.API.Connect.Clients.ProyectionsAPIConnectClient(Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Framework.Guama.Core.Presentation.API.Connect.Providers.Facade.IHttpClientHelperProviderFacade>());

            apiConnectClient.EditAsync(model).ContinueWith(task =>
            {
                Dispatcher.Dispatch(() =>
                {
                    ConcreteBindingContext.IsBusy = false;

                    if (task.IsFaulted || task.IsCanceled)
                    {
                        HandleException(task.Exception);
                    }
                    else
                    {
                        this.PresentToastNotification(Core.Presentation.Terminal.MAUI.Enumerations.NotificationKind.Success, "Cambio aplicado con éxito.");

                        Navigation.PopToRootAsync().ContinueWith(_ =>
                        {
                            Microsoft.Maui.Controls.Application.Current.Dispatcher.Dispatch(() =>
                            {
                                ((Microsoft.Maui.Controls.Application.Current.MainPage as Microsoft.Maui.Controls.Shell).CurrentPage as Microsoft.Maui.Controls.ContentPage).PresentToastNotification(Core.Presentation.Terminal.MAUI.Enumerations.NotificationKind.Success, "Cambio aplicado con éxito.");
                            });
                        });
                    }
                });
            });
        }
        else
        {
            var handled = false;

            foreach (var item in incomingProyections)
            {
                if (item.Id != model.Id) continue;

                item.Index = model.Index;
                item.Name = model.Name;
                item.Description = model.Description;
                item.Guidelines = model.Guidelines;
                item.ResponsabilityOrganizations = model.ResponsabilityOrganizations;
                item.ParticipationOrganizations = model.ParticipationOrganizations;
                item.IncomingActions = model.IncomingActions;

                handled = true;

                break;
            }

            if (!handled)
            {
                foreach (var item in editionProyections)
                {
                    if (item.Id != model.Id) continue;

                    item.Index = model.Index;
                    item.Name = model.Name;
                    item.Description = model.Description;
                    item.Guidelines = model.Guidelines;
                    item.ResponsabilityOrganizations = model.ResponsabilityOrganizations;
                    item.ParticipationOrganizations = model.ParticipationOrganizations;
                    item.IncomingActions = model.IncomingActions;
                    item.EditionActions = model.EditionActions;
                    item.OutgoingActions = model.OutgoingActions;

                    handled = true;

                    break;
                }
            }

            if (!handled)
            {
                editionProyections.Add(model);
            }

            hasChangesFlag = true;
        }

        var index = -1;

        foreach (var item in ConcreteBindingContext.Proyections)
        {
            index++;

            if (item.Id != model.Id) continue;

            ConcreteBindingContext.Proyections[index] = new ProyectionDisplayModel
            {
                Id = model.Id,
                IsModerable = item.IsModerable,
                Index = model.Index,
                Name = model.Name,
                Description = model.Description,
                Guidelines = model.Guidelines,
                ResponsabilityOrganizations = model.ResponsabilityOrganizations,
                ParticipationOrganizations = model.ParticipationOrganizations
            };

            break;
        }
    }

    private void ResponsiblePickComplete_Callback(PersonDisplayModel model)
    {
        ConcreteBindingContext.Responsible = model;
    }

    private void IndicatorCreateSuccess_Callback(ObjectiveIndicatorInputModel model)
    {
        incomingIndicators.Add(model);

        ConcreteBindingContext.Indicators.Add(new ObjectiveIndicatorDisplayModel
        {
            Id = model.Id,
            Priority = model.Priority,
            Name = model.Name,
            Description = model.Description,
            Amount = model.Amount,
            UnitMeasurement = new UnitMeasurementDisplayModel { Id = model.UnitMeasurementId }
        });

        hasChangesFlag = true;
    }

    private void IndicatorEditSuccess_Callback(ObjectiveIndicatorInputModel model)
    {
        var handled = false;

        foreach (var item in incomingIndicators)
        {
            if (item.Id != model.Id) continue;

            item.Priority = model.Priority;
            item.Name = model.Name;
            item.Description = model.Description;
            item.Amount = model.Amount;
            item.UnitMeasurementId = model.UnitMeasurementId;

            handled = true;

            break;
        }

        if (!handled)
        {
            foreach (var item in editionIndicators)
            {
                if (item.Id != model.Id) continue;

                item.Priority = model.Priority;
                item.Name = model.Name;
                item.Description = model.Description;
                item.Amount = model.Amount;
                item.UnitMeasurementId = model.UnitMeasurementId;

                handled = true;

                break;
            }
        }

        if (!handled)
        {
            editionIndicators.Add(model);
        }

        var index = -1;

        foreach (var item in ConcreteBindingContext.Indicators)
        {
            index++;

            if (item.Id != model.Id) continue;

            ConcreteBindingContext.Indicators[index] = new ObjectiveIndicatorDisplayModel
            {
                Id = model.Id,
                Priority = model.Priority,
                Name = model.Name,
                Description = model.Description,
                Amount = model.Amount,
                UnitMeasurement = new UnitMeasurementDisplayModel { Id = model.UnitMeasurementId }
            };

            break;
        }

        hasChangesFlag = true;
    }
}