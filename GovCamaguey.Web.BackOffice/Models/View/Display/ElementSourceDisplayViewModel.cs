namespace MOROPO.SINERGIAGOB.GovCamaguey.Web.BackOffice.Models.View.Display;

public class ElementSourceDisplayViewModel : Interop.API.Common.Models.Display.ElementSourceDisplayModel
{
    public bool Archived { get; set; }

    public override void Import(GovCamaguey.Persistence.Entities.ElementSource entity)
    {
        base.Import(entity);

        Archived = entity.Archived;
    }
}
