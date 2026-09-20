using Microsoft.EntityFrameworkCore;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Web.BackOffice.Persistence.ORM.EF.Repositories;

public class OrganizationRepository : Framework.Guama.Core.Persistence.ORM.EF.Repositories.Contract.AbstractEntityRepository<GovCamaguey.Persistence.Entities.Organization, System.Guid>
{
    public OrganizationRepository(DbContext dbContext) : base(dbContext)
    {
    }

    protected override System.Linq.IQueryable<GovCamaguey.Persistence.Entities.Organization> GetQueryableDbSet()
    {
        return base.GetQueryableDbSet()
            //.AsNoTracking()
            .AsSplitQuery()
            .Include(entity => entity.Identity).ThenInclude(entity => entity.Image)
            .Include(entity => entity.Identity).ThenInclude(entity => entity.PostalContactItemBridges).ThenInclude(bridge => bridge.Stern).ThenInclude(entity => entity.Location)
            .Include(entity => entity.Identity).ThenInclude(entity => entity.PhoneContactItemBridges).ThenInclude(bridge => bridge.Stern)
            .Include(entity => entity.Identity).ThenInclude(entity => entity.EmailContactItemBridges).ThenInclude(bridge => bridge.Stern)
            .Include(entity => entity.Identity).ThenInclude(entity => entity.WebSiteContactItemBridges).ThenInclude(bridge => bridge.Stern);
    }
}
