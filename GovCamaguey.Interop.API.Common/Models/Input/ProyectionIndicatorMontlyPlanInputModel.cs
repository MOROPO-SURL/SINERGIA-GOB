namespace MOROPO.SINERGIAGOB.GovCamaguey.Interop.API.Common.Models.Input
{
    public class ProyectionIndicatorMontlyPlanInputModel : Contract.AbstractIndicatorMontlyPlanInputModel<GovCamaguey.Persistence.Entities.ProyectionIndicatorMontlyPlan>
    {
        public override GovCamaguey.Persistence.Entities.ProyectionIndicatorMontlyPlan Export()
        {
            var entity = new GovCamaguey.Persistence.Entities.ProyectionIndicatorMontlyPlan { Id = Id };

            Mergue(entity);

            return entity;
        }
    }
}