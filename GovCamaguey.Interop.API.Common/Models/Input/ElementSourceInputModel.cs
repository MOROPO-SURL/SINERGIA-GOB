using MOROPO.SINERGIAGOB.GovCamaguey.Persistence.Entities;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Interop.API.Common.Models.Input
{
    public sealed class ElementSourceInputModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityInputModel<ElementSource, System.Guid>
    {
        public System.Guid Id { get; set; } = System.Guid.NewGuid();

        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }

        public string Alias { get; set; }

        public string Description { get; set; }

        public int Priority { get; set; }

        public ElementSource Export()
        {
            var entity = new ElementSource { Id = Id, Enabled = true, Archived = false };

            Mergue(entity);

            return entity;
        }

        public void Import(ElementSource entity)
        {
            throw new System.InvalidOperationException();
        }

        public void Mergue(ElementSource entity)
        {
            entity.Name = Name;
            entity.Alias = Alias;
            entity.Description = Description;
            entity.Priority = Priority;
        }
    }
}
