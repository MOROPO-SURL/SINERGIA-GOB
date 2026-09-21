using MOROPO.SINERGIAGOB.GovCamaguey.Interop.API.Common.Models.Display;
using System.Collections.Generic;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Interop.API.Common.Models.Input
{
    public class ActionInputModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityInputModel<GovCamaguey.Persistence.Entities.Action, System.Guid>
    {
        public System.Guid Id { get; set; }

        public int Index { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.MaxLength(1024)]
        public string Name { get; set; }

        [System.ComponentModel.DataAnnotations.MaxLength(3600)]
        public string ExpectedResult { get; set; }

        public System.Guid? ElementSourceId { get; set; }

        [System.ComponentModel.DataAnnotations.MaxLength(5120)]
        public string Description { get; set; }

        public OrganizationDisplayModel[] ResponsabilityOrganizations { get; set; }

        public OrganizationDisplayModel[] ParticipationOrganizations { get; set; }

        public List<ActionIndicatorInputModel> EditionIndicators { get; set; } = new List<ActionIndicatorInputModel>();
        public List<ActionIndicatorInputModel> IncomingIndicators { get; set; } = new List<ActionIndicatorInputModel>();
        public List<ActionIndicatorDisplayModel> OutgoingIndicators { get; set; } = new List<ActionIndicatorDisplayModel>();

        public List<ActionMonthlyClosureInputModel> EditionMonthlyClosures { get; set; } = new List<ActionMonthlyClosureInputModel>();
        public List<ActionMonthlyClosureInputModel> IncomingMonthlyClosures { get; set; } = new List<ActionMonthlyClosureInputModel>();
        public List<ActionMonthlyClosureDisplayModel> OutgoingMonthlyClosures { get; set; } = new List<ActionMonthlyClosureDisplayModel>();

        public GovCamaguey.Persistence.Entities.Action Export()
        {
            var entity = new GovCamaguey.Persistence.Entities.Action { Id = Id, Enabled = true };

            Mergue(entity);

            return entity;
        }

        public void Import(GovCamaguey.Persistence.Entities.Action entity)
        {
            throw new System.NotImplementedException();
        }

        public void Mergue(GovCamaguey.Persistence.Entities.Action entity)
        {
            entity.Index = Index;
            entity.Name = Name;
            entity.ExpectedResult = ExpectedResult;
            entity.Description = Description;

            if (entity.ElementSourceId != ElementSourceId)
            {
                entity.ElementSource = null;
                entity.ElementSourceId = ElementSourceId;
            }

            entity.OrganizationResponsabilityBridges.Clear();

            foreach (var model in ResponsabilityOrganizations)
            {
                entity.OrganizationResponsabilityBridges.Add(new GovCamaguey.Persistence.Entities.ActionOrganizationResponsabilityBridge
                {
                    ProwId = entity.Id,
                    SternId = model.Id
                });
            }

            entity.OrganizationParticipationBridges.Clear();

            foreach (var model in ParticipationOrganizations)
            {
                entity.OrganizationParticipationBridges.Add(new GovCamaguey.Persistence.Entities.ActionOrganizationParticipationBridge
                {
                    ProwId = entity.Id,
                    SternId = model.Id
                });
            }
        }
    }
}
