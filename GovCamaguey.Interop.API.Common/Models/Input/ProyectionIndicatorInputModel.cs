using System.Collections.Generic;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Interop.API.Common.Models.Input
{
    public class ProyectionIndicatorInputModel : Contract.AbstractIndicatorInputModel<GovCamaguey.Persistence.Entities.ProyectionIndicator>
    {
        public List<ProyectionIndicatorMontlyPlanInputModel> EditionMontlyPlans { get; set; } = new List<ProyectionIndicatorMontlyPlanInputModel>();
        public List<ProyectionIndicatorMontlyPlanInputModel> IncomingMontlyPlans { get; set; } = new List<ProyectionIndicatorMontlyPlanInputModel>();
        public List<Display.ProyectionIndicatorMontlyPlanDisplayModel> OutgoingMontlyPlans { get; set; } = new List<Display.ProyectionIndicatorMontlyPlanDisplayModel>();

        public override GovCamaguey.Persistence.Entities.ProyectionIndicator Export()
        {
            var entity = new GovCamaguey.Persistence.Entities.ProyectionIndicator { Id = Id, Enabled = true };

            Mergue(entity);

            return entity;
        }
    }
}
