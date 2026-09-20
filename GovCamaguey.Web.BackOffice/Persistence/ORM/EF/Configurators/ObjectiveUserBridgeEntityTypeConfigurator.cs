using MOROPO.SINERGIAGOB.GovCamaguey.Persistence.Entities;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Web.BackOffice.Persistence.ORM.EF.Configurators;

public class ObjectiveUserBridgeEntityTypeConfigurator : Framework.Guama.Core.Persistence.ORM.EF.Configurators.Contract.AbstractBridgeEntityTypeConfigurator<ObjectiveUserBridge, Objective, System.Guid, Framework.Guama.Security.Personal.Persistence.Entities.User, string>
{
    public ObjectiveUserBridgeEntityTypeConfigurator()
    {
        prowNavigationExpression = bridge => bridge.ObjectiveUserBridges;
    }
}