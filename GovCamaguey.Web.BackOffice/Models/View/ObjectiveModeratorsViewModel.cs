namespace MOROPO.SINETGIAGOB.GovCamaguey.Web.BackOffice.Models.View;

public class ObjectiveModeratorationViewModel
{
    public Display.ObjectiveDisplayViewModel Objective { get; set; }

    public Display.Bridge.ObjectiveUserBridgeDisplayModel[] Moderators { get; set; }
}
