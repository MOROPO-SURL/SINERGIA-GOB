namespace MOROPO.SINETGIAGOB.GovCamaguey.Interop.API.Common.Models.Filter
{
    public class GlobalIndicatorFilterModel : Framework.Guama.Core.Persistence.Presentation.Models.Filter.Contract.IEntityFilterModel<GovCamaguey.Persistence.Entities.GlobalIndicator, System.Guid>
    {
        public string Name { get; set; }

        public System.Linq.Expressions.Expression<System.Func<GovCamaguey.Persistence.Entities.GlobalIndicator, bool>> GetSelector()
        {
            return entity => entity.Name.ToLower().Contains(Name.ToLower());
        }
    }
}