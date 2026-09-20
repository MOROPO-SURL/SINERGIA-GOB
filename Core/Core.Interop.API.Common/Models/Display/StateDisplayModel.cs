namespace MOROPO.SINERGIAGOB.Core.Interop.API.Common.Models.Display
{
    public class StateDisplayModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityDisplayModel<Framework.Guama.Location.Persistence.Entities.State, System.Guid>
    {
        public System.Guid Id { get; set; }

        public string Name { get; set; }

        public void Import(Framework.Guama.Location.Persistence.Entities.State entity)
        {
            Id = entity.Id;
            Name = entity.Name;
        }
    }
}
