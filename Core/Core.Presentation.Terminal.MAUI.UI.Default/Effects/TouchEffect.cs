namespace MOROPO.SINERGIAGOB.Core.Presentation.Terminal.MAUI.UI.Default.Effects
{
    public class TouchEffect : Microsoft.Maui.Controls.RoutingEffect
    {
        public event Delegates.TouchActionEventHandler TouchAction;

        public TouchEffect() : base("Decisor.TouchEffect")
        {
        }

        public bool Capture { set; get; }

        public void OnTouchAction(Microsoft.Maui.Controls.Element element, Delegates.TouchActionEventArgs args)
        {
            TouchAction?.Invoke(element, args);
        }
    }
}
