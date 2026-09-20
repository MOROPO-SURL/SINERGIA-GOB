namespace MOROPO.SINERGIAGOB.GovCamaguey.Terminal.Models.View
{
    public class ObjectiveOverviewViewModel
    {
        public int Index { get; set; }

        public string Title { get; set; }

        public int ProyectionCount { get; set; }

        public int ActionCount { get; set; }

        public int IndicatorCount { get; set; }

        public ChartViewModel ProgressChart { get; set; }

        public char ProgressGlyph { get; set; }
    }
}
