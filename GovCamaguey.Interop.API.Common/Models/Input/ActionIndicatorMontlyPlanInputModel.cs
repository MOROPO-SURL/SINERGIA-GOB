namespace MOROPO.SINETGIAGOB.GovCamaguey.Interop.API.Common.Models.Input
{
    public class ActionIndicatorMontlyPlanInputModel : Contract.AbstractIndicatorMontlyPlanInputModel<GovCamaguey.Persistence.Entities.ActionIndicatorMontlyPlan>
    {
        public override GovCamaguey.Persistence.Entities.ActionIndicatorMontlyPlan Export()
        {
            var entity = new GovCamaguey.Persistence.Entities.ActionIndicatorMontlyPlan { Id = Id };

            Mergue(entity);

            return entity;
        }
    }
}