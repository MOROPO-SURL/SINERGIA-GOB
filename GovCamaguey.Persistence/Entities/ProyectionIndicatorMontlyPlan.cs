namespace MOROPO.SINETGIAGOB.GovCamaguey.Persistence.Entities
{
    public class ProyectionIndicatorMontlyPlan : Contract.AbstractIndicatorMontlyPlan
    {
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey(nameof(IndicatorId))]
        public ProyectionIndicator Indicator { get; set; }
        public System.Guid IndicatorId { get; set; }
    }
}
