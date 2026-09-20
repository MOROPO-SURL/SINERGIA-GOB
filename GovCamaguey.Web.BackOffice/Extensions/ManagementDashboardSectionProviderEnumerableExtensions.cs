using MOROPO.Framework.Guama.Core.Presentation.Web.Backend.Models.View;
using System.Collections.Generic;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Web.BackOffice.Extensions
{
    public static class ManagementDashboardSectionProviderEnumerableExtensions
    {
        public static Dictionary<string, ICollection<ManagementDashboardSectionViewModel>> ToManagementDashboardSectionGroups(this IEnumerable<Framework.Guama.Core.Presentation.Web.Backend.Providers.Facade.IManagementDashboardSectionProviderFacade> self)
        {
            var securitySection = new List<ManagementDashboardSectionViewModel>();
            var locationSection = new List<ManagementDashboardSectionViewModel>();
            var govermenanceSection = new List<ManagementDashboardSectionViewModel>();

            #region Security
            foreach (var provider in self)
            {
                if (provider is Framework.Guama.Security.Personal.Presentation.Web.Backend.Providers.ManagementDashboardSectionProvider)
                {
                    securitySection.AddRange(provider.GetManagementDashboardSections());
                }
            }

            securitySection.Add(new ManagementDashboardSectionViewModel
            {
                Title = "Registros",
                Controller = "traces",
                Action = "index",
                Area = "security",
                Role = Core.Presentation.Web.Backend.Constants.Security.Authorization.TraceViewerPermission
            });
            #endregion

            #region Location
            locationSection.Add(new ManagementDashboardSectionViewModel
            {
                Title = "Países",
                Controller = "countries",
                Action = "index",
                Area = "location",
                Role = Framework.Guama.Location.Presentation.Constants.Permissions.CountryViewer
            });
            locationSection.Add(new ManagementDashboardSectionViewModel
            {
                Title = "Regiones",
                Controller = "regions",
                Action = "index",
                Area = "location",
                Role = Framework.Guama.Location.Presentation.Constants.Permissions.RegionViewer
            });
            locationSection.Add(new ManagementDashboardSectionViewModel
            {
                Title = "Provincias",
                Controller = "states",
                Action = "index",
                Area = "location",
                Role = Framework.Guama.Location.Presentation.Constants.Permissions.StateViewer
            });
            locationSection.Add(new ManagementDashboardSectionViewModel
            {
                Title = "Municipios",
                Controller = "municipalities",
                Action = "index",
                Area = "location",
                Role = Framework.Guama.Location.Presentation.Constants.Permissions.MunicipalityViewer
            });
            #endregion

            #region Governance
            govermenanceSection.Add(new ManagementDashboardSectionViewModel
            {
                Title = "Lineamientos",
                Controller = "guidelines",
                Action = "index",
                Area = "governance",
                // TODO Set spected permission
                //Role = Domain.Interop.API.Common.Constants.Security.Authorization.EmployeeViewerPermission
            });
            govermenanceSection.Add(new ManagementDashboardSectionViewModel
            {
                Title = "Personas",
                Controller = "persons",
                Action = "index",
                Area = "governance",
                // TODO Set spected permission
                //Role = Domain.Interop.API.Common.Constants.Security.Authorization.EmployeeViewerPermission
            });
            govermenanceSection.Add(new ManagementDashboardSectionViewModel
            {
                Title = "Organismos",
                Controller = "organizations",
                Action = "index",
                Area = "governance",
                // TODO Set spected permission
                //Role = Domain.Interop.API.Common.Constants.Security.Authorization.EmployeeViewerPermission
            });
            govermenanceSection.Add(new ManagementDashboardSectionViewModel
            {
                Title = "Objetivos",
                Controller = "objectives",
                Action = "index",
                Area = "governance",
                // TODO Set spected permission
                //Role = Domain.Interop.API.Common.Constants.Security.Authorization.EmployeeViewerPermission
            });
            govermenanceSection.Add(new ManagementDashboardSectionViewModel
            {
                Title = "Obj. Específicos",
                Controller = "proyections",
                Action = "index",
                Area = "governance",
                // TODO Set spected permission
                //Role = Domain.Interop.API.Common.Constants.Security.Authorization.EmployeeViewerPermission
            });
            govermenanceSection.Add(new ManagementDashboardSectionViewModel
            {
                Title = "Acciones",
                Controller = "actions",
                Action = "index",
                Area = "governance",
                // TODO Set spected permission
                //Role = Domain.Interop.API.Common.Constants.Security.Authorization.EmployeeViewerPermission
            });
            govermenanceSection.Add(new ManagementDashboardSectionViewModel
            {
                Title = "Indicadores",
                Controller = "indicators",
                Action = "index",
                Area = "governance",
                // TODO Set spected permission
                //Role = Domain.Interop.API.Common.Constants.Security.Authorization.EmployeeViewerPermission
            });
            govermenanceSection.Add(new ManagementDashboardSectionViewModel
            {
                Title = "Resultados",
                Controller = "results",
                Action = "index",
                Area = "governance",
                // TODO Set spected permission
                //Role = Domain.Interop.API.Common.Constants.Security.Authorization.EmployeeViewerPermission
            });
            govermenanceSection.Add(new ManagementDashboardSectionViewModel
            {
                Title = "Unidades de Medida",
                Controller = "unitmeasurements",
                Action = "index",
                Area = "governance",
                // TODO Set spected permission
                //Role = Domain.Interop.API.Common.Constants.Security.Authorization.EmployeeViewerPermission
            });
            #endregion

            return new Dictionary<string, ICollection<ManagementDashboardSectionViewModel>>
            {
                { "Seguridad", securitySection },
                { "Locación", locationSection },
                { "Gestión de Gobierno", govermenanceSection }
            };
        }
    }
}
