namespace MOROPO.SINERGIAGOB.GovCamaguey.Persistence.Entities
{
    public class GlobalIndicatorResult : Contract.AbstractIndicatorResult
    {
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey(nameof(IndicatorId))]
        public GlobalIndicator Indicator { get; set; }
        public System.Guid IndicatorId { get; set; }
    }
}
