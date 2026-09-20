using System.Linq;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Interop.API.Common.Models.Display
{
    public class ObjectiveDisplayModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityDisplayModel<GovCamaguey.Persistence.Entities.Objective, System.Guid>, Contract.IModerableModel
    {
        public System.Guid Id { get; set; }

        public bool IsModerable { get; set; }

        public int Index { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public PersonDisplayModel Responsible { get; set; }

        public ObjectiveIndicatorDisplayModel[] Indicators { get; set; }

        public ProyectionDisplayModel[] Proyections { get; set; }

        public virtual void Import(GovCamaguey.Persistence.Entities.Objective entity)
        {
            Id = entity.Id;
            Index = entity.Index;
            Name = entity.Name;
            Description = entity.Description;
            
            if (entity.Responsible != null)
            {
                Responsible = new PersonDisplayModel();

                Responsible.Import(entity.Responsible);
            }

            Indicators = entity.Indicators.Where(_entity => !_entity.Archived).OrderBy(_entity => _entity.Priority).Select(_entity =>
            {
                var model = new ObjectiveIndicatorDisplayModel();

                model.Import(_entity);

                return model;
            }).ToArray();

            Proyections = entity.Proyections.Where(_entity => !_entity.Archived).OrderBy(_entity => _entity.Index).Select(_entity =>
            {
                var model = new ProyectionDisplayModel();

                model.Import(_entity);

                return model;
            }).ToArray();
        }

        public override string ToString()
        {
            return $"{Index} {Name}";
        }
    }
}
