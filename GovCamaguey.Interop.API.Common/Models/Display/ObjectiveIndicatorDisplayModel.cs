using System.Linq;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Interop.API.Common.Models.Display
{
    public class ObjectiveIndicatorDisplayModel : Contract.AbstractIndicatorDisplayModel<GovCamaguey.Persistence.Entities.ObjectiveIndicator>
    {
        public ObjectiveIndicatorMontlyPlanDisplayModel[] MontlyPlans { get; set; }

        public override void Import(GovCamaguey.Persistence.Entities.ObjectiveIndicator entity)
        {
            base.Import(entity);

            MontlyPlans = entity.MontlyPlans.OrderBy(_model => _model.Year * 100 + _model.Month).Select(_entity =>
            {
                var model = new ObjectiveIndicatorMontlyPlanDisplayModel();

                model.Import(_entity);

                return model;
            }).ToArray();
        }
    }
}
