namespace MOROPO.SINERGIAGOB.GovCamaguey.Persistence.Entities
{
    public class ObjectiveIndicator : Contract.AbstractIndicator
    {
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey(nameof(ObjectiveId))]
        public Objective Objective { get; set; }
        public System.Guid ObjectiveId { get; set; }

        public System.Collections.Generic.ICollection<ObjectiveIndicatorMontlyPlan> MontlyPlans { get; set; } = new System.Collections.Generic.List<ObjectiveIndicatorMontlyPlan>();
    }
}
