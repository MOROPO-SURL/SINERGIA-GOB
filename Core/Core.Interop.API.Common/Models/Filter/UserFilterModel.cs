namespace MOROPO.SINERGIAGOB.Core.Interop.API.Common.Models.Filter
{
    public class UserFilterModel : Framework.Guama.Core.Persistence.Presentation.Models.Filter.Contract.IEntityFilterModel<Framework.Guama.Security.Personal.Persistence.Entities.User, string>
    {
        public string Parameter { get; set; }

        public System.Linq.Expressions.Expression<System.Func<Framework.Guama.Security.Personal.Persistence.Entities.User, bool>> GetSelector()
        {
            return entity => entity.Id.ToLower().Contains(Parameter.ToLower()) || entity.Identity.Name.ToLower().Contains(Parameter.ToLower()) || entity.Identity.LastName.ToLower().Contains(Parameter.ToLower());
        }
    }
}
