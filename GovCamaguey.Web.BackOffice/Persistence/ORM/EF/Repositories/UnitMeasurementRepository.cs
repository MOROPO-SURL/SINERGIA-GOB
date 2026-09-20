using Microsoft.EntityFrameworkCore;
using MOROPO.SINERGIAGOB.GovCamaguey.Persistence.Entities;
using System.Linq;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Web.BackOffice.Persistence.ORM.EF.Repositories;

public class UnitMeasurementRepository : Framework.Guama.Core.Persistence.ORM.EF.Repositories.Contract.AbstractEntityRepository<GovCamaguey.Persistence.Entities.UnitMeasurement, System.Guid>
{
    public UnitMeasurementRepository(DbContext dbContext) : base(dbContext)
    {
    }

    //protected override IQueryable<UnitMeasurement> GetQueryableDbSet()
    //{
    //    return base.GetQueryableDbSet().AsNoTracking();
    //}
}