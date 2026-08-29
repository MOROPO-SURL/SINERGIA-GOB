namespace MOROPO.SINETGIAGOB.GovCamaguey.Terminal.UI.Behaviors;

public sealed class DecimalSymbolIdicatotionLabelBehavior : Microsoft.Maui.Controls.Behavior<Microsoft.Maui.Controls.Label>
{
    protected override void OnAttachedTo(Microsoft.Maui.Controls.Label bindable)
    {
        base.OnAttachedTo(bindable);

        bindable.Text = $"Use el símbolo \"{System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator}\" para definir puestos decimales";
    }
}
