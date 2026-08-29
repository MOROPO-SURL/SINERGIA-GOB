using MOROPO.Framework.Caney.MVVM.Core.Extensions;
using MOROPO.SINETGIAGOB.GovCamaguey.Interop.API.Common.Models.Display;
using MOROPO.SINETGIAGOB.GovCamaguey.Interop.API.Common.Models.Input;
using System.Collections.Generic;
using System.Linq;

namespace MOROPO.SINETGIAGOB.GovCamaguey.Terminal.UI.Pages.Configuration;

public partial class ActionIndicatorConfigurationPage : Core.Presentation.Terminal.MAUI.UI.Pages.Base.ConfigurationBaseContentPage<ActionIndicatorInputModel, ActionIndicatorInputModel, Models.Page.Configuration.ActionIndicatorConfigurationPageModel>
{
    private readonly List<ActionIndicatorMontlyPlanInputModel> editionMontlyPlans = [];
    private readonly List<ActionIndicatorMontlyPlanInputModel> incomingMontlyPlans = [];
    private readonly List<ActionIndicatorMontlyPlanDisplayModel> outgoingMontlyPlans = [];

    public ActionIndicatorConfigurationPage(System.Action<ActionIndicatorInputModel> successIndicator, Models.Page.Configuration.ActionIndicatorConfigurationPageModel model) : base(successIndicator, model, new Validators.ActionIndicatorConfigurationValidator())
    {
        InitializeComponent();

        if (model.UnitMeasurementId != null)
        {
            for (int i = 0; i < model.AvailableUnitMeasurements.Length; i++)
            {
                var current = model.AvailableUnitMeasurements[i];

                if (model.UnitMeasurementId != current.Id) continue;

                UnitMeasurementPicker.SelectedIndex = i;

                break;
            }
        }
    }

    public ActionIndicatorConfigurationPage(System.Action<ActionIndicatorInputModel> successAction, Models.Page.Configuration.ActionIndicatorConfigurationPageModel model, List<ActionIndicatorMontlyPlanInputModel> editionMontlyPlans, List<ActionIndicatorMontlyPlanInputModel> incomingMontlyPlans, List<ActionIndicatorMontlyPlanDisplayModel> outgoingMontlyPlans) : this(successAction, model)
    {
        this.editionMontlyPlans = editionMontlyPlans;
        this.incomingMontlyPlans = incomingMontlyPlans;
        this.outgoingMontlyPlans = outgoingMontlyPlans;
    }

    protected override string EntityTypeName => "Indicador";

    protected override System.Threading.Tasks.Task<ActionIndicatorInputModel> PerformCreationAsync()
    {
        return System.Threading.Tasks.Task.FromResult(new ActionIndicatorInputModel
        {
            Id = System.Guid.NewGuid(),
            Priority = ConcreteBindingContext.Priority,
            Name = ConcreteBindingContext.Name,
            Description = ConcreteBindingContext.Description,
            Amount = ConcreteBindingContext.Amount,
            UnitMeasurementId = ConcreteBindingContext.AvailableUnitMeasurements[UnitMeasurementPicker.SelectedIndex].Id,
            IncomingMontlyPlans = incomingMontlyPlans
        });
    }

