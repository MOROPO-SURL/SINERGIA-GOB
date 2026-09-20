using Microsoft.EntityFrameworkCore;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Web.BackOffice.Persistence.ORM.EF.Repositories;

public class ActionRepository : Framework.Guama.Core.Persistence.ORM.EF.Repositories.Contract.AbstractEntityRepository<GovCamaguey.Persistence.Entities.Action, System.Guid>
{
    public ActionRepository(DbContext dbContext) : base(dbContext)
    {
    }

    protected override System.Linq.IQueryable<GovCamaguey.Persistence.Entities.Action> GetQueryableDbSet()
    {
        return base.GetQueryableDbSet()
            //.AsNoTracking()
            .Include(entity => entity.Proyection).ThenInclude(entity => entity.Objective)
            .Include(entity => entity.OrganizationResponsabilityBridges).ThenInclude(bridge => bridge.Stern).ThenInclude(entity => entity.Identity).ThenInclude(entity => entity.Image)
            .Include(entity => entity.OrganizationParticipationBridges).ThenInclude(bridge => bridge.Stern).ThenInclude(entity => entity.Identity).ThenInclude(entity => entity.Image)
            .Include(entity => entity.MonthlyClosures)
            .Include(entity => entity.Indicators).ThenInclude(entity => entity.UnitMeasurement)
            .Include(entity => entity.Indicators).ThenInclude(entity => entity.MontlyPlans);
    }
}
