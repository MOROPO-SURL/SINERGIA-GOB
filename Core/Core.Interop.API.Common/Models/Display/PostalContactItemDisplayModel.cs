namespace MOROPO.SINERGIAGOB.Core.Interop.API.Common.Models.Display
{
    public class PostalContactItemDisplayModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityDisplayModel<Framework.Guama.Identity.Persistence.Entities.PostalContactItem, System.Guid>
    {
        public System.Guid Id { get; set; }

        public string Label { get; set; }

        public string Address { get; set; }

        public int? ZipCode { get; set; }

        public decimal Longitude { get; set; }

        public decimal Latitude { get; set; }

        public MunicipalityDisplayModel Municipality { get; set; }

        public void Import(Framework.Guama.Identity.Persistence.Entities.PostalContactItem entity)
        {
            Id = entity.Id;
            Label = entity.Label;
            Address = entity.Address;
            Longitude = entity.Longitude;
            Latitude = entity.Latitude;

            if (entity.ZipCode > 0)
            {
                ZipCode = entity.ZipCode;
            }

            Municipality = new MunicipalityDisplayModel { Id = (entity.Location as Framework.Guama.Location.Persistence.Entities.MunicipalityLocation).MunicipalityId.Value };
        }

        public override string ToString()
        {
            var baseLine = $"{Address}, {Municipality.Name}, {Municipality.State.Name}";

            if (!string.IsNullOrEmpty(Label))
            {
                return $"{baseLine} ({Label})";
            }

            return baseLine;
        }
    }
}
