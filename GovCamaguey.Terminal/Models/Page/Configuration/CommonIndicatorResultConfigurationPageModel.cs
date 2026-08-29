using MOROPO.SINETGIAGOB.GovCamaguey.Interop.API.Common.Models.Display;

namespace MOROPO.SINETGIAGOB.GovCamaguey.Terminal.Models.Page.Configuration;

public sealed class CommonIndicatorResultConfigurationPageModel : Framework.Caney.MVVM.Core.Models.Page.BasePageModel
{
    public static string AmountValidableProperty => nameof(Amount);
    public static string AtValidableProperty => nameof(At);
    public static string DescriptionValidableProperty => nameof(Description);

    public static System.DateTime MAX_AT => System.DateTime.Now;

    private System.DateTime at = System.DateTime.Now;
    private double amount;
    private string description;
    private FluentValidation.Results.ValidationResult validationResult;

    public CommonIndicatorResultConfigurationPageModel(System.Guid indicatorId, string indicatorName, UnitMeasurementDisplayModel indicatorUnitMeasurement)
    {
        IndicatorId = indicatorId;
        IndicatorName = indicatorName;
        IndicatorUnitMeasurement = indicatorUnitMeasurement;
    }

    public System.Guid IndicatorId { get; set; }

    public string IndicatorName { get; }

    public UnitMeasurementDisplayModel IndicatorUnitMeasurement { get; }

    public System.DateTime At { get => at; set => SetProperty(ref at, value); }

    public double Amount { get => amount; set => SetProperty(ref amount, value); }

    public string Description { get => description; set => SetProperty(ref description, value); }

    public FluentValidation.Results.ValidationResult ValidationResult { get => validationResult; set => SetProperty(ref validationResult, value); }
}
