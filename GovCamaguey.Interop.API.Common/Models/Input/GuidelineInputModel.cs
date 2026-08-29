namespace MOROPO.SINETGIAGOB.GovCamaguey.Interop.API.Common.Models.Input
{
    public class GuidelineInputModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityInputModel<GovCamaguey.Persistence.Entities.Guideline, System.Guid>
    {
        public System.Guid Id { get; set; }

        public int Index { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.MaxLength(1024)]
        public string Name { get; set; }

        [System.ComponentModel.DataAnnotations.MaxLength(5120)]
        public string Description { get; set; }

        public GovCamaguey.Persistence.Entities.Guideline Export()
        {
            var entity = new GovCamaguey.Persistence.Entities.Guideline { Id = Id, Enabled = true };

            Mergue(entity);

            return entity;
        }

        public void Import(GovCamaguey.Persistence.Entities.Guideline entity)
        {
            throw new System.NotImplementedException();
        }

        public void Mergue(GovCamaguey.Persistence.Entities.Guideline entity)
        {
            entity.Index = Index;
            entity.Name = Name;
            entity.Description = Description;
        }
    }
}
