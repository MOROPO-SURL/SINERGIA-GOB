namespace MOROPO.SINERGIAGOB.GovCamaguey.Terminal.Models.Page.Configuration;

public class ActionIndicatorMontlyPlanConfigurationPageModel : Contract.AbstractIndicatorMontlyPlanConfigurationPageModel<GovCamaguey.Persistence.Entities.ActionIndicatorMontlyPlan, Interop.API.Common.Models.Input.ActionIndicatorMontlyPlanInputModel>
{
    public static string YearMonthValidableProperty => nameof(YearMonth);
    public static string AmountValidableProperty => nameof(Amount);

    public ActionIndicatorMontlyPlanConfigurationPageModel(Interop.API.Common.Models.Display.UnitMeasurementDisplayModel unitMeasurement, System.Collections.Generic.KeyValuePair<int,int>[] availableYearMonthPairs, Interop.API.Common.Models.Input.ActionIndicatorMontlyPlanInputModel model) : base(unitMeasurement, availableYearMonthPairs, model)
    {
    }
}