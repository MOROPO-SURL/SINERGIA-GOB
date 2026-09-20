namespace MOROPO.SINERGIAGOB.GovCamaguey.Persistence.Entities
{
    public class ActionIndicator : Contract.AbstractIndicator
    {
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey(nameof(ActionId))]
        public Action Action { get; set; }
        public System.Guid ActionId { get; set; }

        public System.Collections.Generic.ICollection<ActionIndicatorMontlyPlan> MontlyPlans { get; set; } = new System.Collections.Generic.List<ActionIndicatorMontlyPlan>();
    }
}