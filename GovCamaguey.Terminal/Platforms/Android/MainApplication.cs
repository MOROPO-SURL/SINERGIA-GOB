using Android.App;
using Android.Runtime;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Terminal.Droid
{
    [Application]
    public class MainApplication : Microsoft.Maui.MauiApplication
    {
        public MainApplication(nint handle, JniHandleOwnership ownership)
            : base(handle, ownership)
        {
        }

        protected override Microsoft.Maui.Hosting.MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
    }
}
