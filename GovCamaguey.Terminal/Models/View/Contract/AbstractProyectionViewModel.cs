using MOROPO.SINERGIAGOB.GovCamaguey.Interop.API.Common.Models.Display;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Terminal.Models.View.Contract;

public abstract class AbstractProyectionViewModel
{
    public AbstractProyectionViewModel(ProyectionDisplayModel model, int objectiveIndex)
    {
        Id = model.Id;
        Enabled = model.Enabled;
        Index = model.Index;
        Name = model.Name;
        Description = model.Description;
        Indicators = model.Indicators;

        ObjectiveIndex = objectiveIndex;
    }

    public System.Guid Id { get; set; }

    public bool Enabled { get; set; }

    public int Index { get; set; }

    public int ObjectiveIndex { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public ProyectionIndicatorDisplayModel[] Indicators { get; set; }
}