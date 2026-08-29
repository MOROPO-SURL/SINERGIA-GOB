namespace MOROPO.SINETGIAGOB.GovCamaguey.Terminal.Factories.Detail;

public class ObjectiveDetailPageFactory : Core.Presentation.Terminal.MAUI.UI.Factories.Facade.IDetailContentPageFactoryFacade<Interop.API.Common.Models.Display.ObjectiveDisplayModel>
{
    public Core.Presentation.Terminal.MAUI.UI.Pages.Base.DetailBaseContentPage<Interop.API.Common.Models.Display.ObjectiveDisplayModel> Build(Interop.API.Common.Models.Display.ObjectiveDisplayModel model, System.Action<Interop.API.Common.Models.Display.ObjectiveDisplayModel, int?> delegationAction = null)
    {
        return new UI.Pages.Detail.ObjectiveDetailPage(model, delegationAction);
    }
}
