using MOROPO.SINERGIAGOB.GovCamaguey.Interop.API.Common.Models.Display;
using System.Linq;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Terminal.Models.View;

public class ProyectionIndicatorHandlingViewModel : Contract.AbstractProyectionViewModel
{
    public ProyectionIndicatorHandlingViewModel(ProyectionDisplayModel model, int objectiveIndex) : base(model, objectiveIndex)
    {
        Guidelines = model.Guidelines;
        ResponsabilityOrganizations = model.ResponsabilityOrganizations;
        ParticipationOrganizations = model.ParticipationOrganizations;
        Indicators = model.Indicators;

        ObjectiveIndex = objectiveIndex;

        Actions = model.Actions.Select(_model => new ActionEssentialViewModel(_model, model.Index, objectiveIndex)).ToArray();
    }

    public GuidelineDisplayModel[] Guidelines { get; set; }

    public OrganizationDisplayModel[] ResponsabilityOrganizations { get; set; }

    public OrganizationDisplayModel[] ParticipationOrganizations { get; set; }

    public ActionEssentialViewModel[] Actions { get; set; }
}
