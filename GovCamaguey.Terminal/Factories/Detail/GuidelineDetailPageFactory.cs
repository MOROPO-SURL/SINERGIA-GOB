namespace MOROPO.SINERGIAGOB.GovCamaguey.Terminal.Factories.Detail
{
    public class GuidelineDetailPageFactory : Core.Presentation.Terminal.MAUI.UI.Factories.Facade.IDetailContentPageFactoryFacade<Interop.API.Common.Models.Display.GuidelineDisplayModel>
    {
        public Core.Presentation.Terminal.MAUI.UI.Pages.Base.DetailBaseContentPage<Interop.API.Common.Models.Display.GuidelineDisplayModel> Build(Interop.API.Common.Models.Display.GuidelineDisplayModel model, System.Action<Interop.API.Common.Models.Display.GuidelineDisplayModel, int?> delegationAction = null)
        {
            return new UI.Pages.Detail.GuidelineDetailPage(model, delegationAction);
        }
    }
}
