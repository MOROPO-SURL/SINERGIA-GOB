using Microsoft.AspNetCore.Mvc;
using MOROPO.Framework.Guama.Core.Presentation.Web.Backend.Providers.Facade;
using MOROPO.SINERGIAGOB.GovCamaguey.Web.BackOffice.Extensions;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Web.BackOffice.Components
{
    public class DashboardSidebarViewComponent : ViewComponent
    {
        private readonly System.Collections.Generic.IEnumerable<IManagementDashboardSectionProviderFacade> managementDashboardSectionProviders;

        public DashboardSidebarViewComponent(System.Collections.Generic.IEnumerable<IManagementDashboardSectionProviderFacade> managementDashboardSectionProviders)
        {
            this.managementDashboardSectionProviders = managementDashboardSectionProviders;
        }

        public IViewComponentResult Invoke()
        {
            return View(managementDashboardSectionProviders.ToManagementDashboardSectionGroups());
        }
    }
}
