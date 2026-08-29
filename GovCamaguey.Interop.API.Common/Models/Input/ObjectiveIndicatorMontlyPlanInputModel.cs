namespace MOROPO.SINETGIAGOB.GovCamaguey.Interop.API.Common.Models.Input
{
    public class ObjectiveIndicatorMontlyPlanInputModel : Contract.AbstractIndicatorMontlyPlanInputModel<GovCamaguey.Persistence.Entities.ObjectiveIndicatorMontlyPlan>
    {
        public override GovCamaguey.Persistence.Entities.ObjectiveIndicatorMontlyPlan Export()
        {
            var entity = new GovCamaguey.Persistence.Entities.ObjectiveIndicatorMontlyPlan { Id = Id };

            Mergue(entity);

            return entity;
        }
    }
}