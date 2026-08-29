namespace MOROPO.SINETGIAGOB.GovCamaguey.Web.BackOffice.Models.View;

public class ProyectionStatsViewModel
{
    public int Index { get; set; }

    public string Name { get; set; }

    public CommonIndicatorStatsViewModel[] Indicators { get; set; }

    public ActionStatsViewModel[] Actions { get; set; }
}