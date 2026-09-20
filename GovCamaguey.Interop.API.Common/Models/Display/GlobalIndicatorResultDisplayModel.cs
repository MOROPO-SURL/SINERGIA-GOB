namespace MOROPO.SINERGIAGOB.GovCamaguey.Interop.API.Common.Models.Display
{
    public class GlobalIndicatorResultDisplayModel : Contract.AbstractIndicatorResultDisplayModel<GovCamaguey.Persistence.Entities.GlobalIndicatorResult>
    {
        public System.Guid IndicatorId { get; set; }

        public override void Import(GovCamaguey.Persistence.Entities.GlobalIndicatorResult entity)
        {
            base.Import(entity);

            IndicatorId = entity.IndicatorId;
        }
    }
}
