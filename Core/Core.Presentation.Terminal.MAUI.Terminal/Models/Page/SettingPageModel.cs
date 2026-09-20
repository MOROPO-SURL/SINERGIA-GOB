using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.SINERGIAGOB.Core.Presentation.Terminal.MAUI.Models.Page
{
    public class SettingPageModel : Framework.Caney.MVVM.Core.Models.Page.BasePageModel
    {
        public static string ProxyServerAddressValidableProperty => nameof(ProxyServerAddress);
        public static string ProxyUsernameValidableProperty => nameof(ProxyUsername);
        public static string ProxyPasswordValidableProperty => nameof(ProxyPassword);

        private bool useProxyServer;
        private string proxyServerAddress;
        private bool useProxyAuthentication;
        private string proxyUsername;
        private string proxyPassword;
        private FluentValidation.Results.ValidationResult validationResult;

        private readonly System.Action validationCallbackAction;

        public SettingPageModel(System.Action applySuccessCallbackAction, System.Action validationCallbackAction)
        {
            this.validationCallbackAction = validationCallbackAction;

            var settingManager = Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Managers.Facade.ISettingManagerFacade>();

            useProxyServer = settingManager.UseProxyServer;
            proxyServerAddress = settingManager.ProxyServerAddress;
            useProxyAuthentication = settingManager.UseProxyAuthentication;
            proxyUsername = settingManager.ProxyUsername;
            proxyPassword = settingManager.ProxyPassword;

            ApplyChangesCommand = new Microsoft.Maui.Controls.Command(
                execute: () =>
                {
                    var validator = new Validators.SettingValidator();

                    ValidationResult = null;
                    ValidationResult = validator.Validate(this);

                    if (!ValidationResult.IsValid) return;

                    settingManager.UseProxyServer = useProxyServer;
                    settingManager.ProxyServerAddress = proxyServerAddress;
                    settingManager.UseProxyAuthentication = useProxyAuthentication;
                    settingManager.ProxyUsername = proxyUsername;
                    settingManager.ProxyPassword = proxyPassword;

                    applySuccessCallbackAction.Invoke();
                }
            );
        }

        public System.Windows.Input.ICommand ApplyChangesCommand { get; private set; }

        public bool UseProxyServer { get => useProxyServer; set => SetProperty(ref useProxyServer, value); }

        public string ProxyServerAddress { get => proxyServerAddress; set => SetProperty(ref proxyServerAddress, value); }

        public bool UseProxyAuthentication { get => useProxyAuthentication; set => SetProperty(ref useProxyAuthentication, value); }

        public string ProxyUsername { get => proxyUsername; set => SetProperty(ref proxyUsername, value); }

        public string ProxyPassword { get => proxyPassword; set => SetProperty(ref proxyPassword, value); }

        public FluentValidation.Results.ValidationResult ValidationResult { get => validationResult; set => SetProperty(ref validationResult, value, validationCallbackAction); }
    }
}