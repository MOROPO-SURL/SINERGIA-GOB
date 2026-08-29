namespace MOROPO.SINETGIAGOB.Core.Presentation.Terminal.MAUI.Extensions
{
    public static class ImageInputModelExtensions
    {
        public static Microsoft.Maui.Controls.ImageSource ToImageSource(this Interop.API.Common.Models.Input.ImageInputModel target)
        {
            return Microsoft.Maui.Controls.ImageSource.FromStream(() => new System.IO.MemoryStream(target.ContentData));
        }
    }
}
