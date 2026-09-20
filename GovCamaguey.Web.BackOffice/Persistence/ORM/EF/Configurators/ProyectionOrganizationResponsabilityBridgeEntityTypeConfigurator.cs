using MOROPO.SINERGIAGOB.GovCamaguey.Persistence.Entities;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Web.BackOffice.Persistence.ORM.EF.Configurators
{
    public class ProyectionOrganizationResponsabilityBridgeEntityTypeConfigurator : Framework.Guama.Core.Persistence.ORM.EF.Configurators.Contract.AbstractBridgeEntityTypeConfigurator<ProyectionOrganizationResponsabilityBridge, Proyection, System.Guid, Organization, System.Guid>
    {
        public ProyectionOrganizationResponsabilityBridgeEntityTypeConfigurator()
        {
            prowNavigationExpression = bridge => bridge.OrganizationResponsabilityBridges;
        }
    }
}
