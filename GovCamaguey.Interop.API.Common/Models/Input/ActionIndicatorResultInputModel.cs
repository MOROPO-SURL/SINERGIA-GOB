namespace MOROPO.SINETGIAGOB.GovCamaguey.Interop.API.Common.Models.Input
{
    public class ActionIndicatorResultInputModel : Contract.AbstractIndicatorResultInputModel<GovCamaguey.Persistence.Entities.ActionIndicatorResult>
    {
        public System.Guid IndicatorId { get; set; }

        public override GovCamaguey.Persistence.Entities.ActionIndicatorResult Export()
        {
            var entity = new GovCamaguey.Persistence.Entities.ActionIndicatorResult { Id = Id };

            Mergue(entity);

            return entity;
        }

        public override void Mergue(GovCamaguey.Persistence.Entities.ActionIndicatorResult entity)
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
