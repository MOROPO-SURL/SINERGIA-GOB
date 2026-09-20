namespace MOROPO.SINERGIAGOB.Core.Presentation.Terminal.MAUI.Models.View
{
    public class ModuleNavigationItemViewModel
    {
        public required string Glyph { get; set; }

        public required string Title { get; set; }

        public required System.Func<Microsoft.Maui.Controls.Page> PagePresentationFunc { get; set; }
    }
}
