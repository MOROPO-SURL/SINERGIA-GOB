namespace MOROPO.SINERGIAGOB.GovCamaguey.Terminal.Factories.Detail;

public class ElementSourceDetailPageFactory : Core.Presentation.Terminal.MAUI.UI.Factories.Facade.IDetailContentPageFactoryFacade<Interop.API.Common.Models.Display.ElementSourceDisplayModel>
{
    public Core.Presentation.Terminal.MAUI.UI.Pages.Base.DetailBaseContentPage<Interop.API.Common.Models.Display.ElementSourceDisplayModel> Build(Interop.API.Common.Models.Display.ElementSourceDisplayModel model, System.Action<Interop.API.Common.Models.Display.ElementSourceDisplayModel, int?> delegationAction = null)
    {
        return new UI.Pages.Detail.ElementSourceDetailPage(model, delegationAction);
    }
}