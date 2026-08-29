using System.Linq;

namespace MOROPO.SINETGIAGOB.GovCamaguey.Web.BackOffice.Models.View;

public class ActionSummaryViewModel : ActionEssentialViewModel
{
    public ActionSummaryViewModel(GovCamaguey.Persistence.Entities.Action entity, int proyectionIndex, int objectiveIndex) : base(entity, proyectionIndex, objectiveIndex)
    {
        ResponsabilityOrganizations = [.. entity.OrganizationResponsabilityBridges.Select(bridge => bridge.Stern)];
        ParticipationOrganizations = [.. entity.OrganizationParticipationBridges.Select(bridge => bridge.Stern)];
        MonthlyClosures = [.. entity.MonthlyClosures];
    }

    public string Description { get; set; }

    public GovCamaguey.Persistence.Entities.Organization[] ResponsabilityOrganizations { get; set; }

    public GovCamaguey.Persistence.Entities.Organization[] ParticipationOrganizations { get; set; }

    public GovCamaguey.Persistence.Entities.ActionMonthlyClosure[] MonthlyClosures { get; set; }
}
