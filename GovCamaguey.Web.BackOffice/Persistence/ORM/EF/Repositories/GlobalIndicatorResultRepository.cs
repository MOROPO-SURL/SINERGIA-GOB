using Microsoft.EntityFrameworkCore;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Web.BackOffice.Persistence.ORM.EF.Repositories;

public class GlobalIndicatorResultRepository : Framework.Guama.Core.Persistence.ORM.EF.Repositories.Contract.AbstractEntityRepository<GovCamaguey.Persistence.Entities.GlobalIndicatorResult, long>
{
    public GlobalIndicatorResultRepository(DbContext dbContext) : base(dbContext)
    {
    }

    //protected override System.Linq.IQueryable<GovCamaguey.Persistence.Entities.GlobalIndicatorResult> GetQueryableDbSet()
    //{
    //    return base.GetQueryableDbSet().AsNoTracking();
    //}
}