namespace MOROPO.SINERGIAGOB.GovCamaguey.Web.BackOffice.Models.View.Display
{
    public class OrganizationDisplayViewModel : Interop.API.Common.Models.Display.OrganizationDisplayModel
    {
        public bool Enabled { get; set; }

        public bool Archived { get; set; }

        public override void Import(GovCamaguey.Persistence.Entities.Organization entity)
        {
            base.Import(entity);

            Enabled = entity.Enabled;
            Archived = entity.Archived;
        }
    }
}
