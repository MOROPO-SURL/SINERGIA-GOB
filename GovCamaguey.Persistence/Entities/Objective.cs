using MOROPO.Framework.Guama.Core.Persistence.Entities.Contract;
using System.Collections.Generic;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Persistence.Entities
{
    public class Objective : INamedEntity<System.Guid>, IArchivableEntity<System.Guid>, IEnableableEntity<System.Guid>
    {
        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; } = System.Guid.NewGuid();

        public bool Archived { get; set; }

        public bool Enabled { get; set; }

        public int Index { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.MaxLength(1024)]
        public string Name { get; set; }

        [System.ComponentModel.DataAnnotations.MaxLength(5120)]
        public string Description { get; set; }

        public Person Responsible { get; set; }
        public System.Guid ResponsibleId { get; set; }

        public List<ObjectiveUserBridge> ObjectiveUserBridges { get; set; } = new List<ObjectiveUserBridge>();

        public ICollection<Proyection> Proyections { get; set; }

        public ICollection<ObjectiveIndicator> Indicators { get; set; } = new List<ObjectiveIndicator>();
    }
}
