namespace MOROPO.SINETGIAGOB.GovCamaguey.Terminal.Models.Page.Configuration;

public class ObjectiveIndicatorConfigurationPageModel : Contract.AbstractIndicatorConfigurationPageModel<GovCamaguey.Persistence.Entities.ObjectiveIndicator, Interop.API.Common.Models.Input.ObjectiveIndicatorInputModel, GovCamaguey.Persistence.Entities.ObjectiveIndicatorMontlyPlan, Interop.API.Common.Models.Display.ObjectiveIndicatorMontlyPlanDisplayModel>
{
    public static string NameValidableProperty => nameof(Name);
    public static string AmountValidableProperty => nameof(Amount);
    public static string DescriptionValidableProperty => nameof(Description);
    public static string UnitMeasurementValidableProperty => nameof(UnitMeasurement);

    public ObjectiveIndicatorConfigurationPageModel() : this(null) { }

    public ObjectiveIndicatorConfigurationPageModel(Interop.API.Common.Models.Input.ObjectiveIndicatorInputModel model, Interop.API.Common.Models.Display.ObjectiveIndicatorMontlyPlanDisplayModel[] montlyPlans = null) : base(model, montlyPlans)
    {
    }
}