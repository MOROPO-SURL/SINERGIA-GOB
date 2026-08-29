namespace MOROPO.SINETGIAGOB.GovCamaguey.Interop.API.Common.Models.Input
{
    public class ObjectiveIndicatorResultInputModel : Contract.AbstractIndicatorResultInputModel<GovCamaguey.Persistence.Entities.ObjectiveIndicatorResult>
    {
        public System.Guid IndicatorId { get; set; }

        public override GovCamaguey.Persistence.Entities.ObjectiveIndicatorResult Export()
        {
            var entity = new GovCamaguey.Persistence.Entities.ObjectiveIndicatorResult { Id = Id };

            Mergue(entity);

            return entity;
        }

        public override void Mergue(GovCamaguey.Persistence.Entities.ObjectiveIndicatorResult entity)
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
