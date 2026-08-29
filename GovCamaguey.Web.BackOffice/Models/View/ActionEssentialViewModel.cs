namespace MOROPO.SINETGIAGOB.GovCamaguey.Web.BackOffice.Models.View;

public class ActionEssentialViewModel
{
    public ActionEssentialViewModel(GovCamaguey.Persistence.Entities.Action entity, int proyectionIndex, int objectiveIndex)
    {
        Id = entity.Id;
        Enabled = entity.Enabled;
        Index = entity.Index;
        Name = entity.Name;
        Indicators = [.. entity.Indicators];

        ProyectionIndex = proyectionIndex;
        ObjectiveIndex = objectiveIndex;
    }

    public System.Guid Id { get; set; }

    public bool Enabled { get; set; }

    public int Index { get; set; }

    public int ProyectionIndex { get; set; }

    public int ObjectiveIndex { get; set; }

    public string Name { get; set; }

    public GovCamaguey.Persistence.Entities.ActionIndicator[] Indicators { get; set; }
}
