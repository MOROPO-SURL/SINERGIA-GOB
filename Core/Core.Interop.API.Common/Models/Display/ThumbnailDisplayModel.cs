namespace MOROPO.SINERGIAGOB.Core.Interop.API.Common.Models.Display
{
    public class ThumbnailDisplayModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityDisplayModel<Framework.Guama.Media.Persistence.Entities.Thumbnail, System.Guid>
    {
        public System.Guid Id { get; set; }

        public string ContentType { get; set; }

        public long Length { get; set; }

        public string About { get; set; }

        public void Import(Framework.Guama.Media.Persistence.Entities.Thumbnail entity)
        {
            Id = entity.Id;
            ContentType = entity.ContentType;
            Length = entity.Length;
            About = entity.About;
        }
    }
}
