namespace MOROPO.SINETGIAGOB.Core.Presentation.Terminal.MAUI.UI.Behaviors;

public sealed class DecimalSymbolIdicatotionLabelBehavior : Microsoft.Maui.Controls.Behavior<Microsoft.Maui.Controls.Label>
{
    protected override void OnAttachedTo(Microsoft.Maui.Controls.Label bindable)
    {
        base.OnAttachedTo(bindable);

        bindable.FontAttributes = Microsoft.Maui.Controls.FontAttributes.Italic;
        bindable.Text = $"Use el símbolo \"{System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator}\" para definir puestos decimales";
    }
}
