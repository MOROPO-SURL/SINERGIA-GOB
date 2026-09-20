using Microsoft.EntityFrameworkCore;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Web.BackOffice.Persistence.ORM.EF.Repositories;

public class ObjectiveIndicatorRepository : Framework.Guama.Core.Persistence.ORM.EF.Repositories.Contract.AbstractEntityRepository<GovCamaguey.Persistence.Entities.ObjectiveIndicator, System.Guid>
{
    public ObjectiveIndicatorRepository(DbContext dbContext) : base(dbContext)
    {
    }

    protected override System.Linq.IQueryable<GovCamaguey.Persistence.Entities.ObjectiveIndicator> GetQueryableDbSet()
    {
        return base.GetQueryableDbSet()
            //.AsNoTracking()
            .Include(entity => entity.UnitMeasurement);
    }
}
