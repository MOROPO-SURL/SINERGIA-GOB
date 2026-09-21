using MOROPO.SINERGIAGOB.GovCamaguey.Interop.API.Common.Models.Display;
using System.Collections.Generic;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Interop.API.Common.Models.Input
{
    public class ProyectionInputModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityInputModel<Persistence.Entities.Proyection, System.Guid>
    {
        public System.Guid Id { get; set; }

        public int Index { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.MaxLength(1024)]
        public string Name { get; set; }

        [System.ComponentModel.DataAnnotations.MaxLength(5120)]
        public string Description { get; set; }

        [System.ComponentModel.DataAnnotations.MaxLength(3600)]
        public string ExpectedResult { get; set; }

        public System.Guid? ElementSourceId { get; set; }

        public GuidelineDisplayModel[] Guidelines { get; set; }

        public OrganizationDisplayModel[] ResponsabilityOrganizations { get; set; }

        public OrganizationDisplayModel[] ParticipationOrganizations { get; set; }

        public List<ProyectionIndicatorInputModel> EditionIndicators { get; set; } = new List<ProyectionIndicatorInputModel>();
        public List<ProyectionIndicatorInputModel> IncomingIndicators { get; set; } = new List<ProyectionIndicatorInputModel>();
        public List<ProyectionIndicatorDisplayModel> OutgoingIndicators { get; set; } = new List<ProyectionIndicatorDisplayModel>();

        public List<ActionInputModel> EditionActions { get; set; } = new List<ActionInputModel>();
        public List<ActionInputModel> IncomingActions { get; set; } = new List<ActionInputModel>();
        public List<ActionDisplayModel> OutgoingActions { get; set; } = new List<ActionDisplayModel>();

        public List<ProyectionMonthlyClosureInputModel> EditionMonthlyClosures { get; set; } = new List<ProyectionMonthlyClosureInputModel>();
        public List<ProyectionMonthlyClosureInputModel> IncomingMonthlyClosures { get; set; } = new List<ProyectionMonthlyClosureInputModel>();
        public List<ProyectionMonthlyClosureDisplayModel> OutgoingMonthlyClosures { get; set; } = new List<ProyectionMonthlyClosureDisplayModel>();

        public Persistence.Entities.Proyection Export()
        {
            var entity = new Persistence.Entities.Proyection { Id = Id, Enabled = true };

            Mergue(entity);

            return entity;
        }

        public void Import(Persistence.Entities.Proyection entity)
        {
            throw new System.NotImplementedException();
        }

        public void Mergue(Persistence.Entities.Proyection entity)
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

            entity.GuidelineBridges.Clear();

            foreach (var model in Guidelines)
            {
                entity.GuidelineBridges.Add(new Persistence.Entities.ProyectionGuidelineBridge
                {
                    ProwId = entity.Id,
                    SternId = model.Id
                });
            }

            entity.OrganizationResponsabilityBridges.Clear();

            foreach (var model in ResponsabilityOrganizations)
            {
                entity.OrganizationResponsabilityBridges.Add(new Persistence.Entities.ProyectionOrganizationResponsabilityBridge
                {
                    ProwId = entity.Id,
                    SternId = model.Id
                });
            }

            entity.OrganizationParticipationBridges.Clear();

            foreach (var model in ParticipationOrganizations)
            {
                entity.OrganizationParticipationBridges.Add(new Persistence.Entities.ProyectionOrganizationParticipationBridge
                {
                    ProwId = entity.Id,
                    SternId = model.Id
                });
            }
        }
    }
}
