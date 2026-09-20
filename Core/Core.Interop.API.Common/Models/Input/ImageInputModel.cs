namespace MOROPO.SINERGIAGOB.Core.Interop.API.Common.Models.Input
{
    public class ImageInputModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityInputModel<Framework.Guama.Media.Persistence.Entities.Image, System.Guid>
    {
        public System.Guid Id { get; set; }

        public byte[] ContentData { get; set; }

        public string ContentType { get; set; }

        public string About { get; set; }

        public Framework.Guama.Media.Persistence.Entities.Image Export()
        {
            throw new System.NotImplementedException();
        }

        public void Import(Framework.Guama.Media.Persistence.Entities.Image entity)
        {
            About = entity.About;
        }

        public void Mergue(Framework.Guama.Media.Persistence.Entities.Image entity)
        {
            entity.About = About;
        }
    }
}
