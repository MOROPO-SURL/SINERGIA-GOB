namespace MOROPO.SINETGIAGOB.GovCamaguey.Persistence.Entities
{
    public class ProyectionIndicatorResult : Contract.AbstractIndicatorResult
    {
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey(nameof(IndicatorId))]
        public ProyectionIndicator Indicator { get; set; }
        public System.Guid IndicatorId { get; set; }
    }
}
