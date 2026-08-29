namespace MOROPO.SINETGIAGOB.GovCamaguey.Web.BackOffice.Models.View.Display
{
    public class ProyectionDisplayViewModel : Interop.API.Common.Models.Display.ProyectionDisplayModel
    {
        public bool Archived { get; set; }

        public ObjectiveDisplayViewModel Objective { get; set; }

        public override void Import(GovCamaguey.Persistence.Entities.Proyection entity)
        {
            base.Import(entity);

            Archived = entity.Archived;

            if (entity.Objective != null)
            {
                Objective = new ObjectiveDisplayViewModel();

                Objective.Import(entity.Objective);
            }
        }
    }
}
