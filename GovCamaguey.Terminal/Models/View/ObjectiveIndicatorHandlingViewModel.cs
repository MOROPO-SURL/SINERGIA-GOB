using System.Linq;

namespace MOROPO.SINETGIAGOB.GovCamaguey.Terminal.Models.View;

public class ObjectiveIndicatorHandlingViewModel : Contract.AbstractObjectiveViewModel
{
    public ObjectiveIndicatorHandlingViewModel(Interop.API.Common.Models.Display.ObjectiveDisplayModel model) : base(model)
    {
        Proyections = model.Proyections.Select(_model => new ProyectionIndicatorHandlingViewModel(_model, model.Index)).ToArray();
    }

    public ProyectionIndicatorHandlingViewModel[] Proyections { get; set; }
}