using Microsoft.Extensions.DependencyInjection;
using MOROPO.Framework.Caney.MVVM.Core.Extensions;
using MOROPO.SINETGIAGOB.GovCamaguey.Interop.API.Common.Models.Display;
using MOROPO.SINETGIAGOB.GovCamaguey.Interop.API.Common.Models.Input;
using System.Collections.Generic;
using System.Linq;

namespace MOROPO.SINETGIAGOB.GovCamaguey.Terminal.UI.Pages.Configuration;

public partial class ActionConfigurationPage : Core.Presentation.Terminal.MAUI.UI.Pages.Base.ConfigurationBaseContentPage<ActionInputModel, ActionInputModel, Models.Page.Configuration.ActionConfigurationPageModel>
{
    private readonly List<ActionIndicatorInputModel> editionIndicators = [];
    private readonly List<ActionIndicatorInputModel> incomingIndicators = [];
    private readonly List<ActionIndicatorDisplayModel> outgoingIndicators = [];

    private readonly List<ActionMonthlyClosureInputModel> editionMonthlyClosures = [];
    private readonly List<ActionMonthlyClosureInputModel> incomingMonthlyClosures = [];
    private readonly List<ActionMonthlyClosureDisplayModel> outgoingMonthlyClosures = [];

    public ActionConfigurationPage(System.Action<ActionInputModel> successAction, Models.Page.Configuration.ActionConfigurationPageModel model) : base(successAction, model, new Validators.ActionConfigurationValidator())
    {
        InitializeComponent();
    }

    public ActionConfigurationPage(
        System.Action<ActionInputModel> successAction, 
        Models.Page.Configuration.ActionConfigurationPageModel model, 
        List<ActionIndicatorInputModel> editionIndicators, 
        List<ActionIndicatorInputModel> incomingIndicators, 
        List<ActionIndicatorDisplayModel> outgoingIndicators, 
        List<ActionMonthlyClosureInputModel> editionMonthlyClosures, 
        List<ActionMonthlyClosureInputModel> incomingMonthlyClosures, 
        List<ActionMonthlyClosureDisplayModel> outgoingMonthlyClosures) 
        : this(successAction, model)
    {
        this.editionIndicators = editionIndicators;
        this.incomingIndicators = incomingIndicators;
        this.outgoingIndicators = outgoingIndicators;

        this.editionMonthlyClosures = editionMonthlyClosures;
        this.incomingMonthlyClosures = incomingMonthlyClosures;
        this.outgoingMonthlyClosures = outgoingMonthlyClosures;
    }

    protected override string EntityTypeName => "Acción";

    protected override System.Threading.Tasks.Task<ActionInputModel> PerformCreationAsync()
    {
        return System.Threading.Tasks.Task.FromResult(new ActionInputModel
        {
            Id = System.Guid.NewGuid(),
            Index = ConcreteBindingContext.Index,
            Name = ConcreteBindingContext.Name,
            Description = ConcreteBindingContext.Description,
            ResponsabilityOrganizations = [.. ConcreteBindingContext.ResponsabilityOrganizations],
            ParticipationOrganizations = [.. ConcreteBindingContext.ParticipationOrganizations],
            IncomingIndicators = incomingIndicators,
            IncomingMonthlyClosures = incomingMonthlyClosures
        });
    }

