using MOROPO.SINETGIAGOB.GovCamaguey.Web.BackOffice.Models.View.Contract;
using System.Linq;

namespace MOROPO.SINETGIAGOB.GovCamaguey.Web.BackOffice.Models.View;

public class ObjectiveSummaryViewModel : AbstractObjectiveViewModel
{
    public ObjectiveSummaryViewModel(GovCamaguey.Persistence.Entities.Objective entity) : base(entity)
    {
        Description = entity.Description;
        Responsible = entity.Responsible;

        Proyections = entity.Proyections.Where(entity => !entity.Archived).OrderBy(entity => entity.Index).Select(_model => new ProyectionSummaryViewModel(_model, entity.Index)).ToArray();
    }

    public string Description { get; set; }

    public GovCamaguey.Persistence.Entities.Person Responsible { get; set; }

    public ProyectionSummaryViewModel[] Proyections { get; set; }
}
