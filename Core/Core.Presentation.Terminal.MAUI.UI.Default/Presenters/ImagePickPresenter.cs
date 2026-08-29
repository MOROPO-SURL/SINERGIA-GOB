using Microsoft.Maui.Dispatching;
using MOROPO.Framework.Caney.MVVM.Core.Extensions;
using MOROPO.SINETGIAGOB.Core.Presentation.Terminal.MAUI.Constants;

namespace MOROPO.SINETGIAGOB.Core.Presentation.Terminal.MAUI.UI.Default.Presenters
{
    public sealed class ImagePickPresenter
    {
        private static ImagePickPresenter instance;

        public static ImagePickPresenter Instance
        {
            get 
            {
                instance ??= new ImagePickPresenter();

                return instance;
            }
        }

        private Microsoft.Maui.Controls.Page page;
        private System.Action<Interop.API.Common.Models.Input.ImageInputModel> successAction;

        public async void BeginImagePickAsync(Microsoft.Maui.Controls.Page page, System.Action<Interop.API.Common.Models.Input.ImageInputModel> successAction)
        {
            this.page = page;
            this.successAction = successAction;

            var concreteBindingContext = (page.BindingContext as Framework.Caney.MVVM.Core.Models.Page.BasePageModel);

            if (!Microsoft.Maui.Media.MediaPicker.IsCaptureSupported)
            {
                PresentForImagePick();
            }

            var action = await page.DisplayActionSheet("Seleccionar imagen desde", "Cancelar", null, CommonActionSheetConstants.PICKIMAGE_OPTION_CAMARA, CommonActionSheetConstants.PICKIMAGE_OPTION_GALLERY);

            if (action == CommonActionSheetConstants.PICKIMAGE_OPTION_CAMARA)
            {
                var permission = await Microsoft.Maui.ApplicationModel.Permissions.CheckStatusAsync<Microsoft.Maui.ApplicationModel.Permissions.Camera>();

                if (permission != Microsoft.Maui.ApplicationModel.PermissionStatus.Granted)
                {
                    permission = await Microsoft.Maui.ApplicationModel.Permissions.RequestAsync<Microsoft.Maui.ApplicationModel.Permissions.Camera>();
                }

                if (permission != Microsoft.Maui.ApplicationModel.PermissionStatus.Granted)
                {
                    page.Dispatcher.Dispatch(() =>
                    {
                        page.DisplayAlert("Aviso", string.Format("El permiso para acceder a cámara del dispositivo es necesario para esta funcionalidad.{0}{0}Deberá permitir el acceso en los ajustes del sistema para la aplicación.", System.Environment.NewLine), "Aceptar");

                        concreteBindingContext.IsBusy = false;
                        concreteBindingContext.BusyProgressMessage = string.Empty;
                    });

                    return;
                }

                page.Dispatcher.Dispatch(() =>
                {
                    concreteBindingContext.IsBusy = true;
                    concreteBindingContext.BusyProgressMessage = "Capturando imagen ...";
                });

                await System.Threading.Tasks.Task.Delay(System.TimeSpan.FromSeconds(1));

                page.Dispatcher.Dispatch(() =>
                {
                    concreteBindingContext.BusyProgressMessage = "Preparando imagen ...";
                });

                await Microsoft.Maui.Media.MediaPicker.CapturePhotoAsync().ContinueWith(PickedImageContinuationTask);
            }
            else if (action == CommonActionSheetConstants.PICKIMAGE_OPTION_GALLERY)
            {
                PresentForImagePick();
            }
        }

        private void PickedImageCroppingComplete_Callback(SkiaSharp.SKBitmap skBitmap)
        {
            Interop.API.Common.Models.Input.ImageInputModel result = null;

            if (skBitmap != null)
            {
                using (var skImage = SkiaSharp.SKImage.FromBitmap(skBitmap))
                {
                    using (var skData = skImage.Encode(SkiaSharp.SKEncodedImageFormat.Jpeg, 75))
                    {
                        result = new Interop.API.Common.Models.Input.ImageInputModel
                        {
                            ContentData = skData.ToArray(),
                            ContentType = "image/jpeg"
                        };
                    }
                }

                skBitmap.Dispose();
            }

            page.NavigationPopModalPageAsync();

            var concreteBindingContext = (page.BindingContext as Framework.Caney.MVVM.Core.Models.Page.BasePageModel);

            concreteBindingContext.IsBusy = false;
            concreteBindingContext.BusyProgressMessage = string.Empty;

            if (result == null) return;

            successAction.Invoke(result);

            page = null;
            successAction = null;
        }

