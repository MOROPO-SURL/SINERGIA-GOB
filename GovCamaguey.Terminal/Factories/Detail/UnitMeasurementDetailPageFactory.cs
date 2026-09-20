namespace MOROPO.SINERGIAGOB.GovCamaguey.Terminal.Factories.Detail
{
    public class UnitMeasurementDetailPageFactory : Core.Presentation.Terminal.MAUI.UI.Factories.Facade.IDetailContentPageFactoryFacade<Interop.API.Common.Models.Display.UnitMeasurementDisplayModel>
    {
        public Core.Presentation.Terminal.MAUI.UI.Pages.Base.DetailBaseContentPage<Interop.API.Common.Models.Display.UnitMeasurementDisplayModel> Build(Interop.API.Common.Models.Display.UnitMeasurementDisplayModel model, System.Action<Interop.API.Common.Models.Display.UnitMeasurementDisplayModel, int?> delegationAction = null)
        {
            return new UI.Pages.Detail.UnitMeasurementDetailPage(model, delegationAction);
        }
    }
}
