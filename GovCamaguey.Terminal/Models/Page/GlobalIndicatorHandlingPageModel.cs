using MOROPO.SINETGIAGOB.GovCamaguey.Interop.API.Common.Models.Display;

namespace MOROPO.SINETGIAGOB.GovCamaguey.Terminal.Models.Page;

public class GlobalIndicatorHandlingPageModel : Framework.Caney.MVVM.Core.Models.Page.BasePageModel
{
    public System.Collections.ObjectModel.ObservableCollection<GlobalIndicatorDisplayModel> Indicators { get; set; } = [];
}