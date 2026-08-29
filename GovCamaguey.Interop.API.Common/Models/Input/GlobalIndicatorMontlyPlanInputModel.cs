namespace MOROPO.SINETGIAGOB.GovCamaguey.Interop.API.Common.Models.Input
{
    public class GlobalIndicatorMontlyPlanInputModel : Contract.AbstractIndicatorMontlyPlanInputModel<GovCamaguey.Persistence.Entities.GlobalIndicatorMontlyPlan>
    {
        public override GovCamaguey.Persistence.Entities.GlobalIndicatorMontlyPlan Export()
        {
            var entity = new GovCamaguey.Persistence.Entities.GlobalIndicatorMontlyPlan { Id = Id };

            Mergue(entity);

            return entity;
        }
    }
}
