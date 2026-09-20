namespace MOROPO.SINERGIAGOB.Core.Presentation.Terminal.MAUI.Managers.Facade
{
    public interface ISettingManagerFacade
    {
        bool UseProxyServer { get; set; }

        string ProxyServerAddress { get; set; }

        bool UseProxyAuthentication { get; set; }

        string ProxyUsername { get; set; }

        string ProxyPassword { get; set; }
    }
}
