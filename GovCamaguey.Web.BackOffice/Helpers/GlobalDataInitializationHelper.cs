using System.Linq;
using MOROPO.SINETGIAGOB.GovCamaguey.Interop.API.Common.Constants.Security;
using MOROPO.Framework.Guama.Core.Security.Common.Persistence.ORM.EF.Extensions;

namespace MOROPO.SINETGIAGOB.GovCamaguey.Web.BackOffice.Helpers
{
    public class GlobalDataInitializationHelper : Framework.Guama.Core.Persistence.ORM.EF.Helpers.Facade.IDataInitializationHelper
    {
        public void InitialiseData(Microsoft.EntityFrameworkCore.DbContext dbContext)
        {
            var permissionDbSet = dbContext.Set<Framework.Guama.Core.Security.Common.Persistence.Entities.Permission>();

            #region Person
            if (!permissionDbSet.Any(role => role.Code.Equals(Authorization.PersonViewerPermission)))
            {
                permissionDbSet.Add(Authorization.PersonViewerPermission.ToPermissionEntity(Authorization.PersonManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(role => role.Code.Equals(Authorization.PersonCreatorPermission)))
            {
                permissionDbSet.Add(Authorization.PersonCreatorPermission.ToPermissionEntity(Authorization.PersonManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(role => role.Code.Equals(Authorization.PersonEditorPermission)))
            {
                permissionDbSet.Add(Authorization.PersonEditorPermission.ToPermissionEntity(Authorization.PersonManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(role => role.Code.Equals(Authorization.PersonRemoverPermission)))
            {
                permissionDbSet.Add(Authorization.PersonRemoverPermission.ToPermissionEntity(Authorization.PersonManagementPermissionGroup));
            }
            #endregion

            #region Organization
            if (!permissionDbSet.Any(role => role.Code.Equals(Authorization.OrganizationViewerPermission)))
            {
                permissionDbSet.Add(Authorization.OrganizationViewerPermission.ToPermissionEntity(Authorization.OrganizationManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(role => role.Code.Equals(Authorization.OrganizationCreatorPermission)))
            {
                permissionDbSet.Add(Authorization.OrganizationCreatorPermission.ToPermissionEntity(Authorization.OrganizationManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(role => role.Code.Equals(Authorization.OrganizationEditorPermission)))
            {
                permissionDbSet.Add(Authorization.OrganizationEditorPermission.ToPermissionEntity(Authorization.OrganizationManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(role => role.Code.Equals(Authorization.OrganizationRemoverPermission)))
            {
                permissionDbSet.Add(Authorization.OrganizationRemoverPermission.ToPermissionEntity(Authorization.OrganizationManagementPermissionGroup));
            }
            #endregion

            #region Objective
            if (!permissionDbSet.Any(role => role.Code.Equals(Authorization.ObjectiveViewerPermission)))
            {
                permissionDbSet.Add(Authorization.ObjectiveViewerPermission.ToPermissionEntity(Authorization.ObjectiveManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(role => role.Code.Equals(Authorization.ObjectiveCreatorPermission)))
            {
                permissionDbSet.Add(Authorization.ObjectiveCreatorPermission.ToPermissionEntity(Authorization.ObjectiveManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(role => role.Code.Equals(Authorization.ObjectiveEditorPermission)))
            {
                permissionDbSet.Add(Authorization.ObjectiveEditorPermission.ToPermissionEntity(Authorization.ObjectiveManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(role => role.Code.Equals(Authorization.ObjectiveRemoverPermission)))
            {
                permissionDbSet.Add(Authorization.ObjectiveRemoverPermission.ToPermissionEntity(Authorization.ObjectiveManagementPermissionGroup));
            }
            #endregion

            #region Proyection
            if (!permissionDbSet.Any(role => role.Code.Equals(Authorization.ProyectionViewerPermission)))
            {
                permissionDbSet.Add(Authorization.ProyectionViewerPermission.ToPermissionEntity(Authorization.ProyectionManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(role => role.Code.Equals(Authorization.ProyectionCreatorPermission)))
            {
                permissionDbSet.Add(Authorization.ProyectionCreatorPermission.ToPermissionEntity(Authorization.ProyectionManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(role => role.Code.Equals(Authorization.ProyectionEditorPermission)))
            {
                permissionDbSet.Add(Authorization.ProyectionEditorPermission.ToPermissionEntity(Authorization.ProyectionManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(role => role.Code.Equals(Authorization.ProyectionRemoverPermission)))
            {
                permissionDbSet.Add(Authorization.ProyectionRemoverPermission.ToPermissionEntity(Authorization.ProyectionManagementPermissionGroup));
            }
            #endregion

            #region Proyection
            if (!permissionDbSet.Any(role => role.Code.Equals(Authorization.ActionViewerPermission)))
            {
                permissionDbSet.Add(Authorization.ActionViewerPermission.ToPermissionEntity(Authorization.ActionManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(role => role.Code.Equals(Authorization.ActionCreatorPermission)))
            {
                permissionDbSet.Add(Authorization.ActionCreatorPermission.ToPermissionEntity(Authorization.ActionManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(role => role.Code.Equals(Authorization.ActionEditorPermission)))
            {
                permissionDbSet.Add(Authorization.ActionEditorPermission.ToPermissionEntity(Authorization.ActionManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(role => role.Code.Equals(Authorization.ActionRemoverPermission)))
            {
                permissionDbSet.Add(Authorization.ActionRemoverPermission.ToPermissionEntity(Authorization.ActionManagementPermissionGroup));
            }
            #endregion

            #region Guideline
            if (!permissionDbSet.Any(role => role.Code.Equals(Authorization.GuidelineViewerPermission)))
            {
                permissionDbSet.Add(Authorization.GuidelineViewerPermission.ToPermissionEntity(Authorization.GuidelineManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(role => role.Code.Equals(Authorization.GuidelineCreatorPermission)))
            {
                permissionDbSet.Add(Authorization.GuidelineCreatorPermission.ToPermissionEntity(Authorization.GuidelineManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(role => role.Code.Equals(Authorization.GuidelineEditorPermission)))
            {
                permissionDbSet.Add(Authorization.GuidelineEditorPermission.ToPermissionEntity(Authorization.GuidelineManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(role => role.Code.Equals(Authorization.GuidelineRemoverPermission)))
            {
                permissionDbSet.Add(Authorization.GuidelineRemoverPermission.ToPermissionEntity(Authorization.GuidelineManagementPermissionGroup));
            }
            #endregion

            #region UnitMeasurement
            if (!permissionDbSet.Any(role => role.Code.Equals(Authorization.UnitMeasurementViewerPermission)))
            {
                permissionDbSet.Add(Authorization.UnitMeasurementViewerPermission.ToPermissionEntity(Authorization.UnitMeasurementManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(role => role.Code.Equals(Authorization.UnitMeasurementCreatorPermission)))
            {
                permissionDbSet.Add(Authorization.UnitMeasurementCreatorPermission.ToPermissionEntity(Authorization.UnitMeasurementManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(role => role.Code.Equals(Authorization.UnitMeasurementEditorPermission)))
            {
                permissionDbSet.Add(Authorization.UnitMeasurementEditorPermission.ToPermissionEntity(Authorization.UnitMeasurementManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(role => role.Code.Equals(Authorization.UnitMeasurementRemoverPermission)))
            {
                permissionDbSet.Add(Authorization.UnitMeasurementRemoverPermission.ToPermissionEntity(Authorization.UnitMeasurementManagementPermissionGroup));
            }
            #endregion

            #region GlobalIndicator
            if (!permissionDbSet.Any(role => role.Code.Equals(Authorization.GlobalIndicatorViewerPermission)))
            {
                permissionDbSet.Add(Authorization.GlobalIndicatorViewerPermission.ToPermissionEntity(Authorization.GlobalIndicatorManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(role => role.Code.Equals(Authorization.GlobalIndicatorCreatorPermission)))
            {
                permissionDbSet.Add(Authorization.GlobalIndicatorCreatorPermission.ToPermissionEntity(Authorization.GlobalIndicatorManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(role => role.Code.Equals(Authorization.GlobalIndicatorEditorPermission)))
            {
                permissionDbSet.Add(Authorization.GlobalIndicatorEditorPermission.ToPermissionEntity(Authorization.GlobalIndicatorManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(role => role.Code.Equals(Authorization.GlobalIndicatorRemoverPermission)))
            {
                permissionDbSet.Add(Authorization.GlobalIndicatorRemoverPermission.ToPermissionEntity(Authorization.GlobalIndicatorManagementPermissionGroup));
            }
            #endregion

            #region ObjectiveIndicator
            if (!permissionDbSet.Any(role => role.Code.Equals(Authorization.ObjectiveIndicatorViewerPermission)))
            {
                permissionDbSet.Add(Authorization.ObjectiveIndicatorViewerPermission.ToPermissionEntity(Authorization.ObjectiveIndicatorManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(role => role.Code.Equals(Authorization.ObjectiveIndicatorCreatorPermission)))
            {
                permissionDbSet.Add(Authorization.ObjectiveIndicatorCreatorPermission.ToPermissionEntity(Authorization.ObjectiveIndicatorManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(role => role.Code.Equals(Authorization.ObjectiveIndicatorEditorPermission)))
            {
                permissionDbSet.Add(Authorization.ObjectiveIndicatorEditorPermission.ToPermissionEntity(Authorization.ObjectiveIndicatorManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(role => role.Code.Equals(Authorization.ObjectiveIndicatorRemoverPermission)))
            {
                permissionDbSet.Add(Authorization.ObjectiveIndicatorRemoverPermission.ToPermissionEntity(Authorization.ObjectiveIndicatorManagementPermissionGroup));
            }
            #endregion

            #region ProyectionIndicator
            if (!permissionDbSet.Any(role => role.Code.Equals(Authorization.ProyectionIndicatorViewerPermission)))
            {
                permissionDbSet.Add(Authorization.ProyectionIndicatorViewerPermission.ToPermissionEntity(Authorization.ProyectionIndicatorManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(role => role.Code.Equals(Authorization.ProyectionIndicatorCreatorPermission)))
            {
                permissionDbSet.Add(Authorization.ProyectionIndicatorCreatorPermission.ToPermissionEntity(Authorization.ProyectionIndicatorManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(role => role.Code.Equals(Authorization.ProyectionIndicatorEditorPermission)))
            {
                permissionDbSet.Add(Authorization.ProyectionIndicatorEditorPermission.ToPermissionEntity(Authorization.ProyectionIndicatorManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(role => role.Code.Equals(Authorization.ProyectionIndicatorRemoverPermission)))
            {
                permissionDbSet.Add(Authorization.ProyectionIndicatorRemoverPermission.ToPermissionEntity(Authorization.ProyectionIndicatorManagementPermissionGroup));
            }
            #endregion

            #region ActionIndicator
            if (!permissionDbSet.Any(role => role.Code.Equals(Authorization.ActionIndicatorViewerPermission)))
            {
                permissionDbSet.Add(Authorization.ActionIndicatorViewerPermission.ToPermissionEntity(Authorization.ActionIndicatorManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(role => role.Code.Equals(Authorization.ActionIndicatorCreatorPermission)))
            {
                permissionDbSet.Add(Authorization.ActionIndicatorCreatorPermission.ToPermissionEntity(Authorization.ActionIndicatorManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(role => role.Code.Equals(Authorization.ActionIndicatorEditorPermission)))
            {
                permissionDbSet.Add(Authorization.ActionIndicatorEditorPermission.ToPermissionEntity(Authorization.ActionIndicatorManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(role => role.Code.Equals(Authorization.ActionIndicatorRemoverPermission)))
            {
                permissionDbSet.Add(Authorization.ActionIndicatorRemoverPermission.ToPermissionEntity(Authorization.ActionIndicatorManagementPermissionGroup));
            }
            #endregion

            #region GlobalIndicatorResult
            if (!permissionDbSet.Any(role => role.Code.Equals(Authorization.GlobalIndicatorResultViewerPermission)))
            {
                permissionDbSet.Add(Authorization.GlobalIndicatorResultViewerPermission.ToPermissionEntity(Authorization.GlobalIndicatorResultManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(role => role.Code.Equals(Authorization.GlobalIndicatorResultCreatorPermission)))
            {
                permissionDbSet.Add(Authorization.GlobalIndicatorResultCreatorPermission.ToPermissionEntity(Authorization.GlobalIndicatorResultManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(role => role.Code.Equals(Authorization.GlobalIndicatorResultEditorPermission)))
            {
                permissionDbSet.Add(Authorization.GlobalIndicatorResultEditorPermission.ToPermissionEntity(Authorization.GlobalIndicatorResultManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(role => role.Code.Equals(Authorization.GlobalIndicatorResultRemoverPermission)))
            {
                permissionDbSet.Add(Authorization.GlobalIndicatorResultRemoverPermission.ToPermissionEntity(Authorization.GlobalIndicatorResultManagementPermissionGroup));
            }
            #endregion

            #region ObjectiveIndicatorResult
            if (!permissionDbSet.Any(role => role.Code.Equals(Authorization.ObjectiveIndicatorResultViewerPermission)))
            {
                permissionDbSet.Add(Authorization.ObjectiveIndicatorResultViewerPermission.ToPermissionEntity(Authorization.ObjectiveIndicatorResultManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(role => role.Code.Equals(Authorization.ObjectiveIndicatorResultCreatorPermission)))
            {
                permissionDbSet.Add(Authorization.ObjectiveIndicatorResultCreatorPermission.ToPermissionEntity(Authorization.ObjectiveIndicatorResultManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(role => role.Code.Equals(Authorization.ObjectiveIndicatorResultEditorPermission)))
            {
                permissionDbSet.Add(Authorization.ObjectiveIndicatorResultEditorPermission.ToPermissionEntity(Authorization.ObjectiveIndicatorResultManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(role => role.Code.Equals(Authorization.ObjectiveIndicatorResultRemoverPermission)))
            {
                permissionDbSet.Add(Authorization.ObjectiveIndicatorResultRemoverPermission.ToPermissionEntity(Authorization.ObjectiveIndicatorResultManagementPermissionGroup));
            }
            #endregion

            #region ProyectionIndicatorResult
            if (!permissionDbSet.Any(role => role.Code.Equals(Authorization.ProyectionIndicatorResultViewerPermission)))
            {
                permissionDbSet.Add(Authorization.ProyectionIndicatorResultViewerPermission.ToPermissionEntity(Authorization.ProyectionIndicatorResultManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(role => role.Code.Equals(Authorization.ProyectionIndicatorResultCreatorPermission)))
            {
                permissionDbSet.Add(Authorization.ProyectionIndicatorResultCreatorPermission.ToPermissionEntity(Authorization.ProyectionIndicatorResultManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(role => role.Code.Equals(Authorization.ProyectionIndicatorResultEditorPermission)))
            {
                permissionDbSet.Add(Authorization.ProyectionIndicatorResultEditorPermission.ToPermissionEntity(Authorization.ProyectionIndicatorResultManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(role => role.Code.Equals(Authorization.ProyectionIndicatorResultRemoverPermission)))
            {
                permissionDbSet.Add(Authorization.ProyectionIndicatorResultRemoverPermission.ToPermissionEntity(Authorization.ProyectionIndicatorResultManagementPermissionGroup));
            }
            #endregion

            #region ActionIndicatorResult
            if (!permissionDbSet.Any(role => role.Code.Equals(Authorization.ActionIndicatorResultViewerPermission)))
            {
                permissionDbSet.Add(Authorization.ActionIndicatorResultViewerPermission.ToPermissionEntity(Authorization.ActionIndicatorResultManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(role => role.Code.Equals(Authorization.ActionIndicatorResultCreatorPermission)))
            {
                permissionDbSet.Add(Authorization.ActionIndicatorResultCreatorPermission.ToPermissionEntity(Authorization.ActionIndicatorResultManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(role => role.Code.Equals(Authorization.ActionIndicatorResultEditorPermission)))
            {
                permissionDbSet.Add(Authorization.ActionIndicatorResultEditorPermission.ToPermissionEntity(Authorization.ActionIndicatorResultManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(role => role.Code.Equals(Authorization.ActionIndicatorResultRemoverPermission)))
            {
                permissionDbSet.Add(Authorization.ActionIndicatorResultRemoverPermission.ToPermissionEntity(Authorization.ActionIndicatorResultManagementPermissionGroup));
            }
            #endregion

            if (dbContext is Framework.Guama.Core.Persistence.ORM.EF.Context.Contract.ITraceableDbContext)
            {
                (dbContext as Framework.Guama.Core.Persistence.ORM.EF.Context.Contract.ITraceableDbContext).SaveChanges("system");
            }
            else
            {
                dbContext.SaveChanges();
            }
        }
    }
}

