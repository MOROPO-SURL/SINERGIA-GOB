namespace MOROPO.SINERGIAGOB.GovCamaguey.Web.BackOffice.Models.View.Display
{
    public class ObjectiveDisplayViewModel : Interop.API.Common.Models.Display.ObjectiveDisplayModel
    {
        public bool Enabled { get; set; }

        public bool Archived { get; set; }

        public override void Import(GovCamaguey.Persistence.Entities.Objective entity)
        {
            base.Import(entity);

            Enabled = entity.Enabled;
            Archived = entity.Archived;
        }
    }
}
