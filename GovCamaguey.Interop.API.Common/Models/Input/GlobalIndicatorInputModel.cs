using System.Collections.Generic;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Interop.API.Common.Models.Input
{
    public class GlobalIndicatorInputModel : Contract.AbstractIndicatorInputModel<GovCamaguey.Persistence.Entities.GlobalIndicator>
    {
        public List<GlobalIndicatorMontlyPlanInputModel> EditionMontlyPlans { get; set; } = new List<GlobalIndicatorMontlyPlanInputModel>();
        public List<GlobalIndicatorMontlyPlanInputModel> IncomingMontlyPlans { get; set; } = new List<GlobalIndicatorMontlyPlanInputModel>();
        public List<Display.GlobalIndicatorMontlyPlanDisplayModel> OutgoingMontlyPlans { get; set; } = new List<Display.GlobalIndicatorMontlyPlanDisplayModel>();

        public override GovCamaguey.Persistence.Entities.GlobalIndicator Export()
        {
            var entity = new GovCamaguey.Persistence.Entities.GlobalIndicator { Id = Id, Enabled = true };

            Mergue(entity);

            return entity;
        }
    }
}
