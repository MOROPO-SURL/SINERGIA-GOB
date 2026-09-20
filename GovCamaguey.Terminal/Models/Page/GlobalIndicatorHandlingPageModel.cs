using MOROPO.SINERGIAGOB.GovCamaguey.Interop.API.Common.Models.Display;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Terminal.Models.Page;

public class GlobalIndicatorHandlingPageModel : Framework.Caney.MVVM.Core.Models.Page.BasePageModel
{
    public System.Collections.ObjectModel.ObservableCollection<GlobalIndicatorDisplayModel> Indicators { get; set; } = [];
}