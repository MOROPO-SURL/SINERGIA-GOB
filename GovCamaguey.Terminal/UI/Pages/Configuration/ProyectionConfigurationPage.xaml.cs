using Microsoft.Extensions.DependencyInjection;
using MOROPO.Framework.Caney.MVVM.Core.Extensions;
using MOROPO.SINETGIAGOB.Core.Presentation.Terminal.MAUI.UI.Extensions;
using MOROPO.SINETGIAGOB.GovCamaguey.Interop.API.Common.Models.Display;
using MOROPO.SINETGIAGOB.GovCamaguey.Interop.API.Common.Models.Input;
using System.Collections.Generic;
using System.Linq;

namespace MOROPO.SINETGIAGOB.GovCamaguey.Terminal.UI.Pages.Configuration;

public partial class ProyectionConfigurationPage : Core.Presentation.Terminal.MAUI.UI.Pages.Base.ConfigurationBaseContentPage<ProyectionInputModel, ProyectionInputModel, Models.Page.Configuration.ProyectionConfigurationPageModel>
{
    private readonly List<ProyectionIndicatorInputModel> editionIndicators = [];
    private readonly List<ProyectionIndicatorInputModel> incomingIndicators = [];
    private readonly List<ProyectionIndicatorDisplayModel> outgoingIndicators = [];

    private readonly List<ActionInputModel> editionActions = [];
    private readonly List<ActionInputModel> incomingActions = [];
    private readonly List<ActionDisplayModel> outgoingActions = [];

    public ProyectionConfigurationPage(System.Action<ProyectionInputModel> successAction, Models.Page.Configuration.ProyectionConfigurationPageModel model) : base(successAction, model, new Validators.ProyectionConfigurationValidator())
    {
        InitializeComponent();
    }

    public ProyectionConfigurationPage(
        System.Action<ProyectionInputModel> successAction, 
        Models.Page.Configuration.ProyectionConfigurationPageModel model,
        List<ProyectionIndicatorInputModel> editionIndicators,
        List<ProyectionIndicatorInputModel> incomingIndicators,
        List<ProyectionIndicatorDisplayModel> outgoingIndicators,
        List<ActionInputModel> editionActions, 
        List<ActionInputModel> incomingActions, 
        List<ActionDisplayModel> outgoingActions) 
        : this(successAction, model)
    {
        this.editionIndicators = editionIndicators;
        this.incomingIndicators = incomingIndicators;
        this.outgoingIndicators = outgoingIndicators;

        this.editionActions = editionActions;
        this.incomingActions = incomingActions;
        this.outgoingActions = outgoingActions;
    }

    protected override string EntityTypeName => "Objetivo Específico";

    protected override string CreatorPermissionCode => Interop.API.Common.Constants.Security.Authorization.ProyectionCreatorPermission;

    protected override string EditorPermissionCode => Interop.API.Common.Constants.Security.Authorization.ProyectionEditorPermission;

    protected override System.Threading.Tasks.Task<ProyectionInputModel> PerformCreationAsync()
    {
        return System.Threading.Tasks.Task.FromResult(new ProyectionInputModel
        {
            Id = System.Guid.NewGuid(),
            Index = ConcreteBindingContext.Index,
            Name = ConcreteBindingContext.Name,
            Description = ConcreteBindingContext.Description,
            Guidelines = [.. ConcreteBindingContext.Guidelines],
            ResponsabilityOrganizations = [.. ConcreteBindingContext.ResponsabilityOrganizations],
            ParticipationOrganizations = [.. ConcreteBindingContext.ParticipationOrganizations],
            IncomingIndicators = incomingIndicators,
            IncomingActions = incomingActions
        });
    }

