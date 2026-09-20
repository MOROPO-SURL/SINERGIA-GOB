using MOROPO.SINERGIAGOB.Core.Interop.API.Common.Models.Display;

namespace MOROPO.SINERGIAGOB.Core.Presentation.Terminal.MAUI.Models.Page.Configuration
{
    public class PostalContactConfigurationPageModel : Contract.AbstractConfigurationPageModel<PostalContactItemDisplayModel>
    {
        private string label;
        private string address;
        private int? zipCode;
        private decimal longitude;
        private decimal latitude;

        public PostalContactConfigurationPageModel(PostalContactItemDisplayModel model) : base(model)
        {
            if (model != null)
            {
                Id = model.Id;
                label = model.Label;
                address = model.Address;
                latitude = model.Latitude;
                longitude = model.Longitude;
                Municipality = model.Municipality;

                if (model.ZipCode > 0) zipCode = model.ZipCode;
            }
        }

        public System.Guid Id { get; set; }

        public string Label { get => label; set => SetProperty(ref label, value); }

        public string Address { get => address; set => SetProperty(ref address, value); }

        public int? ZipCode { get => zipCode; set => SetProperty(ref zipCode, value); }

        public decimal Longitude { get => longitude; set => SetProperty(ref longitude, value); }

        public decimal Latitude { get => latitude; set => SetProperty(ref latitude, value); }

        public MunicipalityDisplayModel Municipality { get; set; }

        public StateDisplayModel[] AvailableStates { get; set; }

        public System.Collections.ObjectModel.ObservableCollection<MunicipalityDisplayModel> AvailableMunicipalities { get; set; } = [];
    }
}
