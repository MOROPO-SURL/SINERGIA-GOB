using System.Linq;

namespace MOROPO.SINETGIAGOB.GovCamaguey.Terminal.Models.Page.Configuration;

public class ActionIndicatorConfigurationPageModel : Contract.AbstractIndicatorConfigurationPageModel<GovCamaguey.Persistence.Entities.ActionIndicator, Interop.API.Common.Models.Input.ActionIndicatorInputModel, GovCamaguey.Persistence.Entities.ActionIndicatorMontlyPlan, Interop.API.Common.Models.Display.ActionIndicatorMontlyPlanDisplayModel>
{
    public static string NameValidableProperty => nameof(Name);
    public static string AmountValidableProperty => nameof(Amount);
    public static string DescriptionValidableProperty => nameof(Description);
    public static string UnitMeasurementValidableProperty => nameof(UnitMeasurement);

    public ActionIndicatorConfigurationPageModel() : this(null) { }

    public ActionIndicatorConfigurationPageModel(Interop.API.Common.Models.Input.ActionIndicatorInputModel model, Interop.API.Common.Models.Display.ActionIndicatorMontlyPlanDisplayModel[] montlyPlans = null) : base(model, montlyPlans)
    {
    }
}
