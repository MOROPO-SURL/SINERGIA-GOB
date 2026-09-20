namespace MOROPO.SINERGIAGOB.GovCamaguey.Persistence.Entities
{
    public class UnitMeasurement : Framework.Guama.Core.Persistence.Entities.Contract.INamedEntity<System.Guid>, Core.Persistence.Entities.Contract.IFullFeatureEntity<System.Guid>
    {
        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; } = System.Guid.NewGuid();

        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.MaxLength(255)]
        public string Name { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.MaxLength(10)]
        public string Symbol { get; set; }

        public bool Archived { get; set; }

        public bool Enabled { get; set; }

        public int Priority { get; set; }
    }
}

