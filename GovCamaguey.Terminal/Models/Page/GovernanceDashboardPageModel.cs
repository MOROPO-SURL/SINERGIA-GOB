namespace MOROPO.SINETGIAGOB.GovCamaguey.Terminal.Models.Page;

public class GovernanceDashboardPageModel : Framework.Caney.MVVM.Core.Models.Page.BasePageModel
{
    private View.ChartViewModel globalObjectivesStatusChart;
    private View.ChartViewModel globalObjectivesProgressChart;
    private View.ChartViewModel monthlyObjectivesProgressChart;
    private int proyectionCount;
    private View.ChartViewModel proyectionDistributionChart;
    private int actionCount;
    private View.ChartViewModel actionDistributionChart;
    private int indicatorCount;
    private View.ChartViewModel indicatorDistributionChart;

    public View.ChartViewModel GlobalObjectivesStatusChart { get => globalObjectivesStatusChart; set => SetProperty(ref globalObjectivesStatusChart, value); }

    public View.ChartViewModel GlobalObjectivesProgressChart { get => globalObjectivesProgressChart; set => SetProperty(ref globalObjectivesProgressChart, value); }

    public View.ChartViewModel MonthlyObjectivesProgressChart { get => monthlyObjectivesProgressChart; set => SetProperty(ref monthlyObjectivesProgressChart, value); }

    public int ProyectionCount { get => proyectionCount; set => SetProperty(ref proyectionCount, value); }

    public View.ChartViewModel ProyectionDistributionChart { get => proyectionDistributionChart; set => SetProperty(ref proyectionDistributionChart, value); }

    public int ActionCount { get => actionCount; set => SetProperty(ref actionCount, value); }

    public View.ChartViewModel ActionDistributionChart { get => actionDistributionChart; set => SetProperty(ref actionDistributionChart, value); }

    public int IndicatorCount { get => indicatorCount; set => SetProperty(ref indicatorCount, value); }

    public View.ChartViewModel IndicatorDistributionChart { get => indicatorDistributionChart; set => SetProperty(ref indicatorDistributionChart, value); }

    public System.Collections.ObjectModel.ObservableCollection<View.ObjectiveOverviewViewModel> ObjectiveOverviews { get; set; } = [];
}
