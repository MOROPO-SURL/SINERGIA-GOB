namespace MOROPO.SINETGIAGOB.GovCamaguey.Terminal.Models.Page.Configuration;

public class ProyectionIndicatorConfigurationPageModel : Contract.AbstractIndicatorConfigurationPageModel<GovCamaguey.Persistence.Entities.ProyectionIndicator, Interop.API.Common.Models.Input.ProyectionIndicatorInputModel, GovCamaguey.Persistence.Entities.ProyectionIndicatorMontlyPlan, Interop.API.Common.Models.Display.ProyectionIndicatorMontlyPlanDisplayModel>
{
    public static string NameValidableProperty => nameof(Name);
    public static string AmountValidableProperty => nameof(Amount);
    public static string DescriptionValidableProperty => nameof(Description);
    public static string UnitMeasurementValidableProperty => nameof(UnitMeasurement);

    public ProyectionIndicatorConfigurationPageModel() : this(null) { }

    public ProyectionIndicatorConfigurationPageModel(Interop.API.Common.Models.Input.ProyectionIndicatorInputModel model, Interop.API.Common.Models.Display.ProyectionIndicatorMontlyPlanDisplayModel[] montlyPlans = null) : base(model, montlyPlans)
    {
    }
}
