using System;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Web.BackOffice.Models.View;

public class ObjectivesIndicatorsStatsReportViewModel
{
    public DateTime ReferenceAt { get; set; }

    public CommonIndicatorStatsViewModel[] Indicators { get; set; }

    public ObjectiveStatsViewModel[] Objectives { get; set; }
}