    protected override System.Threading.Tasks.Task<ActionInputModel> PerformEditionAsync()
    {
        return System.Threading.Tasks.Task.FromResult(new ActionInputModel
        {
            Id = ConcreteBindingContext.Id,
            Index = ConcreteBindingContext.Index,
            Name = ConcreteBindingContext.Name,
            Description = ConcreteBindingContext.Description,
            ResponsabilityOrganizations = [.. ConcreteBindingContext.ResponsabilityOrganizations],
            ParticipationOrganizations = [.. ConcreteBindingContext.ParticipationOrganizations],
            IncomingIndicators = incomingIndicators,
            EditionIndicators = editionIndicators,
            OutgoingIndicators = outgoingIndicators,
            IncomingMonthlyClosures = incomingMonthlyClosures,
            EditionMonthlyClosures = editionMonthlyClosures,
            OutgoingMonthlyClosures = outgoingMonthlyClosures
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

        if (!user.HasFullAccess && !user.Roles.Any(item => item.Permissions.Any(_item => _item.Code == Interop.API.Common.Constants.Security.Authorization.ActionIndicatorCreatorPermission)))
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

                var model = new Models.Page.Configuration.ActionIndicatorConfigurationPageModel(null)
                {
                    AvailableUnitMeasurements = await unitMeasurementsAPIConnectClient.GetEnabledAsync()
                };

                Dispatcher.Dispatch(() =>
                {
                    ConcreteBindingContext.IsBusy = false;

                    this.NavigationPushPageAsync(new ActionIndicatorConfigurationPage(IndicatorCreateSuccess_Callback, model));
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

        if (!user.HasFullAccess && !user.Roles.Any(item => item.Permissions.Any(_item => _item.Code == Interop.API.Common.Constants.Security.Authorization.ActionIndicatorEditorPermission)))
        {
            DisplayAlert("Aviso", "Su usuario no cuenta con permisos para modificar elementos registrados de este tipo. Póngase en contacto con los administradores del sistema para solicitar los permisos requeridos.", "Aceptar");

            return;
        }

        var target = ((Microsoft.Maui.Controls.BindableObject)sender).BindingContext as ActionIndicatorDisplayModel;
        
        ConcreteBindingContext.IsBusy = true;

        System.Threading.Tasks.Task.Run((System.Func<System.Threading.Tasks.Task>)(async () =>
        {
            try
            {
                var model = incomingIndicators.FirstOrDefault(_model => _model.Id == target.Id) ?? editionIndicators.FirstOrDefault(_model => _model.Id == target.Id);

                model ??= new ActionIndicatorInputModel
                {
                    Id = target.Id,
                    Name = target.Name,
                    Amount = target.Amount,
                    Priority = target.Priority,
                    Description = target.Description,
                    UnitMeasurementId = target.UnitMeasurement.Id
                };

                var unitMeasurementsAPIConnectClient = new Interop.API.Connect.Clients.UnitMeasurementsAPIConnectClient(Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Framework.Guama.Core.Presentation.API.Connect.Providers.Facade.IHttpClientHelperProviderFacade>());

                var configurationModel = new Models.Page.Configuration.ActionIndicatorConfigurationPageModel(model, target.MontlyPlans)
                {
                    AvailableUnitMeasurements = await unitMeasurementsAPIConnectClient.GetEnabledAsync()
                };

                Dispatcher.Dispatch((System.Action)(() =>
                {
                    ConcreteBindingContext.IsBusy = false;

                    this.NavigationPushPageAsync(new ActionIndicatorConfigurationPage(IndicatorEditSuccess_Callback, configurationModel, model.EditionMontlyPlans, model.IncomingMontlyPlans, model.OutgoingMontlyPlans));
                }));
            }
            catch (System.Exception exception)
            {
                Dispatcher.Dispatch(() =>
                {
                    Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Core.Presentation.Terminal.MAUI.Helpers.Facade.IExceptionHandlingHelperFacade>().HandleException(exception, this);
                });
            }
        }));
    }

    private void IndicatorRemotionButton_Clicked(object sender, System.EventArgs e)
    {
        var user = Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Core.Presentation.Terminal.MAUI.Managers.Facade.ISessionManagerFacade>().CurrentSession.User;

        if (!user.HasFullAccess && !user.Roles.Any(item => item.Permissions.Any(_item => _item.Code == Interop.API.Common.Constants.Security.Authorization.ActionIndicatorRemoverPermission)))
        {
            DisplayAlert("Aviso", "Su usuario no cuenta con permisos para retirar elementos registrados de este tipo. Póngase en contacto con los administradores del sistema para solicitar los permisos requeridos.", "Aceptar");

            return;
        }

        DisplayAlert("Confirmación", "Está seguro que desea retirar este indicador?", "Confirmar", "Cancelar").ContinueWith(task =>
        {
            if (!task.Result) return;

            Dispatcher.Dispatch(() =>
            {
                var target = ((Microsoft.Maui.Controls.BindableObject)sender).BindingContext as ActionIndicatorDisplayModel;

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

    private void MonthlyClosureAgregationButton_Clicked(object sender, System.EventArgs e)
    {
        this.NavigationPushPageAsync(new ActionMonthlyClosureConfigurationPage(MonthlyClosureCreateSuccess_Callback, new Models.Page.Configuration.ActionMonthlyClosureConfigurationPageModel(null)));
    }

    private void MonthlyClosureEditionButton_Clicked(object sender, System.EventArgs e)
    {
        var target = ((Microsoft.Maui.Controls.BindableObject)sender).BindingContext as ActionMonthlyClosureDisplayModel;

        this.NavigationPushPageAsync(new ActionMonthlyClosureConfigurationPage(MonthlyClosureEditSuccess_Callback, new Models.Page.Configuration.ActionMonthlyClosureConfigurationPageModel(new ActionMonthlyClosureInputModel { Id = target.Id, At = target.At })));
    }

    private void MonthlyClosureRemotionButton_Clicked(object sender, System.EventArgs e)
    {
        DisplayAlert("Confirmación", "Está seguro que desea retirar este elemento?", "Confirmar", "Cancelar").ContinueWith(task =>
        {
            if (!task.Result) return;

            Dispatcher.Dispatch(() =>
            {
                var target = ((Microsoft.Maui.Controls.BindableObject)sender).BindingContext as ActionMonthlyClosureDisplayModel;

                var handled = false;

                foreach (var item in incomingMonthlyClosures)
                {
                    if (item.Id != target.Id) continue;

                    incomingMonthlyClosures.Remove(incomingMonthlyClosures.First(model => model.Id == target.Id));

                    handled = true;

                    break;
                }

                if (!handled)
                {
                    foreach (var item in editionMonthlyClosures)
                    {
                        if (item.Id != target.Id) continue;

                        editionMonthlyClosures.Remove(editionMonthlyClosures.First(model => model.Id == target.Id));

                        break;
                    }
                }

                if (!handled)
                {
                    outgoingMonthlyClosures.Add(target);
                }

                ConcreteBindingContext.MonthlyClosures.Remove(target);

                hasChangesFlag = true;
            });
        });
    }

    private void AddOrganizationParticipationButton_Clicked(object sender, System.EventArgs e)
    {
        this.NavigationPushModalPageAsync(new Microsoft.Maui.Controls.NavigationPage(new Pick.OrganizationPickPage(ParticipationOrganizationPickComplete_Callback)));
    }

    private void AddOrganizationResponsabilityButton_Clicked(object sender, System.EventArgs e)
    {
        this.NavigationPushModalPageAsync(new Microsoft.Maui.Controls.NavigationPage(new Pick.OrganizationPickPage(ResponsabilityOrganizationPickComplete_Callback)));
    }

    private void ParticipationOrganizationRemoveButton_Clicked(object sender, System.EventArgs e)
    {
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

    private void IndicatorCreateSuccess_Callback(ActionIndicatorInputModel model)
    {
        incomingIndicators.Add(model);

        ConcreteBindingContext.Indicators.Add(new ActionIndicatorDisplayModel
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

    private void IndicatorEditSuccess_Callback(ActionIndicatorInputModel model)
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

            ConcreteBindingContext.Indicators[index] = new ActionIndicatorDisplayModel
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

    private void MonthlyClosureCreateSuccess_Callback(ActionMonthlyClosureInputModel model)
    {
        incomingMonthlyClosures.Add(model);

        ConcreteBindingContext.MonthlyClosures.Add(new ActionMonthlyClosureDisplayModel
        {
            Id = model.Id,
            At = model.At
        });

        hasChangesFlag = true;
    }

    private void MonthlyClosureEditSuccess_Callback(ActionMonthlyClosureInputModel model)
    {
        var handled = false;

        foreach (var item in incomingMonthlyClosures)
        {
            if (item.Id != model.Id) continue;

            item.At = model.At;

            handled = true;

            break;
        }

        if (!handled)
        {
            foreach (var item in editionMonthlyClosures)
            {
                if (item.Id != model.Id) continue;

                item.At = model.At;

                handled = true;

                break;
            }
        }

        if (!handled)
        {
            editionMonthlyClosures.Add(model);
        }

        var index = -1;

        foreach (var item in ConcreteBindingContext.MonthlyClosures)
        {
            index++;

            if (item.Id != model.Id) continue;

            ConcreteBindingContext.MonthlyClosures[index] = new ActionMonthlyClosureDisplayModel
            {
                Id = model.Id,
                At = model.At
            };

            break;
        }

        hasChangesFlag = true;
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
}