using Microsoft.Extensions.DependencyInjection;
using MOROPO.Framework.Caney.MVVM.Core.Extensions;
using System.Linq;

namespace MOROPO.SINERGIAGOB.Core.Presentation.Terminal.MAUI.UI.Pages.Base
{
    public class ConfigurationBaseContentPage<TInModel, TOutModel, TConfigurationPageModel> : Framework.Caney.MVVM.Core.Pages.ContentPage
        where TInModel : class
        where TOutModel : class
        where TConfigurationPageModel : Models.Page.Contract.AbstractConfigurationPageModel<TInModel>
    {
        private readonly System.Action<TOutModel> successCallbackAction;
        private readonly FluentValidation.AbstractValidator<TConfigurationPageModel> validator;
        protected bool hasChangesFlag;

        public ConfigurationBaseContentPage(System.Action<TOutModel> successCallbackAction, TConfigurationPageModel bindingContextModel, FluentValidation.AbstractValidator<TConfigurationPageModel> validator)
        {
            this.successCallbackAction = successCallbackAction;
            this.validator = validator;

            if (bindingContextModel.OnEditionMode)
            {
                Title = $"Modificar {EntityTypeName}";
            }
            else
            {
                Title = $"Agregar {EntityTypeName}";
            }

            BindingContext = bindingContextModel;

            ((Framework.Caney.MVVM.Core.Models.Page.BasePageModel)BindingContext).PropertyChanged += PageModel_PropertyChanged;
        }

        protected override bool OnBackButtonPressed()
        {
            OnCancel(this, System.EventArgs.Empty);

            return true;
        }

        protected virtual string EntityTypeName { get; } = "Elemento";

        protected virtual string CreatorPermissionCode => null;

        protected virtual string EditorPermissionCode => null;

        public TConfigurationPageModel ConcreteBindingContext => (TConfigurationPageModel)BindingContext;

        protected virtual System.Threading.Tasks.Task<TOutModel> PerformCreationAsync() => throw new System.NotImplementedException();

        protected virtual System.Threading.Tasks.Task<TOutModel> PerformEditionAsync() => throw new System.NotImplementedException();

        private void PageModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            hasChangesFlag = true;
        }

        protected void OnApply(object sender, System.EventArgs eventArgs)
        {
            if (ConcreteBindingContext.OnEditionMode && !hasChangesFlag)
            {
                this.NavigationPopPageAsync();

                return;
            }

            if (!ConcreteBindingContext.OnEditionMode)
            {
                if (CreatorPermissionCode != null)
                {
                    var user = Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Managers.Facade.ISessionManagerFacade>().CurrentSession.User;

                    if (!user.HasFullAccess && !user.Roles.Any(item => item.Permissions.Any(_item => _item.Code == CreatorPermissionCode)))
                    {
                        DisplayAlert("Aviso", "Su usuario no cuenta con permisos para agregar nuevos elementos. Póngase en contacto con los administradores del sistema para solicitar los permisos requeridos.", "Aceptar");

                        return;
                    }
                }
            }
            else
            {
                if (EditorPermissionCode != null)
                {
                    var user = Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Managers.Facade.ISessionManagerFacade>().CurrentSession.User;

                    if (!user.HasFullAccess && !user.Roles.Any(item => item.Permissions.Any(_item => _item.Code == EditorPermissionCode)))
                    {
                        DisplayAlert("Aviso", "Su usuario no cuenta con permisos para modificar elementos registrados. Póngase en contacto con los administradores del sistema para solicitar los permisos requeridos.", "Aceptar");

                        ConcreteBindingContext.IsBusy = false;

                        return;
                    }
                }
            }

            if (validator != null)
            {
                ConcreteBindingContext.ValidationResult = null;

                ConcreteBindingContext.ValidationResult = validator.Validate(ConcreteBindingContext);

                if (!ConcreteBindingContext.ValidationResult.IsValid)
                {
                    DisplayAlert("Aviso", "La información especificada no es válida para aplicar los cambios en el sistema.", "Aceptar");

                    return;
                }
            }

            ConcreteBindingContext.IsBusy = true;

            var task = ConcreteBindingContext.OnEditionMode ? PerformEditionAsync() : PerformCreationAsync();

            task.ContinueWith(task =>
            {
                Dispatcher.Dispatch(() =>
                {
                    if (task.Exception == null && !task.IsCanceled)
                    {
                        successCallbackAction.Invoke(task.Result);

                        this.NavigationPopPageAsync();
                    }
                    else
                    {
                        ((Framework.Caney.MVVM.Core.Models.Page.BasePageModel)BindingContext).IsBusy = false;

                        HandleException(task.Exception);
                    }
                });
            });
        }

        protected void OnCancel(object sender, System.EventArgs eventArgs)
        {
            if (!hasChangesFlag)
            {
                this.NavigationPopPageAsync();

                return;
            }

            DisplayAlert("Confirmación", "Está seguro que desea regresar sin guardar los cambios?", "Regresar", "Cancelar").ContinueWith(task =>
            {
                if (!task.Result) return;

                Dispatcher.Dispatch(() => this.NavigationPopPageAsync());
            });
        }

        protected virtual void HandleException(System.Exception exception)
        {
            Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Helpers.Facade.IExceptionHandlingHelperFacade>().HandleException(exception, this);
        }
    }
}
