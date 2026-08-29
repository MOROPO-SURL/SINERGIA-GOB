using MOROPO.SINETGIAGOB.GovCamaguey.Persistence.Entities;

namespace MOROPO.SINETGIAGOB.GovCamaguey.Web.BackOffice.Persistence.ORM.EF.Configurators;

public class ActionOrganizationParticipationBridgeEntityTypeConfigurator : Framework.Guama.Core.Persistence.ORM.EF.Configurators.Contract.AbstractBridgeEntityTypeConfigurator<ActionOrganizationParticipationBridge, Action, System.Guid, Organization, System.Guid>
{
    public ActionOrganizationParticipationBridgeEntityTypeConfigurator()
    {
        prowNavigationExpression = bridge => bridge.OrganizationParticipationBridges;
    }
}
