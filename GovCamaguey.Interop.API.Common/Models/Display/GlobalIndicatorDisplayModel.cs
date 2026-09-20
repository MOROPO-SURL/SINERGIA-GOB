using System.Linq;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Interop.API.Common.Models.Display
{
    public class GlobalIndicatorDisplayModel : Contract.AbstractIndicatorDisplayModel<GovCamaguey.Persistence.Entities.GlobalIndicator>
    {
        public GlobalIndicatorMontlyPlanDisplayModel[] MontlyPlans { get; set; }

        public override void Import(GovCamaguey.Persistence.Entities.GlobalIndicator entity)
        {
            base.Import(entity);

            MontlyPlans = entity.MontlyPlans.OrderBy(_model => _model.Year * 100 + _model.Month).Select(_entity =>
            {
                var model = new GlobalIndicatorMontlyPlanDisplayModel();

                model.Import(_entity);

                return model;
            }).ToArray();
        }
    }
}
