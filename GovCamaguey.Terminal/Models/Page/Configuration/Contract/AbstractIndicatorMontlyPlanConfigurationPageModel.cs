using System.Collections.Generic;
using System.Linq;

namespace MOROPO.SINETGIAGOB.GovCamaguey.Terminal.Models.Page.Configuration.Contract;

public abstract class AbstractIndicatorMontlyPlanConfigurationPageModel<TIndicatorMontlyPlanEntity, TIndicatorMontlyPlanInputModel> : Core.Presentation.Terminal.MAUI.Models.Page.Contract.AbstractConfigurationPageModel<TIndicatorMontlyPlanInputModel>
    where TIndicatorMontlyPlanEntity : GovCamaguey.Persistence.Entities.Contract.AbstractIndicatorMontlyPlan
    where TIndicatorMontlyPlanInputModel : Interop.API.Common.Models.Input.Contract.AbstractIndicatorMontlyPlanInputModel<TIndicatorMontlyPlanEntity>
{
    private KeyValuePair<int, int> yearMonthPair;
    private double amount;

    public AbstractIndicatorMontlyPlanConfigurationPageModel(Interop.API.Common.Models.Display.UnitMeasurementDisplayModel unitMeasurement, KeyValuePair<int,int>[] availableYearMonthPairs, TIndicatorMontlyPlanInputModel model) : base(model)
    {
        UnitMeasurement = unitMeasurement;
        AvailableYearMonthPairs = availableYearMonthPairs;

        if (model != null)
        {
            Id = model.Id;
            yearMonthPair = availableYearMonthPairs.First(item => item.Key == model.Year && item.Value == model.Month);
            Amount = model.Amount;
        }
    }

    public System.Guid Id { get; set; }

    public Interop.API.Common.Models.Display.UnitMeasurementDisplayModel UnitMeasurement { get; set; }

    public KeyValuePair<int, int> YearMonth { get => yearMonthPair; set => SetProperty(ref yearMonthPair, value); }

    public double Amount { get => amount; set => SetProperty(ref amount, value); }

    public KeyValuePair<int, int>[] AvailableYearMonthPairs { get; private set; }
}