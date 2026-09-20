namespace MOROPO.SINERGIAGOB.GovCamaguey.Web.BackOffice.Models.View.Input.Bridge;

public class ActionUserBridgeInputModel
{
    public System.Guid ActionId { get; set; }

    [System.ComponentModel.DataAnnotations.Required]
    [System.ComponentModel.DataAnnotations.Display(Name = "Usuario")]
    public string UserId { get; set; }

    public Framework.Guama.Security.Personal.Presentation.Web.Backend.Models.View.UserDisplayViewModel[] AvailableUsers { get; set; } = [];
}