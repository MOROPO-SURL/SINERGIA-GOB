namespace MOROPO.SINERGIAGOB.GovCamaguey.Persistence.Entities.Contract
{
    [System.ComponentModel.DataAnnotations.Schema.NotMapped]
    public abstract class AbstractIndicator : Framework.Guama.Core.Persistence.Entities.Contract.INamedEntity<System.Guid>, Core.Persistence.Entities.Contract.IFullFeatureEntity<System.Guid>
    {
        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; } = System.Guid.NewGuid();

        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.MaxLength(255)]
        public string Name { get; set; }

        public double Amount { get; set; }

        public bool Enabled { get; set; }


        public bool Archived { get; set; }

        [System.ComponentModel.DataAnnotations.MaxLength(1024)]
        public string Description { get; set; }

        public int Priority { get; set; }

        public UnitMeasurement UnitMeasurement { get; set; }
        public System.Guid UnitMeasurementId { get; set; }
    }
}