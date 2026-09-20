namespace MOROPO.SINERGIAGOB.GovCamaguey.Terminal.Models.Page.Configuration;

public class ObjectiveIndicatorMontlyPlanConfigurationPageModel : Contract.AbstractIndicatorMontlyPlanConfigurationPageModel<GovCamaguey.Persistence.Entities.ObjectiveIndicatorMontlyPlan, Interop.API.Common.Models.Input.ObjectiveIndicatorMontlyPlanInputModel>
{
    public static string YearMonthValidableProperty => nameof(YearMonth);
    public static string AmountValidableProperty => nameof(Amount);

    public ObjectiveIndicatorMontlyPlanConfigurationPageModel(Interop.API.Common.Models.Display.UnitMeasurementDisplayModel unitMeasurement, System.Collections.Generic.KeyValuePair<int, int>[] availableYearMonthPairs, Interop.API.Common.Models.Input.ObjectiveIndicatorMontlyPlanInputModel model) : base(unitMeasurement, availableYearMonthPairs, model)
    {
    }
}