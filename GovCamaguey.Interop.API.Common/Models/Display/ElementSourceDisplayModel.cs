namespace MOROPO.SINERGIAGOB.GovCamaguey.Interop.API.Common.Models.Display
{
    public class ElementSourceDisplayModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityDisplayModel<Persistence.Entities.ElementSource, System.Guid>
    {
        public System.Guid Id { get; set; }

        public bool Enabled { get; set; }

        public string Name { get; set; }

        public string Alias { get; set; }

        public string Description { get; set; }

        public int Priority { get; set; }

        public virtual void Import(Persistence.Entities.ElementSource entity)
        {
            Id = entity.Id;
            Enabled = entity.Enabled;
            Name = entity.Name;
            Alias = entity.Alias;
            Description = entity.Description;
            Priority = entity.Priority;
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(Alias)) return Name;

            return $"{Name} ({Alias})";
        }
    }
}
