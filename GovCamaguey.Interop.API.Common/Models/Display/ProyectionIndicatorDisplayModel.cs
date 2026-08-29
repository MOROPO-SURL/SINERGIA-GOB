using System.Linq;

namespace MOROPO.SINETGIAGOB.GovCamaguey.Interop.API.Common.Models.Display
{
    public class ProyectionIndicatorDisplayModel : Contract.AbstractIndicatorDisplayModel<GovCamaguey.Persistence.Entities.ProyectionIndicator>
    {
        public ProyectionIndicatorMontlyPlanDisplayModel[] MontlyPlans { get; set; }

        public override void Import(GovCamaguey.Persistence.Entities.ProyectionIndicator entity)
        {
            base.Import(entity);

            MontlyPlans = entity.MontlyPlans.OrderBy(_model => _model.Year * 100 + _model.Month).Select(_entity =>
            {
                var model = new ProyectionIndicatorMontlyPlanDisplayModel();

                model.Import(_entity);

                return model;
            }).ToArray();
        }
    }
}
