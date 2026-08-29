namespace MOROPO.SINETGIAGOB.Core.Presentation.Terminal.MAUI.Models.Page.Configuration
{
    public class DocumentConfigurationPageModel : Contract.AbstractConfigurationPageModel<Interop.API.Common.Models.Display.DocumentDisplayModel>
    {
        private string name;
        private string description;

        public DocumentConfigurationPageModel(Interop.API.Common.Models.Display.DocumentDisplayModel model) : base(model)
        {
            Id = model.Id;
            Name = model.Name;
            Description = model.Description;
            Extension = model.Extension;
            Length = model.Length;
        }

        public System.Guid Id { get; set; }

        public string Name { get => name; set => SetProperty(ref name, value); }

        public string Description { get => description; set => SetProperty(ref description, value); }

        public string Extension { get; set; }

        public long Length { get; set; }
    }
}
