namespace MOROPO.SINETGIAGOB.GovCamaguey.Web.BackOffice.Models.View.Input.Bridge;

public class ObjectiveUserBridgeInputModel
{
    public System.Guid ObjectiveId { get; set; }

    [System.ComponentModel.DataAnnotations.Required]
    [System.ComponentModel.DataAnnotations.Display(Name = "Usuario")]
    public string UserId { get; set; }

    public bool IsProyectionsIncluded { get; set; }

    public bool IsActionsIncluded { get; set; }

    public Framework.Guama.Security.Personal.Presentation.Web.Backend.Models.View.UserDisplayViewModel[] AvailableUsers { get; set; } = [];
}