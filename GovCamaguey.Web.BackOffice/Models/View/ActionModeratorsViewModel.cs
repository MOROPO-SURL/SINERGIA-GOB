namespace MOROPO.SINETGIAGOB.GovCamaguey.Web.BackOffice.Models.View;

public class ActionModeratorsViewModel
{
    public Display.ActionDisplayViewModel Action { get; set; }

    public Display.Bridge.ActionUserBridgeDisplayModel[] Moderators { get; set; }
}
