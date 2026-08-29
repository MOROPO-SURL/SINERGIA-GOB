namespace MOROPO.SINETGIAGOB.Core.Interop.API.Common.Models.Display
{
    public class DocumentDisplayModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityDisplayModel<Framework.Guama.Media.Persistence.Entities.Document, System.Guid>
    {
        public System.Guid Id { get; set; }

        public string Name { get; set; }

        public string Extension { get; set; }

        public long Length { get; set; }

        public string Description { get; set; }

        public void Import(Framework.Guama.Media.Persistence.Entities.Document entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Length = entity.Length;
            Description = entity.Description;
        }
    }
}
