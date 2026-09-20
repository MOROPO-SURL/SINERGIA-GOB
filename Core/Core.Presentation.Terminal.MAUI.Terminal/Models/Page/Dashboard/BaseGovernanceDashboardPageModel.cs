namespace MOROPO.SINERGIAGOB.Core.Presentation.Terminal.MAUI.Models.Page.Dashboard;

public class BaseGovernanceDashboardPageModel : Contract.AbstractDashboardPageModel
{
    private bool hasAnyGroup;
    private bool hasAnyMeeting;
    private bool hasAnyAgreement;

    public bool HasAnyGroup { get => hasAnyGroup; set => SetProperty(ref hasAnyGroup, value); }

    public bool HasAnyMeeting { get => hasAnyMeeting; set => SetProperty(ref hasAnyMeeting, value); }

    public bool HasAnyAgreement { get => hasAnyAgreement; set => SetProperty(ref hasAnyAgreement, value); }
}