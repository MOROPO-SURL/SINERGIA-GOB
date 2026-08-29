using System.Collections.Generic;

namespace MOROPO.SINETGIAGOB.GovCamaguey.Interop.API.Common.Models.Input
{
    public class ObjectiveIndicatorInputModel : Contract.AbstractIndicatorInputModel<GovCamaguey.Persistence.Entities.ObjectiveIndicator>
    {
        public List<ObjectiveIndicatorMontlyPlanInputModel> EditionMontlyPlans { get; set; } = new List<ObjectiveIndicatorMontlyPlanInputModel>();
        public List<ObjectiveIndicatorMontlyPlanInputModel> IncomingMontlyPlans { get; set; } = new List<ObjectiveIndicatorMontlyPlanInputModel>();
        public List<Display.ObjectiveIndicatorMontlyPlanDisplayModel> OutgoingMontlyPlans { get; set; } = new List<Display.ObjectiveIndicatorMontlyPlanDisplayModel>();

        public override GovCamaguey.Persistence.Entities.ObjectiveIndicator Export()
        {
            var entity = new GovCamaguey.Persistence.Entities.ObjectiveIndicator { Id = Id, Enabled = true };

            Mergue(entity);

            return entity;
        }
    }
}
