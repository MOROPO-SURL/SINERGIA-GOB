using System.Linq;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Web.BackOffice.Models.View.Contract;

public abstract class AbstractObjectiveViewModel
{
    protected AbstractObjectiveViewModel(GovCamaguey.Persistence.Entities.Objective entity)
    {
        Id = entity.Id;
        Index = entity.Index;
        Name = entity.Name;
        Indicators = [.. entity.Indicators.Where(_entity => !_entity.Archived).OrderBy(_entity => _entity.Priority)];
    }

    public System.Guid Id { get; set; }

    public int Index { get; set; }

    public string Name { get; set; }

    public GovCamaguey.Persistence.Entities.ObjectiveIndicator[] Indicators { get; set; }
}
