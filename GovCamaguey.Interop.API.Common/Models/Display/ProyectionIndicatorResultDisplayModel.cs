namespace MOROPO.SINERGIAGOB.GovCamaguey.Interop.API.Common.Models.Display
{
    public class ProyectionIndicatorResultDisplayModel : Contract.AbstractIndicatorResultDisplayModel<GovCamaguey.Persistence.Entities.ProyectionIndicatorResult>
    {
        public System.Guid IndicatorId { get; set; }

        public override void Import(GovCamaguey.Persistence.Entities.ProyectionIndicatorResult entity)
        {
            base.Import(entity);

            IndicatorId = entity.IndicatorId;
        }
    }
}
