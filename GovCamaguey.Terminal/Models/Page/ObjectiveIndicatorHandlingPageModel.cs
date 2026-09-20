using MOROPO.SINERGIAGOB.GovCamaguey.Interop.API.Common.Models.Display;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Terminal.Models.Page;

public class ObjectiveIndicatorHandlingPageModel : Framework.Caney.MVVM.Core.Models.Page.BasePageModel
{
    private View.ObjectiveIndicatorHandlingViewModel objective;

    public View.ObjectiveIndicatorHandlingViewModel Objective { get => objective; set => SetProperty(ref objective, value); }

    public System.Collections.ObjectModel.ObservableCollection<ObjectiveDisplayModel> AvaialbleObjetives { get; set; } = [];
}