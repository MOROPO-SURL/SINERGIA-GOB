using Microsoft.EntityFrameworkCore;

namespace MOROPO.SINETGIAGOB.GovCamaguey.Web.BackOffice.Persistence.ORM.EF.Repositories;

public class ActionIndicatorMontlyPlanRepository : Framework.Guama.Core.Persistence.ORM.EF.Repositories.Contract.AbstractEntityRepository<GovCamaguey.Persistence.Entities.ActionIndicatorMontlyPlan, System.Guid>
{
    public ActionIndicatorMontlyPlanRepository(DbContext dbContext) : base(dbContext)
    {
    }

    //protected override System.Linq.IQueryable<GovCamaguey.Persistence.Entities.ActionIndicatorMontlyPlan> GetQueryableDbSet()
    //{
    //    return base.GetQueryableDbSet().AsNoTracking();
    //}
}