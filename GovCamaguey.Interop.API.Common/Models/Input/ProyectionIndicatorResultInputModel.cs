namespace MOROPO.SINETGIAGOB.GovCamaguey.Interop.API.Common.Models.Input
{
    public class ProyectionIndicatorResultInputModel : Contract.AbstractIndicatorResultInputModel<GovCamaguey.Persistence.Entities.ProyectionIndicatorResult>
    {
        public System.Guid IndicatorId { get; set; }

        public override GovCamaguey.Persistence.Entities.ProyectionIndicatorResult Export()
        {
            var entity = new GovCamaguey.Persistence.Entities.ProyectionIndicatorResult { Id = Id };

            Mergue(entity);

            return entity;
        }

        public override void Mergue(GovCamaguey.Persistence.Entities.ProyectionIndicatorResult entity)
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
