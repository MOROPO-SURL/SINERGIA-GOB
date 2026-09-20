using MOROPO.Framework.Guama.Core.Persistence.Entities.Contract;

namespace MOROPO.SINERGIAGOB.Core.Persistence.Entities.Contract
{
    [System.ComponentModel.DataAnnotations.Schema.NotMapped]
    public abstract class AbstractNomenclator : INamedEntity<System.Guid>, IFullFeatureEntity<System.Guid>
    {
        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; } = System.Guid.NewGuid();

        public bool Enabled { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public System.DateTime At { get; set; } = System.DateTime.Now;

        public bool Archived { get; set; }

        public int Priority { get; set; }
    }
}
