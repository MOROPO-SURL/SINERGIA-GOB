using MOROPO.SINETGIAGOB.GovCamaguey.Interop.API.Common.Models.Display;

namespace MOROPO.SINETGIAGOB.GovCamaguey.Terminal.Models.Page;

public class ObjectiveSummaryPageModel : Framework.Caney.MVVM.Core.Models.Page.BasePageModel
{
    private View.ObjectiveSummaryViewModel objective;

    public View.ObjectiveSummaryViewModel Objective { get => objective; set => SetProperty(ref objective, value); }

    public System.Collections.ObjectModel.ObservableCollection<ObjectiveDisplayModel> AvaialbleObjetives { get; set; } = [];
}
