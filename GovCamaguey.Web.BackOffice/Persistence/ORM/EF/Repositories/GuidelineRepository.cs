using Microsoft.EntityFrameworkCore;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Web.BackOffice.Persistence.ORM.EF.Repositories;

public class GuidelineRepository : Framework.Guama.Core.Persistence.ORM.EF.Repositories.Contract.AbstractEntityRepository<GovCamaguey.Persistence.Entities.Guideline, System.Guid>
{
    public GuidelineRepository(DbContext dbContext) : base(dbContext)
    {
    }

    //protected override System.Linq.IQueryable<GovCamaguey.Persistence.Entities.Guideline> GetQueryableDbSet()
    //{
    //    return base.GetQueryableDbSet().AsNoTracking();
    //}
}
