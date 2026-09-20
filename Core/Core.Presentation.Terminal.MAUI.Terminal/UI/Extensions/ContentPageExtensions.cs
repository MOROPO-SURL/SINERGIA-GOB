using Microsoft.Maui.Controls;

namespace MOROPO.SINERGIAGOB.Core.Presentation.Terminal.MAUI.UI.Extensions
{
    public static class ContentPageExtensions
    {
        public static void PresentToastNotification(this ContentPage target, Enumerations.NotificationKind kind, string message, System.TimeSpan? delay = null, System.TimeSpan? time = null)
        {
//#if WINDOWS
            if (string.IsNullOrEmpty(message) || string.IsNullOrWhiteSpace(message)) return;

            if (target.Content is not Grid toastContainerGrid)
            {
                if (target.Content is ScrollView targetScrollViewContent && targetScrollViewContent.Content is not Grid)
                {
                    throw new System.InvalidOperationException();
                }
                else
                {
                    toastContainerGrid = (target.Content as ScrollView).Content as Grid;
                }
            }

            if (toastContainerGrid == null) throw new System.InvalidOperationException();

            var toastFrame = new Frame
            {
                Style = (Style)Application.Current.Resources[$"{System.Enum.GetName(kind)}ToastFrameStyle"],
                Opacity = 0,
                Margin = new Microsoft.Maui.Thickness(48),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.End,
                Content = new Label
                {
                    Style = (Style)Application.Current.Resources[$"{System.Enum.GetName(kind)}ToastLabelStyle"],
                    Text = message 
                }
            };

            if (toastContainerGrid.ColumnDefinitions.Count > 1) Grid.SetColumnSpan(toastFrame, toastContainerGrid.ColumnDefinitions.Count);

            if (toastContainerGrid.RowDefinitions.Count > 1) Grid.SetRowSpan(toastFrame, toastContainerGrid.RowDefinitions.Count);

            toastContainerGrid.Add(toastFrame);

            System.Threading.Tasks.Task.Delay(delay ?? System.TimeSpan.FromMilliseconds(100)).ContinueWith(_ =>
            {
                target.Dispatcher.Dispatch(() =>
                {
                    toastFrame.FadeTo(1).ContinueWith(async _ =>
                    {
                        await System.Threading.Tasks.Task.Delay(time ?? System.TimeSpan.FromSeconds(3));

                        target.Dispatcher.Dispatch(() =>
                        {
                            toastFrame.FadeTo(0).ContinueWith(_ =>
                            {
                                target.Dispatcher.Dispatch(() =>
                                {
                                    toastContainerGrid.Remove(toastFrame);

                                    toastFrame = null;
                                    toastContainerGrid = null;
                                });
                            });
                        });
                    });
                });
            });
//#else
//            CommunityToolkit.Maui.Alerts.Snackbar.Make(message).Show();
//#endif
        }
    }
}
