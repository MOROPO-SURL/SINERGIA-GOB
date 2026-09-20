namespace MOROPO.SINERGIAGOB.Core.Presentation.Terminal.MAUI.UI.Default.Delegates
{
    public delegate void TouchActionEventHandler(object sender, TouchActionEventArgs args);

    public class TouchActionEventArgs : System.EventArgs
    {
        public TouchActionEventArgs(long id, Enumerations.TouchActionKind kind, Microsoft.Maui.Graphics.Point location, bool isInContact)
        {
            Id = id;
            Kind = kind;
            Location = location;
            IsInContact = isInContact;
        }

        public long Id { private set; get; }

        public Enumerations.TouchActionKind Kind { private set; get; }

        public Microsoft.Maui.Graphics.Point Location { private set; get; }

        public bool IsInContact { private set; get; }
    }
}
