using LiveChartsCore.Drawing;
using LiveChartsCore.Kernel;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Terminal;

public partial class App : Microsoft.Maui.Controls.Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new UI.Shells.AppShell();
    }

    protected override void OnStart()
    {
        base.OnStart();

        Windows[0].Title = $"SINERGIA GOB Camagüey v{Microsoft.Maui.ApplicationModel.AppInfo.Current.VersionString}";
    }
}