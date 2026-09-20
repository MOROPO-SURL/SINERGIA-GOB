using System.Linq;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Terminal.Models.View;

public class ObjectiveSummaryViewModel : Contract.AbstractObjectiveViewModel
{
    public ObjectiveSummaryViewModel(Interop.API.Common.Models.Display.ObjectiveDisplayModel model) : base(model)
    {
        Description = model.Description;
        Responsible = model.Responsible;

        Proyections = model.Proyections.Select(_model => new ProyectionSummaryViewModel(_model, model.Index)).ToArray();
    }

    public string Description { get; set; }

    public Interop.API.Common.Models.Display.PersonDisplayModel Responsible { get; set; }

    public ProyectionSummaryViewModel[] Proyections { get; set; }
}
