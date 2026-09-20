namespace MOROPO.SINERGIAGOB.GovCamaguey.Persistence.Entities
{
    public class GlobalIndicator : Contract.AbstractIndicator
    {
        public System.Collections.Generic.ICollection<GlobalIndicatorMontlyPlan> MontlyPlans { get; set; } = new System.Collections.Generic.List<GlobalIndicatorMontlyPlan>();
    }
}
