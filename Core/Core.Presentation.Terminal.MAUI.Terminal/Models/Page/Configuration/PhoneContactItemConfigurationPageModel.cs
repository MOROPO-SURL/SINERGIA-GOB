namespace MOROPO.SINERGIAGOB.Core.Presentation.Terminal.MAUI.Models.Page.Configuration
{
    public class PhoneContactItemConfigurationPageModel : Contract.AbstractConfigurationPageModel<Interop.API.Common.Models.Display.PhoneContactItemDisplayModel>
    {
        private string label;
        private string phoneNumber;

        public PhoneContactItemConfigurationPageModel(Interop.API.Common.Models.Display.PhoneContactItemDisplayModel model) : base(model)
        {
            if (model != null)
            {
                Id = model.Id;
                Label = model.Label;
                PhoneNumber = model.PhoneNumber;
            }
        }

        public System.Guid Id { get; set; }

        public string Label { get => label; set => SetProperty(ref label, value); }

        public string PhoneNumber { get => phoneNumber; set => SetProperty(ref phoneNumber, value); }
    }
}
