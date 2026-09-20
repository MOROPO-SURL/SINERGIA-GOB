namespace MOROPO.SINERGIAGOB.GovCamaguey.Web.BackOffice.Models.View;

public class ObjectiveStatsViewModel
{
    public int Index { get; set; }

    public string Name { get; set; }

    public CommonIndicatorStatsViewModel[] Indicators { get; set; }

    public ProyectionStatsViewModel[] Proyections { get; set; }
}
