namespace MOROPO.SINERGIAGOB.GovCamaguey.Interop.API.Common.Models.Display
{
    public class ActionIndicatorResultDisplayModel : Contract.AbstractIndicatorResultDisplayModel<GovCamaguey.Persistence.Entities.ActionIndicatorResult>
    {
        public System.Guid IndicatorId { get; set; }

        public override void Import(GovCamaguey.Persistence.Entities.ActionIndicatorResult entity)
        {
            base.Import(entity);

            IndicatorId = entity.IndicatorId;
        }
    }
}