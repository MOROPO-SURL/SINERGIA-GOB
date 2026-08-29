using System;

namespace MOROPO.SINETGIAGOB.GovCamaguey.Web.BackOffice.Models.View;

public class ObjectivesIndicatorsStatsReportViewModel
{
    public DateTime ReferenceAt { get; set; }

    public CommonIndicatorStatsViewModel[] Indicators { get; set; }

    public ObjectiveStatsViewModel[] Objectives { get; set; }
}
