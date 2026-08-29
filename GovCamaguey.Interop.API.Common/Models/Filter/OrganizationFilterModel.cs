namespace MOROPO.SINETGIAGOB.GovCamaguey.Interop.API.Common.Models.Filter
{
    public sealed class OrganizationFilterModel : Framework.Guama.Core.Persistence.Presentation.Models.Filter.Contract.IEntityFilterModel<GovCamaguey.Persistence.Entities.Organization, System.Guid>
    {
        public string Name { get; set; }

        public System.Linq.Expressions.Expression<System.Func<GovCamaguey.Persistence.Entities.Organization, bool>> GetSelector()
        {
            return entity => entity.Identity.Name.ToLower().Contains(Name.ToLower());
        }
    }
}
