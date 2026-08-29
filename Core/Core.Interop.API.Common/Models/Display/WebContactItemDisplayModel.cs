namespace MOROPO.SINETGIAGOB.Core.Interop.API.Common.Models.Display
{
    public class WebContactItemDisplayModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityDisplayModel<Framework.Guama.Identity.Persistence.Entities.WebSiteContactItem, System.Guid>
    {
        public System.Guid Id { get; set; }

        public string Label { get; set; }

        public string Url { get; set; }

        public void Import(Framework.Guama.Identity.Persistence.Entities.WebSiteContactItem entity)
        {
            Id = entity.Id;
            Label = entity.Label;
            Url = entity.Url;
        }
    }
}
