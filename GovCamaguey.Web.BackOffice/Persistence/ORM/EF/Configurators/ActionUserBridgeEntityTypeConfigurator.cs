using MOROPO.SINERGIAGOB.GovCamaguey.Persistence.Entities;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Web.BackOffice.Persistence.ORM.EF.Configurators;

public class ActionUserBridgeEntityTypeConfigurator : Framework.Guama.Core.Persistence.ORM.EF.Configurators.Contract.AbstractBridgeEntityTypeConfigurator<ActionUserBridge, Action, System.Guid, Framework.Guama.Security.Personal.Persistence.Entities.User, string>
{
    public ActionUserBridgeEntityTypeConfigurator()
    {
        prowNavigationExpression = bridge => bridge.ActionUserBridges;
    }
}