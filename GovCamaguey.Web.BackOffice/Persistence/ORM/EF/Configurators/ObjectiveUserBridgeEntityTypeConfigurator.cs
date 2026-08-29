using MOROPO.SINETGIAGOB.GovCamaguey.Persistence.Entities;

namespace MOROPO.SINETGIAGOB.GovCamaguey.Web.BackOffice.Persistence.ORM.EF.Configurators;

public class ObjectiveUserBridgeEntityTypeConfigurator : Framework.Guama.Core.Persistence.ORM.EF.Configurators.Contract.AbstractBridgeEntityTypeConfigurator<ObjectiveUserBridge, Objective, System.Guid, Framework.Guama.Security.Personal.Persistence.Entities.User, string>
{
    public ObjectiveUserBridgeEntityTypeConfigurator()
    {
        prowNavigationExpression = bridge => bridge.ObjectiveUserBridges;
    }
}