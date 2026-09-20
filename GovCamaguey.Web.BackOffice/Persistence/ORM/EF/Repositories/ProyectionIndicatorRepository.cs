using Microsoft.EntityFrameworkCore;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Web.BackOffice.Persistence.ORM.EF.Repositories;

public class ProyectionIndicatorRepository : Framework.Guama.Core.Persistence.ORM.EF.Repositories.Contract.AbstractEntityRepository<GovCamaguey.Persistence.Entities.ProyectionIndicator, System.Guid>
{
    public ProyectionIndicatorRepository(DbContext dbContext) : base(dbContext)
    {
    }

    protected override System.Linq.IQueryable<GovCamaguey.Persistence.Entities.ProyectionIndicator> GetQueryableDbSet()
    {
        return base.GetQueryableDbSet()
            //.AsNoTracking()
            .Include(entity => entity.UnitMeasurement)
            .Include(entity => entity.Proyection).ThenInclude(entity => entity.Objective);
    }
}