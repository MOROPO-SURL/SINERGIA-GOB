using Microsoft.EntityFrameworkCore;
using MOROPO.SINETGIAGOB.GovCamaguey.Persistence.Entities;
using System.Linq;

namespace MOROPO.SINETGIAGOB.GovCamaguey.Web.BackOffice.Persistence.ORM.EF.Repositories;

public class ObjectiveRepository : Framework.Guama.Core.Persistence.ORM.EF.Repositories.Contract.AbstractEntityRepository<GovCamaguey.Persistence.Entities.Objective, System.Guid>
{
    public ObjectiveRepository(DbContext dbContext) : base(dbContext)
    {
    }

    protected override IQueryable<Objective> GetQueryableDbSet()
    {
        return base.GetQueryableDbSet()
            //.AsNoTracking()
            .AsSplitQuery()
            .Include(entity => entity.Responsible).ThenInclude(entity => entity.Identity).ThenInclude(entity => entity.Image)
            .Include(entity => entity.Indicators).ThenInclude(entity => entity.UnitMeasurement)
            .Include(entity => entity.Indicators).ThenInclude(entity => entity.MontlyPlans)
            .Include(entity => entity.Proyections).ThenInclude(entity => entity.GuidelineBridges).ThenInclude(bridge => bridge.Stern)
            .Include(entity => entity.Proyections).ThenInclude(entity => entity.OrganizationResponsabilityBridges).ThenInclude(bridge => bridge.Stern).ThenInclude(entity => entity.Identity).ThenInclude(entity => entity.Image)
            .Include(entity => entity.Proyections).ThenInclude(entity => entity.OrganizationParticipationBridges).ThenInclude(bridge => bridge.Stern).ThenInclude(entity => entity.Identity).ThenInclude(entity => entity.Image)
            .Include(entity => entity.Proyections).ThenInclude(entity => entity.Indicators).ThenInclude(entity => entity.UnitMeasurement)
            .Include(entity => entity.Proyections).ThenInclude(entity => entity.Indicators).ThenInclude(entity => entity.MontlyPlans)
            .Include(entity => entity.Proyections).ThenInclude(entity => entity.Actions).ThenInclude(entity => entity.OrganizationResponsabilityBridges).ThenInclude(bridge => bridge.Stern).ThenInclude(entity => entity.Identity).ThenInclude(entity => entity.Image)
            .Include(entity => entity.Proyections).ThenInclude(entity => entity.Actions).ThenInclude(entity => entity.OrganizationParticipationBridges).ThenInclude(bridge => bridge.Stern).ThenInclude(entity => entity.Identity).ThenInclude(entity => entity.Image)
            .Include(entity => entity.Proyections).ThenInclude(entity => entity.Actions).ThenInclude(entity => entity.MonthlyClosures)
            .Include(entity => entity.Proyections).ThenInclude(entity => entity.Actions).ThenInclude(entity => entity.Indicators).ThenInclude(entity => entity.UnitMeasurement)
            .Include(entity => entity.Proyections).ThenInclude(entity => entity.Actions).ThenInclude(entity => entity.Indicators).ThenInclude(entity => entity.MontlyPlans);
    }
}