        private async void PresentForImagePick()
        {
            var concreteBindingContext = (page.BindingContext as Framework.Caney.MVVM.Core.Models.Page.BasePageModel);

            if (Microsoft.Maui.Devices.DeviceInfo.Platform == Microsoft.Maui.Devices.DevicePlatform.iOS)
            {
                var permission = await Microsoft.Maui.ApplicationModel.Permissions.CheckStatusAsync<Microsoft.Maui.ApplicationModel.Permissions.Photos>();

                if (permission != Microsoft.Maui.ApplicationModel.PermissionStatus.Granted)
                {
                    permission = await Microsoft.Maui.ApplicationModel.Permissions.RequestAsync<Microsoft.Maui.ApplicationModel.Permissions.Photos>();
                }

                if (permission != Microsoft.Maui.ApplicationModel.PermissionStatus.Granted)
                {
                    page.Dispatcher.Dispatch(() =>
                    {
                        page.DisplayAlert("Aviso", string.Format("El permiso para acceder a las fotos del dispositivo es necesario para esta funcionalidad.{0}{0}Deberá permitir el acceso en los ajustes del sistema para la aplicación.", System.Environment.NewLine), "Aceptar");

                        concreteBindingContext.IsBusy = false;
                        concreteBindingContext.BusyProgressMessage = string.Empty;

                        page = null;
                        successAction = null;
                    });

                    return;
                }
            }
            else
            {
                var permission = await Microsoft.Maui.ApplicationModel.Permissions.CheckStatusAsync<Microsoft.Maui.ApplicationModel.Permissions.StorageRead>();

                if (permission != Microsoft.Maui.ApplicationModel.PermissionStatus.Granted)
                {
                    permission = await Microsoft.Maui.ApplicationModel.Permissions.RequestAsync<Microsoft.Maui.ApplicationModel.Permissions.StorageRead>();
                }

                if (permission != Microsoft.Maui.ApplicationModel.PermissionStatus.Granted)
                {
                    page.Dispatcher.Dispatch(() =>
                    {
                        page.DisplayAlert("Aviso", string.Format("El permiso para acceder a las fotos del dispositivo es necesario para esta funcionalidad.{0}{0}Deberá permitir el acceso en los ajustes del sistema para la aplicación.", System.Environment.NewLine), "Aceptar");

                        concreteBindingContext.IsBusy = false;
                        concreteBindingContext.BusyProgressMessage = string.Empty;

                        page = null;
                        successAction = null;
                    });

                    return;
                }
            }

            page.Dispatcher.Dispatch(() =>
            {
                concreteBindingContext.IsBusy = true;
                concreteBindingContext.BusyProgressMessage = "Seleccionando imagen ...";
            });

            await Microsoft.Maui.Media.MediaPicker.PickPhotoAsync().ContinueWith(PickedImageContinuationTask);
        }

        private async System.Threading.Tasks.Task PickedImageContinuationTask(System.Threading.Tasks.Task<Microsoft.Maui.Storage.FileResult> task)
        {
            var concreteBindingContext = (page.BindingContext as Framework.Caney.MVVM.Core.Models.Page.BasePageModel);

            if (task.Result == null)
            {
                page.Dispatcher.Dispatch(() =>
                {
                    concreteBindingContext.IsBusy = false;
                    concreteBindingContext.BusyProgressMessage = string.Empty;
                });

                return;
            }

            await System.Threading.Tasks.Task.Delay(System.TimeSpan.FromMilliseconds(100));

            page.Dispatcher.Dispatch(() =>
            {
                concreteBindingContext.BusyProgressMessage = "Preparando imagen ...";
            });

            SkiaSharp.SKBitmap croppingImageTarget = null;

            using (var imageFileStream = await task.Result.OpenReadAsync())
            {
                using (var skStream = new SkiaSharp.SKManagedStream(imageFileStream))
                {
                    croppingImageTarget = SkiaSharp.SKBitmap.Decode(skStream);
                }
            }

            page.Dispatcher.Dispatch(() =>
            {
                page.DisplayAlert("Recortar", "Desea recortar esta imagen?", "Si", "No").ContinueWith(cropAskTask =>
                {
                    page.Dispatcher.Dispatch(() =>
                    {
                        if (cropAskTask.Result)
                        {
                            concreteBindingContext.BusyProgressMessage = "Recortando imagen ...";

                            page.NavigationPushModalPageAsync(new Microsoft.Maui.Controls.NavigationPage(new Pages.ImageCropPage(croppingImageTarget, PickedImageCroppingComplete_Callback)));
                        }
                        else
                        {
                            Interop.API.Common.Models.Input.ImageInputModel result = null;

                            using (var skImage = SkiaSharp.SKImage.FromBitmap(croppingImageTarget))
                            {
                                using (var skData = skImage.Encode(SkiaSharp.SKEncodedImageFormat.Jpeg, 75))
                                {
                                    result = new Interop.API.Common.Models.Input.ImageInputModel
                                    {
                                        ContentData = skData.ToArray(),
                                        ContentType = task.Result.ContentType
                                    };
                                }
                            }

                            croppingImageTarget.Dispose();

                            var concreteBindingContext = (page.BindingContext as Framework.Caney.MVVM.Core.Models.Page.BasePageModel);

                            concreteBindingContext.IsBusy = false;
                            concreteBindingContext.BusyProgressMessage = string.Empty;

                            successAction.Invoke(result);

                            page = null;
                            successAction = null;
                        }
                    });
                });
            });
        }
    }
}
