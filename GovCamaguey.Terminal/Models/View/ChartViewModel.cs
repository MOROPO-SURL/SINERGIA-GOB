namespace MOROPO.SINERGIAGOB.GovCamaguey.Terminal.Models.View
{
    public class ChartViewModel : Framework.Caney.MVVM.Core.Models.BaseBindableModel
    {
        private LiveChartsCore.ISeries[] series;

        public LiveChartsCore.ISeries[] Series { get => series; set => SetProperty(ref series, value); }

        public LiveChartsCore.Kernel.Sketches.ICartesianAxis[] XAxes { get; set; }

        public LiveChartsCore.Kernel.Sketches.ICartesianAxis[] YAxes { get; set; }
    }
}