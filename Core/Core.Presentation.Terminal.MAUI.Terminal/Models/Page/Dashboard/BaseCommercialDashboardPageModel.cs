namespace MOROPO.SINETGIAGOB.Core.Presentation.Terminal.MAUI.Models.Page.Dashboard;

public class BaseCommercialDashboardPageModel : Contract.AbstractDashboardPageModel
{
    private bool hasAnyLegalBusinessKind;
    private bool hasAnyNaturalBusinessKind;
    private bool hasAnyPaymentTerm;

    public bool HasAnyLegalBusinessKind { get => hasAnyLegalBusinessKind; set => SetProperty(ref hasAnyLegalBusinessKind, value); }

    public bool HasAnyNaturalBusinessKind { get => hasAnyNaturalBusinessKind; set => SetProperty(ref hasAnyNaturalBusinessKind, value); }

    public bool HasAnyPaymentTerm { get => hasAnyPaymentTerm; set => SetProperty(ref hasAnyPaymentTerm, value); }
}
