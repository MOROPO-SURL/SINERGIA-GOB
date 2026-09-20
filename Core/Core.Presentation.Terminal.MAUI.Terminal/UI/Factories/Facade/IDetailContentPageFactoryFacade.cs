namespace MOROPO.SINERGIAGOB.Core.Presentation.Terminal.MAUI.UI.Factories.Facade
{
    public interface IDetailContentPageFactoryFacade<TModel> where TModel : class 
    {
        Pages.Base.DetailBaseContentPage<TModel> Build(TModel model, System.Action<TModel,int?> delegationAction = null);
    }
}