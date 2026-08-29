namespace MOROPO.SINETGIAGOB.GovCamaguey.Web.BackOffice.Models.View.Input.Bridge;

public class ProyectionUserBridgeInputModel
{
    public System.Guid ProyectionId { get; set; }

    [System.ComponentModel.DataAnnotations.Required]
    [System.ComponentModel.DataAnnotations.Display(Name = "Usuario")]
    public string UserId { get; set; }

    public bool IsActionsIncluded { get; set; }

    public Framework.Guama.Security.Personal.Presentation.Web.Backend.Models.View.UserDisplayViewModel[] AvailableUsers { get; set; } = [];
}