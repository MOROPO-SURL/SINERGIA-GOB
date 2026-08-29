using MOROPO.SINETGIAGOB.GovCamaguey.Persistence.Entities;

namespace MOROPO.SINETGIAGOB.GovCamaguey.Web.BackOffice.Persistence.ORM.EF.Configurators;

public class ProyectionUserBridgeEntityTypeConfigurator : Framework.Guama.Core.Persistence.ORM.EF.Configurators.Contract.AbstractBridgeEntityTypeConfigurator<ProyectionUserBridge, Proyection, System.Guid, Framework.Guama.Security.Personal.Persistence.Entities.User, string>
{
    public ProyectionUserBridgeEntityTypeConfigurator()
    {
        prowNavigationExpression = bridge => bridge.ProyectionUserBridges;
    }
}
