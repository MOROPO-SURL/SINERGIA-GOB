using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.SINERGIAGOB.GovCamaguey.Web.BackOffice.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Web.BackOffice.Controllers.Backend
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [Framework.Guama.Core.Presentation.Web.Security.Attributes.RequirePermissions(Framework.Guama.Core.Security.Presentation.Constants.Permissions.ShellManager)]
    [Framework.Guama.Core.Presentation.Web.Security.Attributes.ChangeUserPasswordRequiredAction]
    [Framework.Guama.Core.Presentation.Web.Security.Attributes.CatchChangeUserPasswordRequiredException]
    [Route("[area]/[controller]")]
    [Area("management")]
    public class DashboardController : Framework.Guama.Core.Presentation.Web.Common.Controllers.BaseWebController
    {
        private readonly IEnumerable<Framework.Guama.Core.Presentation.Web.Backend.Providers.Facade.IManagementDashboardSectionProviderFacade> managementDashboardSectionProviders;
        private readonly IEntityRepository<Framework.Guama.Core.Persistence.ORM.EF.Entities.Trace, long> traceRepository;

        public DashboardController(IEntityRepository<Framework.Guama.Core.Persistence.ORM.EF.Entities.Trace, long> traceRepository, IEnumerable<Framework.Guama.Core.Presentation.Web.Backend.Providers.Facade.IManagementDashboardSectionProviderFacade> managementDashboardSectionProviders, Microsoft.Extensions.Localization.IStringLocalizer<DashboardController> localizer, ILogger<DashboardController> logger) : base(localizer, logger)
        {
            this.managementDashboardSectionProviders = managementDashboardSectionProviders;
            this.traceRepository = traceRepository;
        }

        public IActionResult Index()
        {
            var now = System.DateTime.Now;

            var yearTraces = traceRepository.FindBy(_entity => _entity.Registered > now.AddYears(-1)).ToArray();

            var semesterTraces = yearTraces.Where(_entity => _entity.Registered > now.AddMonths(-6)).ToArray();

            var monthTraces = semesterTraces.Where(_entity => _entity.Registered > now.AddMonths(-1)).ToArray();

            var weekTraces = monthTraces.Where(_entity => _entity.Registered > now.AddDays(-5)).ToArray();

            var model = new Models.View.ManagementHomeViewModel
            {
                Sections = managementDashboardSectionProviders.ToManagementDashboardSectionGroups(),
                SingleByLastYearUserActivity = new Dictionary<string, long>(),
                WeekUserActivity = new KeyValuePair<System.DayOfWeek, long>[]
                {
                    new KeyValuePair<System.DayOfWeek,long>(now.DayOfWeek, weekTraces.LongCount(_entity => _entity.Registered.Day == now.Day)),
                    new KeyValuePair<System.DayOfWeek,long>(now.AddDays(-1).DayOfWeek, weekTraces.LongCount(_entity => _entity.Registered.Day == now.Day - 1)),
                    new KeyValuePair<System.DayOfWeek,long>(now.AddDays(-2).DayOfWeek, weekTraces.LongCount(_entity => _entity.Registered.Day == now.Day - 2)),
                    new KeyValuePair<System.DayOfWeek,long>(now.AddDays(-3).DayOfWeek, weekTraces.LongCount(_entity => _entity.Registered.Day == now.Day - 3)),
                    new KeyValuePair<System.DayOfWeek,long>(now.AddDays(-4).DayOfWeek, weekTraces.LongCount(_entity => _entity.Registered.Day == now.Day - 4)),
                    new KeyValuePair<System.DayOfWeek,long>(now.AddDays(-5).DayOfWeek, weekTraces.LongCount(_entity => _entity.Registered.Day == now.Day - 5)),
                    new KeyValuePair<System.DayOfWeek,long>(now.AddDays(-6).DayOfWeek, weekTraces.LongCount(_entity => _entity.Registered.Day == now.Day - 6))
                },
                MonthUserActivity = new KeyValuePair<int, long>[]
                {
                    new KeyValuePair<int,long>(now.Day, monthTraces.LongCount(_entity => _entity.Registered.Day == now.Day)),
                    new KeyValuePair<int,long>(now.AddDays(-1).Day, monthTraces.LongCount(_entity => _entity.Registered.Day == now.Day - 1)),
                    new KeyValuePair<int,long>(now.AddDays(-2).Day, monthTraces.LongCount(_entity => _entity.Registered.Day == now.Day - 2)),
                    new KeyValuePair<int,long>(now.AddDays(-3).Day, monthTraces.LongCount(_entity => _entity.Registered.Day == now.Day - 3)),
                    new KeyValuePair<int,long>(now.AddDays(-4).Day, monthTraces.LongCount(_entity => _entity.Registered.Day == now.Day - 4)),
                    new KeyValuePair<int,long>(now.AddDays(-5).Day, monthTraces.LongCount(_entity => _entity.Registered.Day == now.Day - 5)),
                    new KeyValuePair<int,long>(now.AddDays(-6).Day, monthTraces.LongCount(_entity => _entity.Registered.Day == now.Day - 6)),
                    new KeyValuePair<int,long>(now.AddDays(-7).Day, monthTraces.LongCount(_entity => _entity.Registered.Day == now.Day - 7)),
                    new KeyValuePair<int,long>(now.AddDays(-8).Day, monthTraces.LongCount(_entity => _entity.Registered.Day == now.Day - 8)),
                    new KeyValuePair<int,long>(now.AddDays(-9).Day, monthTraces.LongCount(_entity => _entity.Registered.Day == now.Day - 9)),
                    new KeyValuePair<int,long>(now.AddDays(-10).Day, monthTraces.LongCount(_entity => _entity.Registered.Day == now.Day - 10)),
                    new KeyValuePair<int,long>(now.AddDays(-11).Day, monthTraces.LongCount(_entity => _entity.Registered.Day == now.Day - 11)),
                    new KeyValuePair<int,long>(now.AddDays(-12).Day, monthTraces.LongCount(_entity => _entity.Registered.Day == now.Day - 12)),
                    new KeyValuePair<int,long>(now.AddDays(-13).Day, monthTraces.LongCount(_entity => _entity.Registered.Day == now.Day - 13)),
                    new KeyValuePair<int,long>(now.AddDays(-14).Day, monthTraces.LongCount(_entity => _entity.Registered.Day == now.Day - 14)),
                    new KeyValuePair<int,long>(now.AddDays(-15).Day, monthTraces.LongCount(_entity => _entity.Registered.Day == now.Day - 15)),
                    new KeyValuePair<int,long>(now.AddDays(-16).Day, monthTraces.LongCount(_entity => _entity.Registered.Day == now.Day - 16)),
                    new KeyValuePair<int,long>(now.AddDays(-17).Day, monthTraces.LongCount(_entity => _entity.Registered.Day == now.Day - 17)),
                    new KeyValuePair<int,long>(now.AddDays(-18).Day, monthTraces.LongCount(_entity => _entity.Registered.Day == now.Day - 18)),
                    new KeyValuePair<int,long>(now.AddDays(-19).Day, monthTraces.LongCount(_entity => _entity.Registered.Day == now.Day - 19)),
                    new KeyValuePair<int,long>(now.AddDays(-20).Day, monthTraces.LongCount(_entity => _entity.Registered.Day == now.Day - 20)),
                    new KeyValuePair<int,long>(now.AddDays(-21).Day, monthTraces.LongCount(_entity => _entity.Registered.Day == now.Day - 21)),
                    new KeyValuePair<int,long>(now.AddDays(-22).Day, monthTraces.LongCount(_entity => _entity.Registered.Day == now.Day - 22)),
                    new KeyValuePair<int,long>(now.AddDays(-23).Day, monthTraces.LongCount(_entity => _entity.Registered.Day == now.Day - 23)),
                    new KeyValuePair<int,long>(now.AddDays(-24).Day, monthTraces.LongCount(_entity => _entity.Registered.Day == now.Day - 24)),
                    new KeyValuePair<int,long>(now.AddDays(-25).Day, monthTraces.LongCount(_entity => _entity.Registered.Day == now.Day - 25)),
                    new KeyValuePair<int,long>(now.AddDays(-26).Day, monthTraces.LongCount(_entity => _entity.Registered.Day == now.Day - 26)),
                    new KeyValuePair<int,long>(now.AddDays(-27).Day, monthTraces.LongCount(_entity => _entity.Registered.Day == now.Day - 27)),
                    new KeyValuePair<int,long>(now.AddDays(-28).Day, monthTraces.LongCount(_entity => _entity.Registered.Day == now.Day - 28)),
                    new KeyValuePair<int,long>(now.AddDays(-29).Day, monthTraces.LongCount(_entity => _entity.Registered.Day == now.Day - 29)),
                    new KeyValuePair<int,long>(now.AddDays(-30).Day, monthTraces.LongCount(_entity => _entity.Registered.Day == now.Day - 30))
                },
                SemesterUserActivity = new KeyValuePair<int, long>[]
                {
                    new KeyValuePair<int,long>(now.Month, semesterTraces.LongCount(_entity => _entity.Registered.Month == now.Month)),
                    new KeyValuePair<int,long>(now.AddMonths(-1).Month, semesterTraces.LongCount(_entity => _entity.Registered.Month == now.AddMonths(-1).Month)),
                    new KeyValuePair<int,long>(now.AddMonths(-2).Month, semesterTraces.LongCount(_entity => _entity.Registered.Month == now.AddMonths(-2).Month)),
                    new KeyValuePair<int,long>(now.AddMonths(-3).Month, semesterTraces.LongCount(_entity => _entity.Registered.Month == now.AddMonths(-3).Month)),
                    new KeyValuePair<int,long>(now.AddMonths(-4).Month, semesterTraces.LongCount(_entity => _entity.Registered.Month == now.AddMonths(-4).Month)),
                    new KeyValuePair<int,long>(now.AddMonths(-5).Month, semesterTraces.LongCount(_entity => _entity.Registered.Month == now.AddMonths(-5).Month))
                }
            };

            foreach (var trace in yearTraces)
            {
                var author = trace.Author ?? "api";

                // TODO Back to spected behavior when Terminal user autentication
                //if (!model.SingleByLastYearUserActivity.ContainsKey(trace.Author))
                //if (!model.SingleByLastYearUserActivity.ContainsKey(trace.Author))
                //{
                //    model.SingleByLastYearUserActivity.Add(trace.Author, 1);
                //}
                //else
                //{
                //    model.SingleByLastYearUserActivity[trace.Author]++;
                //}

                if (!model.SingleByLastYearUserActivity.ContainsKey(author))
                {
                    model.SingleByLastYearUserActivity.Add(author, 1);
                }
                else
                {
                    model.SingleByLastYearUserActivity[author]++;
                }
            }

            return View(model);
        }
    }
}
