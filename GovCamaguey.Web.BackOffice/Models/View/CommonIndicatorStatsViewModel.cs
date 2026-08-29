using MOROPO.SINETGIAGOB.GovCamaguey.Persistence.Entities;

namespace MOROPO.SINETGIAGOB.GovCamaguey.Web.BackOffice.Models.View;

public class CommonIndicatorStatsViewModel
{
    public string Name { get; set; }

    public double Amount { get; set; }

    public UnitMeasurement UnitMeasurement { get; set; }

    public double ReferenceResult { get; set; }

    public double LastResult { get; set; }

    public string Context { get; set; }

    public IndicatorPlanViewModel? Plan { get; set; }
}
