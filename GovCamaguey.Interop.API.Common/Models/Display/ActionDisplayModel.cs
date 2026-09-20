using System.Linq;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Interop.API.Common.Models.Display
{
    public class ActionDisplayModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityDisplayModel<GovCamaguey.Persistence.Entities.Action, System.Guid>, Contract.IModerableModel
    {
        public System.Guid Id { get; set; }

        public bool IsModerable { get; set; }

        public bool Enabled { get; set; }

        public int Index { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public OrganizationDisplayModel[] ResponsabilityOrganizations { get; set; }

        public OrganizationDisplayModel[] ParticipationOrganizations { get; set; }

        public ActionIndicatorDisplayModel[] Indicators { get; set; }

        public ActionMonthlyClosureDisplayModel[] MonthlyClosures { get; set; }

        public virtual void Import(GovCamaguey.Persistence.Entities.Action entity)
        {
            Id = entity.Id;
            Enabled = entity.Enabled;
            Index = entity.Index;
            Name = entity.Name;
            Description = entity.Description;

            ResponsabilityOrganizations = entity.OrganizationResponsabilityBridges.Where(bridge => !bridge.Stern.Archived).OrderBy(bridge => bridge.Stern.Priority).Select(bridge =>
            {
                var model = new OrganizationDisplayModel();

                model.Import(bridge.Stern);

                return model;
            }).ToArray();

            ParticipationOrganizations = entity.OrganizationParticipationBridges.Where(bridge => !bridge.Stern.Archived).OrderBy(bridge => bridge.Stern.Priority).Select(bridge =>
            {
                var model = new OrganizationDisplayModel();

                model.Import(bridge.Stern);

                return model;
            }).ToArray();

            Indicators = entity.Indicators.Where(_entity => !_entity.Archived).OrderBy(_entity => _entity.Priority).Select(_entity =>
            {
                var model = new ActionIndicatorDisplayModel();

                model.Import(_entity);

                return model;
            }).ToArray();

            MonthlyClosures = entity.MonthlyClosures?.OrderBy(_entity => _entity.At).Select(_entity =>
            {
                var model = new ActionMonthlyClosureDisplayModel();

                model.Import(_entity);

                return model;
            }).ToArray();
        }
    }
}
