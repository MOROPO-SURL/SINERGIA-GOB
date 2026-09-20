using Microsoft.EntityFrameworkCore;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Web.BackOffice.Persistence.ORM.EF.Repositories;

public class ProyectionIndicatorMontlyPlanRepository : Framework.Guama.Core.Persistence.ORM.EF.Repositories.Contract.AbstractEntityRepository<GovCamaguey.Persistence.Entities.ProyectionIndicatorMontlyPlan, System.Guid>
{
    public ProyectionIndicatorMontlyPlanRepository(DbContext dbContext) : base(dbContext)
    {
    }

    //protected override System.Linq.IQueryable<GovCamaguey.Persistence.Entities.ProyectionIndicatorMontlyPlan> GetQueryableDbSet()
    //{
    //    return base.GetQueryableDbSet().AsNoTracking();
    //}
}
