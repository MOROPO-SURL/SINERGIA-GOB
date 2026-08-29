namespace MOROPO.SINETGIAGOB.GovCamaguey.Terminal.Models.Page.Configuration
{
    public class GuidelineConfigurationPageModel : Core.Presentation.Terminal.MAUI.Models.Page.Contract.AbstractConfigurationPageModel<Interop.API.Common.Models.Display.GuidelineDisplayModel>
    {
        public static string NameValidableProperty => nameof(Name);
        public static string IndexValidableProperty => nameof(Index);
        public static string DescriptionValidableProperty => nameof(Description);

        private string name;
        private string description;
        private int index = 1;

        public GuidelineConfigurationPageModel() : this(null) { }

        public GuidelineConfigurationPageModel(Interop.API.Common.Models.Display.GuidelineDisplayModel model) : base(model)
        {
            if (model != null)
            {
                Id = model.Id;
                Index = model.Index;
                Name = model.Name;
                Description = model.Description;
            }
        }

        public System.Guid Id { get; set; }

        public int Index { get => index; set => SetProperty(ref index, value); }

        public string Name { get => name; set => SetProperty(ref name, value); }

        public string Description { get => description; set => SetProperty(ref description, value); }
    }
}
