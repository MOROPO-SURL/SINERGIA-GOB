using MOROPO.SINERGIAGOB.GovCamaguey.Interop.API.Common.Models.Display;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Terminal.Models.View;

public class ActionEssentialViewModel
{
    public ActionEssentialViewModel(ActionDisplayModel model, int proyectionIndex, int objectiveIndex)
    {
        Id = model.Id;
        Enabled = model.Enabled;
        Index = model.Index;
        Name = model.Name;
        Indicators = model.Indicators;

        ProyectionIndex = proyectionIndex;
        ObjectiveIndex = objectiveIndex;
    }

    public System.Guid Id { get; set; }

    public bool Enabled { get; set; }

    public int Index { get; set; }

    public int ProyectionIndex { get; set; }

    public int ObjectiveIndex { get; set; }

    public string Name { get; set; }

    public ActionIndicatorDisplayModel[] Indicators { get; set; }
}
