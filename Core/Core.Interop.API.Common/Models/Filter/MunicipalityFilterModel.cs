namespace MOROPO.SINERGIAGOB.Core.Interop.API.Common.Models.Filter
{
    public class MunicipalityFilterModel : Framework.Guama.Core.Persistence.Presentation.Models.Filter.Contract.IEntityFilterModel<Framework.Guama.Location.Persistence.Entities.Municipality, System.Guid>
    {
        public System.Guid StateId { get; set; }

        public System.Linq.Expressions.Expression<System.Func<Framework.Guama.Location.Persistence.Entities.Municipality, bool>> GetSelector()
        {
            return entity => entity.StateId == StateId;
        }
    }
}
