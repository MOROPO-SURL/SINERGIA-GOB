using System.Linq;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Web.BackOffice.Models.View.Contract;

public abstract class AbstractProyectionViewModel
{
    public AbstractProyectionViewModel(GovCamaguey.Persistence.Entities.Proyection model, int objectiveIndex)
    {
        Id = model.Id;
        Enabled = model.Enabled;
        Index = model.Index;
        Name = model.Name;
        Description = model.Description;
        Indicators = [.. model.Indicators.Where(_entity => !_entity.Archived).OrderBy(_entity => _entity.Priority)];

        ObjectiveIndex = objectiveIndex;
    }

    public System.Guid Id { get; set; }

    public bool Enabled { get; set; }

    public int Index { get; set; }

    public int ObjectiveIndex { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public GovCamaguey.Persistence.Entities.ProyectionIndicator[] Indicators { get; set; }
}