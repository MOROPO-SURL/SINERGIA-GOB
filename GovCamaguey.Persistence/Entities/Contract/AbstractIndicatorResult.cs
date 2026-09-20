namespace MOROPO.SINERGIAGOB.GovCamaguey.Persistence.Entities.Contract
{
    [System.ComponentModel.DataAnnotations.Schema.NotMapped]
    public abstract class AbstractIndicatorResult : Framework.Guama.Core.Persistence.Entities.Contract.IEntity<long>
    {
        [System.ComponentModel.DataAnnotations.Key]
        public long Id { get; set; }

        public System.DateTime At { get; set; }

        public double Amount { get; set; }

        [System.ComponentModel.DataAnnotations.MaxLength(2048)]
        public string Description { get; set; }
    }
}