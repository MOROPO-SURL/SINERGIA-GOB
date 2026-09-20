namespace MOROPO.SINERGIAGOB.GovCamaguey.Web.BackOffice.Models.View.Display
{
    public class IndicatorDisplayViewModel : Interop.API.Common.Models.Display.ActionIndicatorDisplayModel
    {
        public bool Archived { get; set; }

        public ActionDisplayViewModel Action { get; set; }

        public override void Import(GovCamaguey.Persistence.Entities.ActionIndicator entity)
        {
            base.Import(entity);

            Archived = entity.Archived;

            if (entity.Action != null)
            {
                Action = new ActionDisplayViewModel();

                Action.Import(entity.Action);
            }
        }
    }
}
