namespace MOROPO.SINERGIAGOB.GovCamaguey.Terminal.Models.View.Contract;

public abstract class AbstractObjectiveViewModel
{
    protected AbstractObjectiveViewModel(Interop.API.Common.Models.Display.ObjectiveDisplayModel model)
    {
        Id = model.Id;
        Index = model.Index;
        Name = model.Name;
        Indicators = model.Indicators;
    }

    public System.Guid Id { get; set; }

    public int Index { get; set; }

    public string Name { get; set; }

    public Interop.API.Common.Models.Display.ObjectiveIndicatorDisplayModel[] Indicators { get; set; }
}
