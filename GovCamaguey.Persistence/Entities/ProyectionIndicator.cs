namespace MOROPO.SINETGIAGOB.GovCamaguey.Persistence.Entities
{
    public class ProyectionIndicator : Contract.AbstractIndicator
    {
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey(nameof(ProyectionId))]
        public Proyection Proyection { get; set; }
        public System.Guid ProyectionId { get; set; }

        public System.Collections.Generic.ICollection<ProyectionIndicatorMontlyPlan> MontlyPlans { get; set; } = new System.Collections.Generic.List<ProyectionIndicatorMontlyPlan>();
    }
}