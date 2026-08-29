namespace MOROPO.SINETGIAGOB.GovCamaguey.Web.BackOffice.Models.View;

public class ActionStatsViewModel
{
    public int Index { get; set; }

    public string Name { get; set; }

    public CommonIndicatorStatsViewModel[] Indicators { get; set; }
}
