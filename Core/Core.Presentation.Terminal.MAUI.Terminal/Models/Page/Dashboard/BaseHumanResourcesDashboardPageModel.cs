namespace MOROPO.SINERGIAGOB.Core.Presentation.Terminal.MAUI.Models.Page.Dashboard;

public class BaseHumanResourcesDashboardPageModel : Contract.AbstractDashboardPageModel
{
    private bool hasAnyWorkPositionKind;
    private bool hasAnyWorkPosition;
    private bool hasAnySalaryStandard;
    private bool hasAnyWorkforce;
    private bool hasAnyStaff;
    private bool hasAnyEmployee;

    public bool HasAnyWorkPositionKind { get => hasAnyWorkPositionKind; set => SetProperty(ref hasAnyWorkPositionKind, value); }

    public bool HasAnyWorkPosition { get => hasAnyWorkPosition; set => SetProperty(ref hasAnyWorkPosition, value); }

    public bool HasAnySalaryStandard { get => hasAnySalaryStandard; set => SetProperty(ref hasAnySalaryStandard, value); }

    public bool HasAnyWorkforce { get => hasAnyWorkforce; set => SetProperty(ref hasAnyWorkforce, value); }

    public bool HasAnyStaff { get => hasAnyStaff; set => SetProperty(ref hasAnyStaff, value); }

    public bool HasAnyOcupation { get => hasAnyEmployee; set => SetProperty(ref hasAnyEmployee, value); }
}
