namespace MOROPO.SINETGIAGOB.GovCamaguey.Persistence.Entities
{
    public class ActionIndicatorResult : Contract.AbstractIndicatorResult
    {
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey(nameof(IndicatorId))]
        public ActionIndicator Indicator { get; set; }
        public System.Guid IndicatorId { get; set; }
    }
}