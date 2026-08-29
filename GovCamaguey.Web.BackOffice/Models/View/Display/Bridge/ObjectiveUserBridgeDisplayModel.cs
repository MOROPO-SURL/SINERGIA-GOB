namespace MOROPO.SINETGIAGOB.GovCamaguey.Web.BackOffice.Models.View.Display.Bridge;

public class ObjectiveUserBridgeDisplayModel
{
    public Framework.Guama.Security.Personal.Presentation.Web.Backend.Models.View.UserDisplayViewModel User { get; set; }

    public bool IsProyectionsIncluded { get; set; }

    public bool IsActionsIncluded { get; set; }
}