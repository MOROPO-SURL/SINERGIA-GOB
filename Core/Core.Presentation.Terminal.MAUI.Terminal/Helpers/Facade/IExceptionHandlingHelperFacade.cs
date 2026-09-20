namespace MOROPO.SINERGIAGOB.Core.Presentation.Terminal.MAUI.Helpers.Facade
{
    public interface IExceptionHandlingHelperFacade
    {
        void HandleException(System.Exception exception, Microsoft.Maui.Controls.Page page, System.Action<System.Exception> callbackAction = null);
    }
}
