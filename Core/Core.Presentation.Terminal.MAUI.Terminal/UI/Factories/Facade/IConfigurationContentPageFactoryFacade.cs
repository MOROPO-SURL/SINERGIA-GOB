namespace MOROPO.SINETGIAGOB.Core.Presentation.Terminal.MAUI.UI.Factories.Facade
{
    public interface IConfigurationContentPageFactoryFacade<TModel, TConfigurationPageModel>
        where TModel : class
        where TConfigurationPageModel : Models.Page.Contract.AbstractConfigurationPageModel<TModel> 
    {
        System.Threading.Tasks.Task<Pages.Base.ConfigurationBaseContentPage<TModel, TModel, TConfigurationPageModel>> BuildAsync(System.Action<TModel> configurationSuccessAction, TModel model);
    }
}
