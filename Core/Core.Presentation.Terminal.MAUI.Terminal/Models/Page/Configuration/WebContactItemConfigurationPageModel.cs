namespace MOROPO.SINETGIAGOB.Core.Presentation.Terminal.MAUI.Models.Page.Configuration
{
    public class WebContactItemConfigurationPageModel : Contract.AbstractConfigurationPageModel<Interop.API.Common.Models.Display.WebContactItemDisplayModel>
    {
        private string label;
        private string url;

        public WebContactItemConfigurationPageModel(Interop.API.Common.Models.Display.WebContactItemDisplayModel model) : base(model)
        {
            if (model != null)
            {
                Id = model.Id;
                Label = model.Label;
                Url = model.Url;
            }
        }

        public System.Guid Id { get; set; }

        public string Label { get => label; set => SetProperty(ref label, value); }

        public string Url { get => url; set => SetProperty(ref url, value); }
    }
}
