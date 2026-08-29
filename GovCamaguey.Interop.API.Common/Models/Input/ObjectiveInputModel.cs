using MOROPO.SINETGIAGOB.GovCamaguey.Interop.API.Common.Models.Display;
using System.Collections.Generic;

namespace MOROPO.SINETGIAGOB.GovCamaguey.Interop.API.Common.Models.Input
{
    public class ObjectiveInputModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityInputModel<GovCamaguey.Persistence.Entities.Objective, System.Guid>
    {
        public System.Guid Id { get; set; }

        public int Index { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.MaxLength(1024)]
        public string Name { get; set; }

        [System.ComponentModel.DataAnnotations.MaxLength(5120)]
        public string Description { get; set; }

        public System.Guid ResponsibleId { get; set; }

        public List<ObjectiveIndicatorInputModel> EditionIndicators { get; set; } = new List<ObjectiveIndicatorInputModel>();
        public List<ObjectiveIndicatorInputModel> IncomingIndicators { get; set; } = new List<ObjectiveIndicatorInputModel>();
        public List<ObjectiveIndicatorDisplayModel> OutgoingIndicators { get; set; } = new List<ObjectiveIndicatorDisplayModel>();

        public List<ProyectionInputModel> EditionProyections { get; set; } = new List<ProyectionInputModel>();
        public List<ProyectionInputModel> IncomingProyections { get; set; } = new List<ProyectionInputModel>();
        public List<ProyectionDisplayModel> OutgoingProyections { get; set; } = new List<ProyectionDisplayModel>();

        public GovCamaguey.Persistence.Entities.Objective Export()
        {
            var entity = new GovCamaguey.Persistence.Entities.Objective { Id = Id, Enabled = true };

            Mergue(entity);

            return entity;
        }

        public void Import(GovCamaguey.Persistence.Entities.Objective entity)
        {
            throw new System.NotImplementedException();
        }

        public void Mergue(GovCamaguey.Persistence.Entities.Objective entity)
        {
            entity.Index = Index;
            entity.Name = Name;
            entity.Description = Description;

            if (entity.ResponsibleId != ResponsibleId)
            {
                entity.Responsible = null;
                entity.ResponsibleId = ResponsibleId;
            }
        }
    }
}
