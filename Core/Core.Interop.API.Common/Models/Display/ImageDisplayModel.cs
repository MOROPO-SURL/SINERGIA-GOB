namespace MOROPO.SINERGIAGOB.Core.Interop.API.Common.Models.Display
{
    public class ImageDisplayModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityDisplayModel<Framework.Guama.Media.Persistence.Entities.Image, System.Guid>
    {
        public System.Guid Id { get; set; }

        public System.DateTime CreateAt { get; set; }

        public string Extension { get; set; }

        public long Length { get; set; }

        public string About { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }

        public ThumbnailDisplayModel Thumbnail { get; set; }

        public void Import(Framework.Guama.Media.Persistence.Entities.Image entity)
        {
            Id = entity.Id;
            CreateAt = entity.CreateAt;
            Length = entity.Length;
            About = entity.About;
            Height = entity.Height;
            Width = entity.Width;

            if (entity.Thumbnail != null)
            {
                Thumbnail = new ThumbnailDisplayModel();

                Thumbnail.Import(entity.Thumbnail);
            }
        }
    }
}
