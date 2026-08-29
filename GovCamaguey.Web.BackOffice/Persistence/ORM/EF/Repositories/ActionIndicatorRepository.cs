using Microsoft.EntityFrameworkCore;

namespace MOROPO.SINETGIAGOB.GovCamaguey.Web.BackOffice.Persistence.ORM.EF.Repositories;

public class ActionIndicatorRepository : Framework.Guama.Core.Persistence.ORM.EF.Repositories.Contract.AbstractEntityRepository<GovCamaguey.Persistence.Entities.ActionIndicator, System.Guid>
{
    public ActionIndicatorRepository(DbContext dbContext) : base(dbContext)
    {
    }

    protected override System.Linq.IQueryable<GovCamaguey.Persistence.Entities.ActionIndicator> GetQueryableDbSet()
    {
        return base.GetQueryableDbSet()
            //.AsNoTracking()
            .Include(entity => entity.UnitMeasurement)
            .Include(entity => entity.Action).ThenInclude(entity => entity.Proyection).ThenInclude(entity => entity.Objective);
    }
}
