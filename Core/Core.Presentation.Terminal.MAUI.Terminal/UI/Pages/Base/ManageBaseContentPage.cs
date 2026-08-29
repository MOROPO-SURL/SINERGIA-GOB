using Microsoft.Extensions.DependencyInjection;
using MOROPO.Framework.Caney.MVVM.Core.Extensions;
using MOROPO.SINETGIAGOB.Core.Presentation.Terminal.MAUI.UI.Extensions;
using System.Linq;

namespace MOROPO.SINETGIAGOB.Core.Presentation.Terminal.MAUI.UI.Pages.Base
{
    public class ManageBaseContentPage<TModel, TManagePageModel, TConfigurationPageModel> : ListBaseContentPage<TModel, TManagePageModel>
        where TModel : class
        where TManagePageModel : Models.Page.Contract.AbstractManageEntityPageModel<TModel>, new()
        where TConfigurationPageModel : Models.Page.Contract.AbstractConfigurationPageModel<TModel>
    {
        protected virtual bool ConfigurationRequiredAsyncWorkPresentation => false;

        protected virtual string CreatorPermissionCode => null;

        protected virtual string EditorPermissionCode => null;

        protected override void ItemDetailRequestControl_Invoked(object sender, System.EventArgs eventArgs)
        {
            if (((TManagePageModel)BindingContext).IsBusy) return;

            FillItemDataAsync((sender as Microsoft.Maui.Controls.BindableObject).BindingContext as TModel).ContinueWith(task =>
            {
                Dispatcher.Dispatch(() =>
                {
                    var factory = Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Factories.Facade.IDetailContentPageFactoryFacade<TModel>>();

                    try
                    {
                        var page = factory.Build(task.Result, DetailActionDelegationRequest_Callback);

                        this.NavigationPushModalPageAsync(page);
                    }
                    catch (System.Exception exception)
                    {
                        DisplayAlert("Aviso", string.Format("Unspected runtime error!! {0}", exception.Message), "Aceptar");

                        return;
                    }
                });
            });
        }

        protected void AddItem(object sender, System.EventArgs eventArgs)
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

            ConcreteBindingContext.IsBusy = ConfigurationRequiredAsyncWorkPresentation;

            System.Threading.Tasks.Task.Run(async () =>
            {
                try
                {
                    var factory = Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Factories.Facade.IConfigurationContentPageFactoryFacade<TModel, TConfigurationPageModel>>();

                    var page = await factory.BuildAsync(ConfigurationSuccess_Callback, null);

                    ComplementConfigurationPage(page);

                    Dispatcher.Dispatch(() =>
                    {
                        ConcreteBindingContext.IsBusy = false;

                        this.NavigationPushPageAsync(page);
                    });
                }
                catch (System.Exception exception)
                {
                    Dispatcher.Dispatch(() =>
                    {
                        Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Helpers.Facade.IExceptionHandlingHelperFacade>().HandleException(exception, this);
                    });
                }
            });
        }

        private void DetailActionDelegationRequest_Callback(TModel target, int? discriminator)
        {
            ConcreteBindingContext.IsBusy = ConfigurationRequiredAsyncWorkPresentation;

            if (discriminator == null)
            {
                var user = Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Managers.Facade.ISessionManagerFacade>().CurrentSession.User;

                if (!IsAllowedForEdition(target, user))
                {
                    DisplayAlert("Aviso", "Su usuario no cuenta con permisos para modificar elementos registrados. Póngase en contacto con los administradores del sistema para solicitar los permisos requeridos.", "Aceptar");

                    ConcreteBindingContext.IsBusy = false;

                    return;
                }

                System.Threading.Tasks.Task.Run(async () =>
                {
                    try
                    {
                        var factory = Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Factories.Facade.IConfigurationContentPageFactoryFacade<TModel, TConfigurationPageModel>>();

                        var page = await factory.BuildAsync(ConfigurationSuccess_Callback, target);

                        ComplementConfigurationPage(page, target);

                        Dispatcher.Dispatch(() =>
                        {
                            ConcreteBindingContext.IsBusy = false;

                            this.NavigationPushPageAsync(page);
                        });
                    }
                    catch (System.Exception exception)
                    {
                        Dispatcher.Dispatch(() =>
                        {
                            Microsoft.Maui.IPlatformApplication.Current.Services.GetRequiredService<Helpers.Facade.IExceptionHandlingHelperFacade>().HandleException(exception, this);
                        });
                    }
                });
            }
            else OnActionDelegationRequest(target, discriminator.Value);
        }

        protected virtual bool IsAllowedForEdition(TModel target, Interop.API.Common.Models.Display.UserDisplayModel user)
        {
            if (EditorPermissionCode != null)
            {
                return user.HasFullAccess && !user.Roles.Any(item => item.Permissions.Any(_item => _item.Code == EditorPermissionCode));
            }

            return true;
        }

        protected virtual void OnActionDelegationRequest(TModel target, int discriminator) { }

        protected virtual void ConfigurationSuccess_Callback(TModel model)
        {
            this.PresentToastNotification(Enumerations.NotificationKind.Success, "Cambios aplicados con éxito.", delay: System.TimeSpan.FromMilliseconds(500));

            LoadItems(true);
        }

        protected virtual void ComplementConfigurationPage(ConfigurationBaseContentPage<TModel, TModel, TConfigurationPageModel> page, TModel model = null)
        {
        }
    }
}
