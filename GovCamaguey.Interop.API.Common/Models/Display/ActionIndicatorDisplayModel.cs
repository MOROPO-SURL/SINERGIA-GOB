using System.Linq;

namespace MOROPO.SINETGIAGOB.GovCamaguey.Interop.API.Common.Models.Display
{
    public class ActionIndicatorDisplayModel : Contract.AbstractIndicatorDisplayModel<GovCamaguey.Persistence.Entities.ActionIndicator>
    {
        public ActionIndicatorMontlyPlanDisplayModel[] MontlyPlans { get; set; }

        public override void Import(GovCamaguey.Persistence.Entities.ActionIndicator entity)
        {
            base.Import(entity);

            MontlyPlans = entity.MontlyPlans.OrderBy(_model => _model.Year * 100 + _model.Month).Select(_entity =>
            {
                var model = new ActionIndicatorMontlyPlanDisplayModel();

                model.Import(_entity);

                return model;
            }).ToArray();
        }
    }
}
