using MOROPO.SINERGIAGOB.GovCamaguey.Persistence.Entities;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Web.BackOffice.Persistence.ORM.EF.Configurators;

public class ActionOrganizationParticipationBridgeEntityTypeConfigurator : Framework.Guama.Core.Persistence.ORM.EF.Configurators.Contract.AbstractBridgeEntityTypeConfigurator<ActionOrganizationParticipationBridge, Action, System.Guid, Organization, System.Guid>
{
    public ActionOrganizationParticipationBridgeEntityTypeConfigurator()
    {
        prowNavigationExpression = bridge => bridge.OrganizationParticipationBridges;
    }
}
