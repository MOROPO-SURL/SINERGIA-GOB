using MOROPO.SINERGIAGOB.GovCamaguey.Web.BackOffice.Models.View.Contract;
using System.Linq;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Web.BackOffice.Models.View;

public class ProyectionSummaryViewModel : AbstractProyectionViewModel
{
    public ProyectionSummaryViewModel(GovCamaguey.Persistence.Entities.Proyection entity, int objectiveIndex) : base(entity, objectiveIndex)
    {
        ExpectedResult = entity.ExpectedResult;
        Description = entity.Description;
        ElementSource = entity.ElementSource;
        MonthlyClosures = [.. entity.MonthlyClosures];
        Guidelines = [.. entity.GuidelineBridges.Where(bridge => !bridge.Stern.Archived).Select(bridge => bridge.Stern)];
        ResponsabilityOrganizations = [.. entity.OrganizationResponsabilityBridges.Where(bridge => !bridge.Stern.Archived).Select(bridge => bridge.Stern).OrderBy(item => item.Priority)];
        ParticipationOrganizations = [.. entity.OrganizationParticipationBridges.Where(bridge => !bridge.Stern.Archived).Select(bridge => bridge.Stern).OrderBy(item => item.Priority)];

        ObjectiveIndex = objectiveIndex;

        Actions = [.. entity.Actions.Where(entity => !entity.Archived).OrderBy(entity => entity.Index)];
    }

    public string ExpectedResult { get; set; }

    public GovCamaguey.Persistence.Entities.ElementSource ElementSource { get; set; }

    public GovCamaguey.Persistence.Entities.ProyectionMonthlyClosure[] MonthlyClosures { get; set; }

    public GovCamaguey.Persistence.Entities.Guideline[] Guidelines { get; set; }

    public GovCamaguey.Persistence.Entities.Organization[] ResponsabilityOrganizations { get; set; }

    public GovCamaguey.Persistence.Entities.Organization[] ParticipationOrganizations { get; set; }

    public GovCamaguey.Persistence.Entities.Action[] Actions { get; set; }
}
