using System.Linq;

namespace MOROPO.SINETGIAGOB.GovCamaguey.Interop.API.Common.Models.Display
{
    public class ProyectionDisplayModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityDisplayModel<GovCamaguey.Persistence.Entities.Proyection, System.Guid>, Contract.IModerableModel
    {
        public System.Guid Id { get; set; }

        public bool IsModerable { get; set; }

        public bool Enabled { get; set; }

        public int Index { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public GuidelineDisplayModel[] Guidelines { get; set; }

        public OrganizationDisplayModel[] ResponsabilityOrganizations { get; set; }

        public OrganizationDisplayModel[] ParticipationOrganizations { get; set; }

        public ProyectionIndicatorDisplayModel[] Indicators { get; set; }

        public ActionDisplayModel[] Actions { get; set; }

        public virtual void Import(GovCamaguey.Persistence.Entities.Proyection entity)
        {
            Id = entity.Id;
            Enabled = entity.Enabled;
            Index = entity.Index;
            Name = entity.Name;
            Description = entity.Description;

            Guidelines = entity.GuidelineBridges.Where(bridge => !bridge.Stern.Archived).OrderBy(bridge => bridge.Stern.Index).Select(bridge =>
            {
                var model = new GuidelineDisplayModel();

                model.Import(bridge.Stern);

                return model;
            }).ToArray();

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
                var model = new ProyectionIndicatorDisplayModel();

                model.Import(_entity);

                return model;
            }).ToArray();

            Actions = entity.Actions.Where(_entity => !_entity.Archived).OrderBy(_entity => _entity.Index).Select(_entity =>
            {
                var model = new ActionDisplayModel();

                model.Import(_entity);

                return model;
            }).ToArray();
        }
    }
}
