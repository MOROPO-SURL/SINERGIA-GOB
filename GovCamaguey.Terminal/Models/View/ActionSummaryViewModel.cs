using MOROPO.SINETGIAGOB.GovCamaguey.Interop.API.Common.Models.Display;

namespace MOROPO.SINETGIAGOB.GovCamaguey.Terminal.Models.View;

public class ActionSummaryViewModel : ActionEssentialViewModel
{
    public ActionSummaryViewModel(ActionDisplayModel model, int proyectionIndex, int objectiveIndex) : base(model, proyectionIndex, objectiveIndex)
    {
        ResponsabilityOrganizations = model.ResponsabilityOrganizations;
        ParticipationOrganizations = model.ParticipationOrganizations;
        MonthlyClosures = model.MonthlyClosures;
    }

    public string Description { get; set; }

    public OrganizationDisplayModel[] ResponsabilityOrganizations { get; set; }

    public OrganizationDisplayModel[] ParticipationOrganizations { get; set; }

    public ActionMonthlyClosureDisplayModel[] MonthlyClosures { get; set; }
}
