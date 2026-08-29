namespace MOROPO.SINETGIAGOB.GovCamaguey.Terminal.Factories.Detail
{
    public class PersonDetailPageFactory : Core.Presentation.Terminal.MAUI.UI.Factories.Facade.IDetailContentPageFactoryFacade<Interop.API.Common.Models.Display.PersonDisplayModel>
    {
        public Core.Presentation.Terminal.MAUI.UI.Pages.Base.DetailBaseContentPage<Interop.API.Common.Models.Display.PersonDisplayModel> Build(Interop.API.Common.Models.Display.PersonDisplayModel model, System.Action<Interop.API.Common.Models.Display.PersonDisplayModel, int?> delegationAction = null)
        {
            return new UI.Pages.Detail.PersonDetailPage(model, delegationAction);
        }
    }
}