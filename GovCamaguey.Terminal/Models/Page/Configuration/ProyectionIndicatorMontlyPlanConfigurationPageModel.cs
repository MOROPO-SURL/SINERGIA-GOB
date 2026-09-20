namespace MOROPO.SINERGIAGOB.GovCamaguey.Terminal.Models.Page.Configuration;

public class ProyectionIndicatorMontlyPlanConfigurationPageModel : Contract.AbstractIndicatorMontlyPlanConfigurationPageModel<GovCamaguey.Persistence.Entities.ProyectionIndicatorMontlyPlan, Interop.API.Common.Models.Input.ProyectionIndicatorMontlyPlanInputModel>
{
    public static string YearMonthValidableProperty => nameof(YearMonth);
    public static string AmountValidableProperty => nameof(Amount);

    public ProyectionIndicatorMontlyPlanConfigurationPageModel(Interop.API.Common.Models.Display.UnitMeasurementDisplayModel unitMeasurement, System.Collections.Generic.KeyValuePair<int, int>[] availableYearMonthPairs, Interop.API.Common.Models.Input.ProyectionIndicatorMontlyPlanInputModel model) : base(unitMeasurement, availableYearMonthPairs, model)
    {
    }
}