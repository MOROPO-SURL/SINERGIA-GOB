using MOROPO.Framework.Guama.Core.Persistence.Entities.Contract;
using System.Collections.Generic;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Persistence.Entities
{
    public class Action : INamedEntity<System.Guid>, IArchivableEntity<System.Guid>, IEnableableEntity<System.Guid>
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

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey(nameof(ProyectionId))]
        public Proyection Proyection { get; set; }
        public System.Guid ProyectionId { get; set; }

        public ICollection<ActionMonthlyClosure> MonthlyClosures { get; set; }

        public List<ActionOrganizationResponsabilityBridge> OrganizationResponsabilityBridges { get; set; } = new List<ActionOrganizationResponsabilityBridge>();

        public List<ActionOrganizationParticipationBridge> OrganizationParticipationBridges { get; set; } = new List<ActionOrganizationParticipationBridge>();

        public List<ActionIndicator> Indicators { get; set; } = new List<ActionIndicator>();

        public List<ActionUserBridge> ActionUserBridges { get; set; } = new List<ActionUserBridge>();
    }
}
