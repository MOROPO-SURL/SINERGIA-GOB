namespace MOROPO.SINETGIAGOB.GovCamaguey.Terminal.Helpers
{
    public class MainExceptionHandlingHelper : Core.Presentation.Terminal.MAUI.Helpers.Facade.IExceptionHandlingHelperFacade
    {
        private const string SUPPORT_ORIENTATION_MESSAGE = "Por favor contacte con el personal de asistencia técnica para notificar esta situación.";

        public const string INVALID_CREDENTIALS_LDAP_EXCEPTION_MESSAGE = "Usuario y/o contraseña incorrectos.";

        public static string UNSPECTED_LDAP_EXCEPTION_MESSAGE = string.Format("Error al contactar con el servicio de autenticación.{1}{1}{0}", SUPPORT_ORIENTATION_MESSAGE, System.Environment.NewLine);

        public const string INSUFFICIENT_STORAGE_SPACE_EXCEPTION_MESSAGE = "El espacio de almacenamiento disponible en su dispositivo no será suficiente para descargar este audio visual.";

        public const string UNSPECIFIED_STORAGE_DEVICE_EXCEPTION_MESSAGE = "Dispositivo de almacenamiento no especificado.";

        public const string LOCKED_AUDIOVISUAL_FOR_FETCH_LIST_EXCEPTION_MESSAGE = "La lista de Audio Visuales a descargar no puede ser modificada durante el proceso de descarga";

        public static string UPGRADE_REQUIRED_EXCEPTION_MESSAGE = string.Format("Es necearia la actualización de esta aplicación para poder continuar.{1}{1}{0}", SUPPORT_ORIENTATION_MESSAGE, System.Environment.NewLine);

        public static string UNSPECTED_API_RESPONSE_EXCEPTION_MESSAGE = string.Format("Respuesta del proveedor del sistema no esperada.{1}{1}{0}", SUPPORT_ORIENTATION_MESSAGE, System.Environment.NewLine);

        public static string NETWORK_EXCEPTION_MESSAGE = string.Format("Error de comunicación con el proveedor del sistema.{1}{1}{0}", SUPPORT_ORIENTATION_MESSAGE, System.Environment.NewLine);

        public static string TASK_CANCELED_EXCEPTION_MESSAGE = string.Format("El sistema ha tardado demasiado tiempo en responder.{1}{1}{0}", SUPPORT_ORIENTATION_MESSAGE, System.Environment.NewLine);

        public static string UNSPECTED_EXCEPTION_MESSAGE = string.Format("Error no esperado en el sistema.{1}{1}{0}", SUPPORT_ORIENTATION_MESSAGE, System.Environment.NewLine);

        public const string ADVICE_ALERT_DIALOG_TITLE = "Aviso";

        public const string CONFIRMATION_ALERT_DIALOG_TITLE = "Confirmar";

        public const string MALE_NONE_OPTION = "Ninguno";

        public const string OK_ACTION = "Aceptar";

        public const string CANCEL_ACTION = "Cancelar";

        public void HandleException(System.Exception exception, Microsoft.Maui.Controls.Page page, System.Action<System.Exception> callbackAction = null)
        {
            if (exception == null)
            {
                page.DisplayAlert(ADVICE_ALERT_DIALOG_TITLE, TASK_CANCELED_EXCEPTION_MESSAGE, OK_ACTION).ContinueWith(_ => { callbackAction?.Invoke(exception); });

                return;
            }

            if (exception.InnerException is Framework.Guama.Core.Presentation.API.Connect.UnspectedHttpStatusException)
            {
                var concreteException = exception.InnerException as Framework.Guama.Core.Presentation.API.Connect.UnspectedHttpStatusException;

                if (concreteException.HttpStatusCode == System.Net.HttpStatusCode.UpgradeRequired)
                {
                    page.DisplayAlert(ADVICE_ALERT_DIALOG_TITLE, UPGRADE_REQUIRED_EXCEPTION_MESSAGE, OK_ACTION).ContinueWith(_ => { callbackAction?.Invoke(exception); });
                }
                else
                {
#if DEBUG
                    page.DisplayAlert(ADVICE_ALERT_DIALOG_TITLE, string.Format("Respuesta del proveedor del sistema no esperada.{1}{1}Código del error: {2}{1}{1}{0}", SUPPORT_ORIENTATION_MESSAGE, System.Environment.NewLine, concreteException.HttpStatusCode), OK_ACTION).ContinueWith(_ => { callbackAction?.Invoke(exception); });
#else
                    page.DisplayAlert(ADVICE_ALERT_DIALOG_TITLE, UNSPECTED_API_RESPONSE_EXCEPTION_MESSAGE, OK_ACTION).ContinueWith(_ => { callbackAction?.Invoke(exception); });
#endif
                }
            }
            else if (exception.InnerException is System.Net.Http.HttpRequestException || exception.InnerException?.InnerException is System.Net.Sockets.SocketException)
            {
                page.DisplayAlert(ADVICE_ALERT_DIALOG_TITLE, NETWORK_EXCEPTION_MESSAGE, OK_ACTION).ContinueWith(_ => { callbackAction?.Invoke(exception); });
            }
            else
            {
                page.DisplayAlert(ADVICE_ALERT_DIALOG_TITLE, UNSPECTED_EXCEPTION_MESSAGE, OK_ACTION).ContinueWith(_ => { callbackAction?.Invoke(exception); });
            }
            // TODO Handle native exceptions
            //else
            //{
            //    var nativeMessage = Xamarin.Forms.DependencyService.Get<IApplicationManagerFacade>().GetUnhandleExceptionMessage(targetException);

            //    if (nativeMessage != null) message = nativeMessage;
            //}
        }
    }
}
