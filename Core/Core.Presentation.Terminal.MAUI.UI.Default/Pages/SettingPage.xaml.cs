using MOROPO.Framework.Caney.MVVM.Core.Extensions;

namespace MOROPO.SINETGIAGOB.Core.Presentation.Terminal.MAUI.UI.Default.Pages;

public partial class SettingPage : Microsoft.Maui.Controls.ContentPage
{
	private readonly System.Action settingChangeCallbackAction;

    public SettingPage(System.Action settingChangeCallbackAction)
	{
		InitializeComponent();

		this.settingChangeCallbackAction = settingChangeCallbackAction;

		BindingContext = new Models.Page.SettingPageModel(ApplySuccess_Callback, Validation_Callback);
	}

    private void ApplySuccess_Callback()
	{
        settingChangeCallbackAction?.Invoke();

        this.NavigationPopModalPageAsync();
    }

    private void Validation_Callback()
	{
		var concreteBindingContext = BindingContext as Models.Page.SettingPageModel;

		if (concreteBindingContext.ValidationResult == null || concreteBindingContext.ValidationResult.IsValid) return;

        DisplayAlert("Aviso", "La información especificada no es válida para aplicar los cambios en el sistema.", "Aceptar");
    }

    protected void DismissControl_Invoked(object sender, System.EventArgs eventArgs)
    {
        this.NavigationPopModalPageAsync();
    }
}