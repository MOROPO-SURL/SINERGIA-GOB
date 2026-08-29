namespace MOROPO.SINETGIAGOB.GovCamaguey.Interop.API.Common.Models.Display
{
    public class GuidelineDisplayModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityDisplayModel<GovCamaguey.Persistence.Entities.Guideline, System.Guid>
    {
        public System.Guid Id { get; set; }

        public bool Enabled { get; set; }

        public int Index { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual void Import(GovCamaguey.Persistence.Entities.Guideline entity)
        {
            Id = entity.Id;
            Enabled = entity.Enabled;
            Index = entity.Index;
            Name = entity.Name;
            Description = entity.Description;
        }
    }
}
