using System.Collections.Generic;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Web.BackOffice.Models.View
{
    public class ManagementHomeViewModel
    {
        public Dictionary<string, ICollection<Framework.Guama.Core.Presentation.Web.Backend.Models.View.ManagementDashboardSectionViewModel>> Sections { get; set; }

        public KeyValuePair<System.DayOfWeek, long>[] WeekUserActivity { get; set; }

        public KeyValuePair<int, long>[] MonthUserActivity { get; set; }

        public KeyValuePair<int, long>[] SemesterUserActivity { get; set; }

        public Dictionary<string, long> SingleByLastYearUserActivity { get; set; }
    }
}