    protected override System.Threading.Tasks.Task<ActionIndicatorInputModel> PerformEditionAsync()
    {
        return System.Threading.Tasks.Task.FromResult(new ActionIndicatorInputModel
        {
            Id = ConcreteBindingContext.Id,
            Priority = ConcreteBindingContext.Priority,
            Name = ConcreteBindingContext.Name,
            Description = ConcreteBindingContext.Description,
            Amount = ConcreteBindingContext.Amount,
            UnitMeasurementId = ConcreteBindingContext.AvailableUnitMeasurements[UnitMeasurementPicker.SelectedIndex].Id,
            IncomingMontlyPlans = incomingMontlyPlans,
            EditionMontlyPlans = editionMontlyPlans,
            OutgoingMontlyPlans = outgoingMontlyPlans
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

    private void Picker_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        hasChangesFlag = true;
    }

    private void MontlyPlanAgregationButton_Clicked(object sender, System.EventArgs e)
    {
        var availableMonthYearPairs = new List<KeyValuePair<int,int>>();

        var now = System.DateTime.Now;

        for (int year = now.Year; year <= now.Year + 1; year++)
        {
            for (int month = 1; month <= 12; month++)
            {
                if (ConcreteBindingContext.MontlyPlans.Any(model => model.Year == year && model.Month == month)) continue;

                availableMonthYearPairs.Add(new KeyValuePair<int, int>(year,month));
            }
        }

        this.NavigationPushPageAsync(new ActionIndicatorMontlyPlanConfigurationPage(MontlyPlanCreateSuccess_Callback, new Models.Page.Configuration.ActionIndicatorMontlyPlanConfigurationPageModel(ConcreteBindingContext.AvailableUnitMeasurements[UnitMeasurementPicker.SelectedIndex], [.. availableMonthYearPairs], null)));
    }

    private void MontlyPlanEditionButton_Clicked(object sender, System.EventArgs e)
    {
        var target = ((Microsoft.Maui.Controls.BindableObject)sender).BindingContext as ActionIndicatorMontlyPlanDisplayModel;

        var availableMonthYearPairs = new List<KeyValuePair<int, int>>();

        var now = System.DateTime.Now;

        for (int year = now.Year; year <= now.Year + 1; year++)
        {
            for (int month = 1; month <= 12; month++)
            {
                if ((year != target.Year && month != target.Month) && ConcreteBindingContext.MontlyPlans.Any(model => model.Year == year && model.Month == month)) continue;

                availableMonthYearPairs.Add(new KeyValuePair<int, int>(year, month));
            }
        }

        this.NavigationPushPageAsync((Microsoft.Maui.Controls.Page)new ActionIndicatorMontlyPlanConfigurationPage(MontlyPlanEditSuccess_Callback, new Models.Page.Configuration.ActionIndicatorMontlyPlanConfigurationPageModel(ConcreteBindingContext.AvailableUnitMeasurements[UnitMeasurementPicker.SelectedIndex], [.. availableMonthYearPairs], new ActionIndicatorMontlyPlanInputModel
        {
            Id = target.Id,
            Month = target.Month,
            Year = target.Year,
            Amount = target.Amount
        })));
    }

    private void MontlyPlanRemotionButton_Clicked(object sender, System.EventArgs e)
    {
        DisplayAlert("Confirmación", "Está seguro que desea retirar este elemento?", "Confirmar", "Cancelar").ContinueWith(task =>
        {
            if (!task.Result) return;

            Dispatcher.Dispatch(() =>
            {
                var target = ((Microsoft.Maui.Controls.BindableObject)sender).BindingContext as ActionIndicatorMontlyPlanDisplayModel;

                var handled = false;

                foreach (var item in incomingMontlyPlans)
                {
                    if (item.Id != target.Id) continue;

                    incomingMontlyPlans.Remove(incomingMontlyPlans.First(model => model.Id == target.Id));

                    handled = true;

                    break;
                }

                if (!handled)
                {
                    foreach (var item in editionMontlyPlans)
                    {
                        if (item.Id != target.Id) continue;

                        editionMontlyPlans.Remove(editionMontlyPlans.First(model => model.Id == target.Id));

                        break;
                    }
                }

                if (!handled)
                {
                    outgoingMontlyPlans.Add(target);
                }

                ConcreteBindingContext.MontlyPlans.Remove(target);

                hasChangesFlag = true;
            });
        });
    }

    private void MontlyPlanCreateSuccess_Callback(ActionIndicatorMontlyPlanInputModel model)
    {
        incomingMontlyPlans.Add(model);

        ConcreteBindingContext.MontlyPlans.Add(new ActionIndicatorMontlyPlanDisplayModel
        {
            Id = model.Id,
            Month = model.Month,
            Year = model.Year,
            Amount = model.Amount
        });

        hasChangesFlag = true;
    }

    private void MontlyPlanEditSuccess_Callback(ActionIndicatorMontlyPlanInputModel model)
    {
        var handled = false;

        foreach (var item in incomingMontlyPlans)
        {
            if (item.Id != model.Id) continue;

            item.Month = model.Month;
            item.Year = model.Year;
            item.Amount = model.Amount;

            handled = true;

            break;
        }

        if (!handled)
        {
            foreach (var item in editionMontlyPlans)
            {
                if (item.Id != model.Id) continue;

                item.Month = model.Month;
                item.Year = model.Year;
                item.Amount = model.Amount;

                handled = true;

                break;
            }
        }

        if (!handled)
        {
            editionMontlyPlans.Add(model);
        }

        var index = -1;

        foreach (var item in ConcreteBindingContext.MontlyPlans)
        {
            index++;

            if (item.Id != model.Id) continue;

            ConcreteBindingContext.MontlyPlans[index] = new ActionIndicatorMontlyPlanDisplayModel
            {
                Id = model.Id,
                Month = model.Month,
                Year = model.Year,
                Amount = model.Amount
            };

            break;
        }

        hasChangesFlag = true;
    }
}