namespace MOROPO.SINERGIAGOB.GovCamaguey.Interop.API.Common.Models.Input
{
    public class GlobalIndicatorResultInputModel : Contract.AbstractIndicatorResultInputModel<GovCamaguey.Persistence.Entities.GlobalIndicatorResult>
    {
        public System.Guid IndicatorId { get; set; }

        public override GovCamaguey.Persistence.Entities.GlobalIndicatorResult Export()
        {
            var entity = new GovCamaguey.Persistence.Entities.GlobalIndicatorResult { Id = Id };

            Mergue(entity);

            return entity;
        }

        public override void Mergue(GovCamaguey.Persistence.Entities.GlobalIndicatorResult entity)
        {
            base.Mergue(entity);

            if (entity.IndicatorId != IndicatorId)
            {
                entity.Indicator = null;
                entity.IndicatorId = IndicatorId;
            }
        }
    }
}
