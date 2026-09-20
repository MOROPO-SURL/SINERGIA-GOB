namespace MOROPO.SINERGIAGOB.Core.Presentation.Terminal.MAUI.Models.Page.Configuration
{
    public class EmailContactItemConfigurationPageModel : Contract.AbstractConfigurationPageModel<Interop.API.Common.Models.Display.EmailContactItemDisplayModel>
    {
        private string label;
        private string emailAddress;

        public EmailContactItemConfigurationPageModel(Interop.API.Common.Models.Display.EmailContactItemDisplayModel model) : base(model)
        {
            if (model != null)
            {
                Id = model.Id;
                Label = model.Label;
                EmailAddress = model.EmailAddress;
            }
        }

        public System.Guid Id { get; set; }

        public string Label { get => label; set => SetProperty(ref label, value); }

        public string EmailAddress { get => emailAddress; set => SetProperty(ref emailAddress, value); }
    }
}
