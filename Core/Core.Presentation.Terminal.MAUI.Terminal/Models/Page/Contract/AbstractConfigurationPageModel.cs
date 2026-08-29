namespace MOROPO.SINETGIAGOB.Core.Presentation.Terminal.MAUI.Models.Page.Contract
{
    public abstract class AbstractConfigurationPageModel<TModel> : Framework.Caney.MVVM.Core.Models.Page.BasePageModel
    {
        private FluentValidation.Results.ValidationResult validationResult;

        protected AbstractConfigurationPageModel(TModel model)
        {
            OnEditionMode = model != null;
        }

        public bool OnEditionMode { get; private set; }

        public FluentValidation.Results.ValidationResult ValidationResult { get => validationResult; set => SetProperty(ref validationResult, value); }

        public System.Collections.ObjectModel.ObservableCollection<Interop.API.Common.Models.Display.UserDisplayModel> NotificationUsers { get; private set; } = [];
    }
}