    protected override System.Threading.Tasks.Task<ProyectionInputModel> PerformEditionAsync()
    {
        return System.Threading.Tasks.Task.FromResult(new ProyectionInputModel
        {
            Id = ConcreteBindingContext.Id,
            Index = ConcreteBindingContext.Index,
            Name = ConcreteBindingContext.Name,
            Description = ConcreteBindingContext.Description,
            Guidelines = [.. ConcreteBindingContext.Guidelines],
            ResponsabilityOrganizations = [.. ConcreteBindingContext.ResponsabilityOrganizations],
            ParticipationOrganizations = [.. ConcreteBindingContext.ParticipationOrganizations],
            IncomingIndicators = incomingIndicators,
            EditionIndicators = editionIndicators,
            OutgoingIndicators = outgoingIndicators,
            IncomingActions = incomingActions,
            EditionActions = editionActions,
            OutgoingActions = outgoingActions
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

        if (!user.HasFullAccess && !user.Roles.Any(item => item.Permissions.Any(_item => _item.Code == Interop.API.Common.Constants.Security.Authorization.ProyectionIndicatorCreatorPermission)))
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

                var model = new Models.Page.Configuration.ProyectionIndicatorConfigurationPageModel(null)
                {
                    AvailableUnitMeasurements = await unitMeasurementsAPIConnectClient.GetEnabledAsync()
                };

                Dispatcher.Dispatch(() =>
                {
                    ConcreteBindingContext.IsBusy = false;

                    this.NavigationPushPageAsync(new ProyectionIndicatorConfigurationPage(IndicatorCreateSuccess_Callback, model));
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

        if (!user.HasFullAccess && !user.Roles.Any(item => item.Permissions.Any(_item => _item.Code == Interop.API.Common.Constants.Security.Authorization.ProyectionIndicatorEditorPermission)))
        {
            DisplayAlert("Aviso", "Su usuario no cuenta con permisos para modificar elementos registrados de este tipo. Póngase en contacto con los administradores del sistema para solicitar los permisos requeridos.", "Aceptar");

            return;
        }

        var target = ((Microsoft.Maui.Controls.BindableObject)sender).BindingContext as ProyectionIndicatorDisplayModel;

        ConcreteBindingContext.IsBusy = true;

        System.Threading.Tasks.Task.Run(async () =>
        {
            try
            {
                var model = incomingIndicators.FirstOrDefault(_model => _model.Id == target.Id) ?? editionIndicators.FirstOrDefault(_model => _model.Id == target.Id);

                model ??= new ProyectionIndicatorInputModel
                {
                    Id = target.Id,
                    Name = target.Name,
                    Amount = target.Amount,
                    Priority = target.Priority,
                    Description = target.Description,
                    UnitMeasurementId = target.UnitMeasurement.Id
                };

                var unitMeasurementsAPIConnectClient = new Interop.API.Connect.Clients.UnitMeasurementsAPIConnectClient(Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Framework.Guama.Core.Presentation.API.Connect.Providers.Facade.IHttpClientHelperProviderFacade>());

                var configurationModel = new Models.Page.Configuration.ProyectionIndicatorConfigurationPageModel(model, target.MontlyPlans)
                {
                    AvailableUnitMeasurements = await unitMeasurementsAPIConnectClient.GetEnabledAsync()
                };

                Dispatcher.Dispatch(() =>
                {
                    ConcreteBindingContext.IsBusy = false;

                    this.NavigationPushPageAsync(new ProyectionIndicatorConfigurationPage(IndicatorEditSuccess_Callback, configurationModel, model.EditionMontlyPlans, model.IncomingMontlyPlans, model.OutgoingMontlyPlans));
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

        if (!user.HasFullAccess && !user.Roles.Any(item => item.Permissions.Any(_item => _item.Code == Interop.API.Common.Constants.Security.Authorization.ProyectionIndicatorRemoverPermission)))
        {
            DisplayAlert("Aviso", "Su usuario no cuenta con permisos para retirar elementos registrados de este tipo. Póngase en contacto con los administradores del sistema para solicitar los permisos requeridos.", "Aceptar");

            return;
        }

        DisplayAlert("Confirmación", "Está seguro que desea retirar este indicador?", "Confirmar", "Cancelar").ContinueWith(task =>
        {
            if (!task.Result) return;

            Dispatcher.Dispatch(() =>
            {
                var target = ((Microsoft.Maui.Controls.BindableObject)sender).BindingContext as ProyectionIndicatorDisplayModel;

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

    private void ActionAgregationButton_Clicked(object sender, System.EventArgs e)
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

        if (!user.HasFullAccess && !user.Roles.Any(item => item.Permissions.Any(_item => _item.Code == Interop.API.Common.Constants.Security.Authorization.ActionCreatorPermission)))
        {
            DisplayAlert("Aviso", "Su usuario no cuenta con permisos para agregar nuevos elementos de este tipo. Póngase en contacto con los administradores del sistema para solicitar los permisos requeridos.", "Aceptar");

            return;
        }

        this.NavigationPushPageAsync(new ActionConfigurationPage(ActionCreateSuccess_Callback, new Models.Page.Configuration.ActionConfigurationPageModel(null)));
    }

    private void ActionEditionButton_Clicked(object sender, System.EventArgs e)
    {
        var user = Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Core.Presentation.Terminal.MAUI.Managers.Facade.ISessionManagerFacade>().CurrentSession.User;

        var target = ((Microsoft.Maui.Controls.BindableObject)sender).BindingContext as ActionDisplayModel;

        if (!user.HasFullAccess && !user.Roles.Any(item => item.Permissions.Any(_item => _item.Code == Interop.API.Common.Constants.Security.Authorization.ActionEditorPermission)))
        {
            DisplayAlert("Aviso", "Su usuario no cuenta con permisos para modificar elementos registrados de este tipo. Póngase en contacto con los administradores del sistema para solicitar los permisos requeridos.", "Aceptar");

            return;
        }

        if (!target.IsModerable)
        {
            DisplayAlert("Aviso", "Su usuario no ha sido asiganado como moderador de esta acción, por lo que no podrá realizar operaciones sobre el mismo. Contacte con los administradores del sistema ante cualquier inquietud al respecto.", "Aceptar");

            return;
        }

        var model = incomingActions.FirstOrDefault(_model => _model.Id == target.Id) ?? editionActions.FirstOrDefault(_model => _model.Id == target.Id);

        model ??= new ActionInputModel
        {
            Id = target.Id,
            Index = target.Index,
            Name = target.Name,
            Description = target.Description,
            ResponsabilityOrganizations = target.ResponsabilityOrganizations,
            ParticipationOrganizations = target.ParticipationOrganizations
        };
        
        this.NavigationPushPageAsync(new ActionConfigurationPage(ActionEditSuccess_Callback, new Models.Page.Configuration.ActionConfigurationPageModel(model, target.Indicators, target.MonthlyClosures), model.EditionIndicators, model.IncomingIndicators, model.OutgoingIndicators, model.EditionMonthlyClosures, model.IncomingMonthlyClosures, model.OutgoingMonthlyClosures));
    }

    private void ActionRemotionButton_Clicked(object sender, System.EventArgs e)
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

        if (!user.HasFullAccess && !user.Roles.Any(item => item.Permissions.Any(_item => _item.Code == Interop.API.Common.Constants.Security.Authorization.ActionRemoverPermission)))
        {
            DisplayAlert("Aviso", "Su usuario no cuenta con permisos para retirar elementos registrados de este tipo. Póngase en contacto con los administradores del sistema para solicitar los permisos requeridos.", "Aceptar");

            return;
        }

        var target = ((Microsoft.Maui.Controls.BindableObject)sender).BindingContext as ActionDisplayModel;

        if (!target.IsModerable)
        {
            DisplayAlert("Aviso", "Su usuario no ha sido asiganado como moderador de esta acción, por lo que no podrá realizar operaciones sobre el mismo. Contacte con los administradores del sistema ante cualquier inquietud al respecto.", "Aceptar");

            return;
        }

        DisplayAlert("Confirmación", "Está seguro que desea retirar esta proyección?", "Confirmar", "Cancelar").ContinueWith(task =>
        {
            if (!task.Result) return;

            Dispatcher.Dispatch(() =>
            {
                var handled = false;

                foreach (var item in incomingActions)
                {
                    if (item.Id != target.Id) continue;

                    incomingActions.Remove(incomingActions.First(model => model.Id == target.Id));

                    handled = true;

                    break;
                }

                if (!handled)
                {
                    foreach (var item in editionActions)
                    {
                        if (item.Id != target.Id) continue;

                        editionActions.Remove(editionActions.First(model => model.Id == target.Id));

                        break;
                    }
                }

                if (!handled)
                {
                    outgoingActions.Add(target);
                }

                ConcreteBindingContext.Actions.Remove(target);

                hasChangesFlag = true;
            });
        });
    }

    private void AddOrganizationParticipationButton_Clicked(object sender, System.EventArgs e)
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

        this.NavigationPushModalPageAsync(new Microsoft.Maui.Controls.NavigationPage(new Pick.OrganizationPickPage(ParticipationOrganizationPickComplete_Callback)));
    }

    private void AddOrganizationResponsabilityButton_Clicked(object sender, System.EventArgs e)
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

        this.NavigationPushModalPageAsync(new Microsoft.Maui.Controls.NavigationPage(new Pick.OrganizationPickPage(ResponsabilityOrganizationPickComplete_Callback)));
    }

    private void AddGuidelineButton_Clicked(object sender, System.EventArgs e)
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

        this.NavigationPushModalPageAsync(new Microsoft.Maui.Controls.NavigationPage(new Pick.GuidelinePickPage(GuidelinePickComplete_Callback)));
    }

    private void ParticipationOrganizationRemoveButton_Clicked(object sender, System.EventArgs e)
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

        DisplayAlert("Confirmación", "Está seguro que desea retirar este organismo participante?", "Confirmar", "Cancelar").ContinueWith(task =>
        {
            if (!task.Result) return;

            Dispatcher.Dispatch(() =>
            {
                ConcreteBindingContext.ParticipationOrganizations.Remove(((Microsoft.Maui.Controls.BindableObject)sender).BindingContext as OrganizationDisplayModel);

                hasChangesFlag = true;
            });
        });
    }

    private void ResponsabilityOrganizationRemoveButton_Clicked(object sender, System.EventArgs e)
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

        DisplayAlert("Confirmación", "Está seguro que desea retirar este organismo responsable?", "Confirmar", "Cancelar").ContinueWith(task =>
        {
            if (!task.Result) return;

            Dispatcher.Dispatch(() =>
            {
                ConcreteBindingContext.ResponsabilityOrganizations.Remove(((Microsoft.Maui.Controls.BindableObject)sender).BindingContext as OrganizationDisplayModel);

                hasChangesFlag = true;
            });
        });
    }

    private void GuidelineRemoveButton_Clicked(object sender, System.EventArgs e)
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

        DisplayAlert("Confirmación", "Está seguro que desea retirar este lineamiento?", "Confirmar", "Cancelar").ContinueWith(task =>
        {
            if (!task.Result) return;

            Dispatcher.Dispatch(() =>
            {
                ConcreteBindingContext.Guidelines.Remove(((Microsoft.Maui.Controls.BindableObject)sender).BindingContext as GuidelineDisplayModel);

                hasChangesFlag = true;
            });
        });
    }

    private void ActionCreateSuccess_Callback(ActionInputModel model)
    {
        incomingActions.Add(model);

        ConcreteBindingContext.Actions.Add(new ActionDisplayModel
        {
            Id = model.Id,
            IsModerable = true,
            Index = model.Index,
            Name = model.Name,
            Description = model.Description,
            ResponsabilityOrganizations = model.ResponsabilityOrganizations,
            ParticipationOrganizations = model.ParticipationOrganizations
        });

        hasChangesFlag = true;
    }

    private void ActionEditSuccess_Callback(ActionInputModel model)
    {
        var user = Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Core.Presentation.Terminal.MAUI.Managers.Facade.ISessionManagerFacade>().CurrentSession.User;

        if (ConcreteBindingContext.OnEditionMode && !user.HasFullAccess && !user.Roles.Any(item => item.Permissions.Any(_item => _item.Code == Interop.API.Common.Constants.Security.Authorization.ProyectionEditorPermission)))
        {
            ConcreteBindingContext.IsBusy = true;

            var apiConnectClient = new Interop.API.Connect.Clients.ActionsAPIConnectClient(Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Framework.Guama.Core.Presentation.API.Connect.Providers.Facade.IHttpClientHelperProviderFacade>());

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

            foreach (var item in incomingActions)
            {
                if (item.Id != model.Id) continue;

                item.Index = model.Index;
                item.Name = model.Name;
                item.Description = model.Description;
                item.ResponsabilityOrganizations = model.ResponsabilityOrganizations;
                item.ParticipationOrganizations = model.ParticipationOrganizations;
                item.IncomingIndicators = model.IncomingIndicators;

                handled = true;

                break;
            }

            if (!handled)
            {
                foreach (var item in editionActions)
                {
                    if (item.Id != model.Id) continue;

                    item.Index = model.Index;
                    item.Name = model.Name;
                    item.Description = model.Description;
                    item.ResponsabilityOrganizations = model.ResponsabilityOrganizations;
                    item.ParticipationOrganizations = model.ParticipationOrganizations;
                    item.IncomingIndicators = model.IncomingIndicators;
                    item.EditionIndicators = model.EditionIndicators;
                    item.OutgoingIndicators = model.OutgoingIndicators;

                    handled = true;

                    break;
                }
            }

            if (!handled)
            {
                editionActions.Add(model);
            }

            var index = -1;

            foreach (var item in ConcreteBindingContext.Actions)
            {
                index++;

                if (item.Id != model.Id) continue;

                ConcreteBindingContext.Actions[index] = new ActionDisplayModel
                {
                    Id = model.Id,
                    IsModerable = item.IsModerable,
                    Index = model.Index,
                    Name = model.Name,
                    Description = model.Description,
                    ResponsabilityOrganizations = model.ResponsabilityOrganizations,
                    ParticipationOrganizations = model.ParticipationOrganizations
                };

                break;
            }

            hasChangesFlag = true;
        }
    }

    private void ParticipationOrganizationPickComplete_Callback(OrganizationDisplayModel model)
    {
        ConcreteBindingContext.ParticipationOrganizations.Add(model);

        hasChangesFlag = true;
    }

    private void ResponsabilityOrganizationPickComplete_Callback(OrganizationDisplayModel model)
    {
        ConcreteBindingContext.ResponsabilityOrganizations.Add(model);

        hasChangesFlag = true;
    }

    private void GuidelinePickComplete_Callback(GuidelineDisplayModel model)
    {
        ConcreteBindingContext.Guidelines.Add(model);

        hasChangesFlag = true;
    }

    private void IndicatorCreateSuccess_Callback(ProyectionIndicatorInputModel model)
    {
        incomingIndicators.Add(model);

        ConcreteBindingContext.Indicators.Add(new ProyectionIndicatorDisplayModel
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

    private void IndicatorEditSuccess_Callback(ProyectionIndicatorInputModel model)
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

            ConcreteBindingContext.Indicators[index] = new ProyectionIndicatorDisplayModel
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