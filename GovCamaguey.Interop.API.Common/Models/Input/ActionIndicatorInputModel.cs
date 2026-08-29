using System.Collections.Generic;

namespace MOROPO.SINETGIAGOB.GovCamaguey.Interop.API.Common.Models.Input
{
    public class ActionIndicatorInputModel : Contract.AbstractIndicatorInputModel<GovCamaguey.Persistence.Entities.ActionIndicator>
    {
        public List<ActionIndicatorMontlyPlanInputModel> EditionMontlyPlans { get; set; } = new List<ActionIndicatorMontlyPlanInputModel>();
        public List<ActionIndicatorMontlyPlanInputModel> IncomingMontlyPlans { get; set; } = new List<ActionIndicatorMontlyPlanInputModel>();
        public List<Display.ActionIndicatorMontlyPlanDisplayModel> OutgoingMontlyPlans { get; set; } = new List<Display.ActionIndicatorMontlyPlanDisplayModel>();

        public override GovCamaguey.Persistence.Entities.ActionIndicator Export()
        {
            var entity = new GovCamaguey.Persistence.Entities.ActionIndicator { Id = Id, Enabled = true };

            Mergue(entity);

            return entity;
        }
    }
}
