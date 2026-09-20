using Microsoft.EntityFrameworkCore;
using MOROPO.SINERGIAGOB.GovCamaguey.Persistence.Entities;
using System.Linq;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Web.BackOffice.Persistence.ORM.EF.Repositories;

public class ActionUserBridgeRepository : Framework.Guama.Core.Persistence.ORM.EF.Repositories.Contract.AbstractBridgeRepository<ActionUserBridge, Action, System.Guid, Framework.Guama.Security.Personal.Persistence.Entities.User, string>
{
    public ActionUserBridgeRepository(DbContext dbContext) : base(dbContext)
    {
    }

    protected override IQueryable<ActionUserBridge> GetQueryableDbSet()
    {
        return base.GetQueryableDbSet().Include(bridge => bridge.Stern).ThenInclude(entity => entity.Identity);
    }
}
