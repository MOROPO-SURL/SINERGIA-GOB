using Microsoft.Extensions.DependencyInjection;
using MOROPO.Framework.Caney.MVVM.Core.Extensions;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Terminal.UI.Shells
{
    public partial class AppShell : Microsoft.Maui.Controls.Shell
    {
        public AppShell()
        {
            InitializeComponent();

            BindingContext = new Core.Presentation.Terminal.MAUI.Models.Page.MainPageModel
            {
                OwnerInformation = Providers.SingleRuntimeContextProvider.OWNER_INFORMATION
            };
        }

        private Core.Presentation.Terminal.MAUI.Models.Page.MainPageModel ConcretBindingContext => BindingContext as Core.Presentation.Terminal.MAUI.Models.Page.MainPageModel;

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var sessionManager = Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Core.Presentation.Terminal.MAUI.Managers.Facade.ISessionManagerFacade>();

            ConcretBindingContext.User = sessionManager.CurrentSession?.User;

            if (ConcretBindingContext.User == null)
            {
                this.NavigationPushModalPageAsync(new Pages.UserAuthenticationPage());
            }
        }

        private void LogOutButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            var sessionManager = Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Core.Presentation.Terminal.MAUI.Managers.Facade.ISessionManagerFacade>();

            if (sessionManager.CurrentSession?.User == null) return;

            DisplayAlert("Confirmación", "Está seguro que desea terminar su sesión?", "Confirmar", "Cancelar").ContinueWith(async task =>
            {
                if (!task.Result) return;

                await sessionManager.SignOutAsync();

                Dispatcher.Dispatch(() => this.NavigationPushModalPageAsync(new Pages.UserAuthenticationPage()));
            });
        }
    }
}
