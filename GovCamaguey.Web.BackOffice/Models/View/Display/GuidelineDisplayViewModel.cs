namespace MOROPO.SINERGIAGOB.GovCamaguey.Web.BackOffice.Models.View.Display
{
    public class GuidelineDisplayViewModel : Interop.API.Common.Models.Display.GuidelineDisplayModel
    {
        public bool Archived { get; set; }

        public override void Import(GovCamaguey.Persistence.Entities.Guideline entity)
        {
            base.Import(entity);

            Archived = entity.Archived;
        }
    }
}
