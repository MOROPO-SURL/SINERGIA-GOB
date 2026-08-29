using MOROPO.SINETGIAGOB.GovCamaguey.Persistence.Entities;

namespace MOROPO.SINETGIAGOB.GovCamaguey.Web.BackOffice.Persistence.ORM.EF.Configurators
{
    public class ActionOrganizationResponsabilityBridgeEntityTypeConfigurator : Framework.Guama.Core.Persistence.ORM.EF.Configurators.Contract.AbstractBridgeEntityTypeConfigurator<ActionOrganizationResponsabilityBridge, Action, System.Guid, Organization, System.Guid>
    {
        public ActionOrganizationResponsabilityBridgeEntityTypeConfigurator()
        {
            prowNavigationExpression = bridge => bridge.OrganizationResponsabilityBridges;
        }
    }
}
