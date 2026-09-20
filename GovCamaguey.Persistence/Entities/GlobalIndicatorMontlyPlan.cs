namespace MOROPO.SINERGIAGOB.GovCamaguey.Persistence.Entities
{
    public class GlobalIndicatorMontlyPlan : Contract.AbstractIndicatorMontlyPlan
    {
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey(nameof(IndicatorId))]
        public GlobalIndicator Indicator { get; set; }
        public System.Guid IndicatorId { get; set; }
    }
}