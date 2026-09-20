using MOROPO.Framework.Caney.MVVM.Core.Extensions;

namespace MOROPO.SINERGIAGOB.Core.Presentation.Terminal.MAUI.UI.Pages.Base
{
    public class DetailBaseContentPage<TModel> : Framework.Caney.MVVM.Core.Pages.ContentPage where TModel : class
    {
        public DetailBaseContentPage(TModel bindingContextModel)
        {
            BindingContext = bindingContextModel;
        }

        protected override bool OnBackButtonPressed()
        {
            DismissControl_Invoked(this, System.EventArgs.Empty);

            return true;
        }

        protected void DismissControl_Invoked(object sender, System.EventArgs eventArgs)
        {
            this.NavigationPopModalPageAsync();
        }
    }
}
