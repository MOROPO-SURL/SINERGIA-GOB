using Microsoft.EntityFrameworkCore;

namespace MOROPO.SINETGIAGOB.GovCamaguey.Web.BackOffice.Persistence.ORM.EF.Repositories;

public class ObjectiveIndicatorResultRepository : Framework.Guama.Core.Persistence.ORM.EF.Repositories.Contract.AbstractEntityRepository<GovCamaguey.Persistence.Entities.ObjectiveIndicatorResult, long>
{
    public ObjectiveIndicatorResultRepository(DbContext dbContext) : base(dbContext)
    {
    }

    protected override System.Linq.IQueryable<GovCamaguey.Persistence.Entities.ObjectiveIndicatorResult> GetQueryableDbSet()
    {
        return base.GetQueryableDbSet()
            //.AsNoTracking()
            .AsSplitQuery()
            .Include(entity => entity.Indicator).ThenInclude(entity => entity.UnitMeasurement)
            .Include(entity => entity.Indicator).ThenInclude(entity => entity.Objective);
    }
}
