using Microsoft.EntityFrameworkCore;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Web.BackOffice.Persistence.ORM.EF.Repositories;

public class ProyectionRepository : Framework.Guama.Core.Persistence.ORM.EF.Repositories.Contract.AbstractEntityRepository<GovCamaguey.Persistence.Entities.Proyection, System.Guid>
{
    public ProyectionRepository(DbContext dbContext) : base(dbContext)
    {
    }

    protected override System.Linq.IQueryable<GovCamaguey.Persistence.Entities.Proyection> GetQueryableDbSet()
    {
        return base.GetQueryableDbSet()
            //.AsNoTracking()
            .AsSplitQuery()
            .Include(entity => entity.Objective)
            .Include(entity => entity.MonthlyClosures)
            .Include(entity => entity.ElementSource)
            .Include(entity => entity.GuidelineBridges).ThenInclude(bridge => bridge.Stern)
            .Include(entity => entity.OrganizationResponsabilityBridges).ThenInclude(bridge => bridge.Stern).ThenInclude(entity => entity.Identity).ThenInclude(entity => entity.Image)
            .Include(entity => entity.OrganizationParticipationBridges).ThenInclude(bridge => bridge.Stern).ThenInclude(entity => entity.Identity).ThenInclude(entity => entity.Image)
            .Include(entity => entity.Indicators).ThenInclude(entity => entity.UnitMeasurement)
            .Include(entity => entity.Indicators).ThenInclude(entity => entity.MontlyPlans)
            .Include(entity => entity.Actions).ThenInclude(entity => entity.OrganizationResponsabilityBridges).ThenInclude(bridge => bridge.Stern).ThenInclude(entity => entity.Identity).ThenInclude(entity => entity.Image)
            .Include(entity => entity.Actions).ThenInclude(entity => entity.OrganizationParticipationBridges).ThenInclude(bridge => bridge.Stern).ThenInclude(entity => entity.Identity).ThenInclude(entity => entity.Image)
            .Include(entity => entity.Actions).ThenInclude(entity => entity.ElementSource)
            .Include(entity => entity.Actions).ThenInclude(entity => entity.MonthlyClosures)
            .Include(entity => entity.Actions).ThenInclude(entity => entity.Indicators).ThenInclude(entity => entity.UnitMeasurement)
            .Include(entity => entity.Actions).ThenInclude(entity => entity.Indicators).ThenInclude(entity => entity.MontlyPlans);
    }
}
