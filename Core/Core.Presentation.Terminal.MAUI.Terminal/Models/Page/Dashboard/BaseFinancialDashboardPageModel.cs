namespace MOROPO.SINETGIAGOB.Core.Presentation.Terminal.MAUI.Models.Page.Dashboard;

public class BaseFinancialDashboardPageModel : Contract.AbstractDashboardPageModel
{
    private bool hasAnyBankAgency;
    private bool hasAnyBankBrench;
    private bool hasAnyCurrency;

    public bool HasAnyBankAgency { get => hasAnyBankAgency; set => SetProperty(ref hasAnyBankAgency, value); }

    public bool HasAnyBankBrench { get => hasAnyBankBrench; set => SetProperty(ref hasAnyBankBrench, value); }

    public bool HasAnyCurrency { get => hasAnyCurrency; set => SetProperty(ref hasAnyCurrency, value); }
}
