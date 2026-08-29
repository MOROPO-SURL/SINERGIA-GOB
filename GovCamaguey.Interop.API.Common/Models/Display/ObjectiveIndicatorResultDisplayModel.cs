namespace MOROPO.SINETGIAGOB.GovCamaguey.Interop.API.Common.Models.Display
{
    public class ObjectiveIndicatorResultDisplayModel : Contract.AbstractIndicatorResultDisplayModel<GovCamaguey.Persistence.Entities.ObjectiveIndicatorResult>
    {
        public System.Guid IndicatorId { get; set; }

        public override void Import(GovCamaguey.Persistence.Entities.ObjectiveIndicatorResult entity)
        {
            base.Import(entity);

            IndicatorId = entity.IndicatorId;
        }
    }
}
