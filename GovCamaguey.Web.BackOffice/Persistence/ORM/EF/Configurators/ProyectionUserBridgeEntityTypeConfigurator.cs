using MOROPO.SINERGIAGOB.GovCamaguey.Persistence.Entities;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Web.BackOffice.Persistence.ORM.EF.Configurators;

public class ProyectionUserBridgeEntityTypeConfigurator : Framework.Guama.Core.Persistence.ORM.EF.Configurators.Contract.AbstractBridgeEntityTypeConfigurator<ProyectionUserBridge, Proyection, System.Guid, Framework.Guama.Security.Personal.Persistence.Entities.User, string>
{
    public ProyectionUserBridgeEntityTypeConfigurator()
    {
        prowNavigationExpression = bridge => bridge.ProyectionUserBridges;
    }
}
