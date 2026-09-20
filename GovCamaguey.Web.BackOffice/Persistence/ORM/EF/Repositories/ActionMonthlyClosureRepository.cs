using Microsoft.EntityFrameworkCore;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Web.BackOffice.Persistence.ORM.EF.Repositories;

public class ActionMonthlyClosureRepository : Framework.Guama.Core.Persistence.ORM.EF.Repositories.Contract.AbstractEntityRepository<GovCamaguey.Persistence.Entities.ActionMonthlyClosure, System.Guid>
{
    public ActionMonthlyClosureRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
    {
    }

    //protected override System.Linq.IQueryable<GovCamaguey.Persistence.Entities.ActionMonthlyClosure> GetQueryableDbSet()
    //{
    //    return base.GetQueryableDbSet().AsNoTracking();
    //}
}
