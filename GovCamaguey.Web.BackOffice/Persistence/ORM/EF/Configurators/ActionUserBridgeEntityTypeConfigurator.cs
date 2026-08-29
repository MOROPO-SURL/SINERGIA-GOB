using MOROPO.SINETGIAGOB.GovCamaguey.Persistence.Entities;

namespace MOROPO.SINETGIAGOB.GovCamaguey.Web.BackOffice.Persistence.ORM.EF.Configurators;

public class ActionUserBridgeEntityTypeConfigurator : Framework.Guama.Core.Persistence.ORM.EF.Configurators.Contract.AbstractBridgeEntityTypeConfigurator<ActionUserBridge, Action, System.Guid, Framework.Guama.Security.Personal.Persistence.Entities.User, string>
{
    public ActionUserBridgeEntityTypeConfigurator()
    {
        prowNavigationExpression = bridge => bridge.ActionUserBridges;
    }
}