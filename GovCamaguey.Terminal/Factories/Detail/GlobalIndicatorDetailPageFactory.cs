namespace MOROPO.SINERGIAGOB.GovCamaguey.Terminal.Factories.Detail;

public class GlobalIndicatorDetailPageFactory : Core.Presentation.Terminal.MAUI.UI.Factories.Facade.IDetailContentPageFactoryFacade<Interop.API.Common.Models.Display.GlobalIndicatorDisplayModel>
{
    public Core.Presentation.Terminal.MAUI.UI.Pages.Base.DetailBaseContentPage<Interop.API.Common.Models.Display.GlobalIndicatorDisplayModel> Build(Interop.API.Common.Models.Display.GlobalIndicatorDisplayModel model, System.Action<Interop.API.Common.Models.Display.GlobalIndicatorDisplayModel, int?> delegationAction = null)
    {
        return new UI.Pages.Detail.GlobalIndicatorDetailPage(model, delegationAction);
    }
}