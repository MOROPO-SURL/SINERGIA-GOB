using Microsoft.EntityFrameworkCore;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Web.BackOffice.Persistence.ORM.EF.Repositories;

public class GlobalIndicatorMontlyPlanRepository : Framework.Guama.Core.Persistence.ORM.EF.Repositories.Contract.AbstractEntityRepository<GovCamaguey.Persistence.Entities.GlobalIndicatorMontlyPlan, System.Guid>
{
    public GlobalIndicatorMontlyPlanRepository(DbContext dbContext) : base(dbContext)
    {
    }

    //protected override System.Linq.IQueryable<GovCamaguey.Persistence.Entities.GlobalIndicatorMontlyPlan> GetQueryableDbSet()
    //{
    //    return base.GetQueryableDbSet().AsNoTracking();
    //}
}