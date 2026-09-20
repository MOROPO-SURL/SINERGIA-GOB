namespace MOROPO.SINERGIAGOB.Core.Interop.API.Common.Models.Input
{
    public class PostalContactItemInputModel
    {
        public System.Guid Id { get; set; }

        public System.Guid MunicipalityId { get; set; }

        public string Label { get; set; }

        public string Address { get; set; }

        public int ZipCode { get; set; }

        public decimal Longitude { get; set; }

        public decimal Latitude { get; set; }
    }
}
