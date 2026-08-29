using MOROPO.SINETGIAGOB.GovCamaguey.Interop.API.Common.Models.Display;
using System.Linq;

namespace MOROPO.SINETGIAGOB.GovCamaguey.Terminal.Models.View;

public class ProyectionSummaryViewModel : Contract.AbstractProyectionViewModel
{
    public ProyectionSummaryViewModel(ProyectionDisplayModel model, int objectiveIndex) : base(model, objectiveIndex)
    {
        Description = model.Description;
        Guidelines = model.Guidelines;
        ResponsabilityOrganizations = model.ResponsabilityOrganizations;
        ParticipationOrganizations = model.ParticipationOrganizations;

        ObjectiveIndex = objectiveIndex;

        Actions = model.Actions.Select(_model => new ActionSummaryViewModel(_model, model.Index, objectiveIndex)).ToArray();
    }

    public GuidelineDisplayModel[] Guidelines { get; set; }

    public OrganizationDisplayModel[] ResponsabilityOrganizations { get; set; }

    public OrganizationDisplayModel[] ParticipationOrganizations { get; set; }

    public ActionSummaryViewModel[] Actions { get; set; }
}
