namespace MOROPO.SINERGIAGOB.GovCamaguey.Web.BackOffice.Models.View.Display
{
    public class ActionDisplayViewModel : Interop.API.Common.Models.Display.ActionDisplayModel
    {
        public bool Archived { get; set; }

        public ProyectionDisplayViewModel Proyection { get; set; }

        public override void Import(GovCamaguey.Persistence.Entities.Action entity)
        {
            base.Import(entity);

            Archived = entity.Archived;

             if (entity.Proyection != null)
            {
                Proyection = new ProyectionDisplayViewModel();

                Proyection.Import(entity.Proyection);
            }
        }
    }
}
