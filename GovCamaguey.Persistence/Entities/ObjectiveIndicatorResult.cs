namespace MOROPO.SINETGIAGOB.GovCamaguey.Persistence.Entities
{
    public class ObjectiveIndicatorResult : Contract.AbstractIndicatorResult
    {
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey(nameof(IndicatorId))]
        public ObjectiveIndicator Indicator { get; set; }
        public System.Guid IndicatorId { get; set; }
    }
}
