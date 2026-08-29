namespace MOROPO.SINETGIAGOB.GovCamaguey.Persistence.Entities.Contract
{
    [System.ComponentModel.DataAnnotations.Schema.NotMapped]
    public abstract class AbstractIndicatorMontlyPlan : Framework.Guama.Core.Persistence.Entities.Contract.IEntity<System.Guid>
    {
        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; } = System.Guid.NewGuid();

        public int Month { get; set; }

        public int Year { get; set; }

        public double Amount { get; set; }
    }
}