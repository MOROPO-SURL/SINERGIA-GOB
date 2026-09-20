using Microsoft.EntityFrameworkCore;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Web.BackOffice.Persistence.ORM.EF.Repositories;

public class ProyectionIndicatorResultRepository : Framework.Guama.Core.Persistence.ORM.EF.Repositories.Contract.AbstractEntityRepository<GovCamaguey.Persistence.Entities.ProyectionIndicatorResult, long>
{
    public ProyectionIndicatorResultRepository(DbContext dbContext) : base(dbContext)
    {
    }

    protected override System.Linq.IQueryable<GovCamaguey.Persistence.Entities.ProyectionIndicatorResult> GetQueryableDbSet()
    {
        return base.GetQueryableDbSet()
            //.AsNoTracking()
            .AsSplitQuery()
            .Include(entity => entity.Indicator).ThenInclude(entity => entity.UnitMeasurement)
            .Include(entity => entity.Indicator).ThenInclude(entity => entity.Proyection).ThenInclude(entity => entity.Objective);
    }
}
