using MOROPO.Framework.Caney.MVVM.Core.Extensions;

namespace MOROPO.SINETGIAGOB.GovCamaguey.Terminal.UI.Pages.Configuration;

public partial class CommonIndicatorResultConfigurationPage : Framework.Caney.MVVM.Core.Pages.BindingContextedContentPage<Models.Page.Configuration.CommonIndicatorResultConfigurationPageModel>
{
	private readonly System.Action<Models.Data.CommonIndicatorResultDataModel> successCallbackAction;

	public CommonIndicatorResultConfigurationPage(Models.Page.Configuration.CommonIndicatorResultConfigurationPageModel model, System.Action<Models.Data.CommonIndicatorResultDataModel> successCallbackAction) : base(model)
	{
		InitializeComponent();

		this.successCallbackAction = successCallbackAction;
    }

    protected override bool OnBackButtonPressed()
    {
        DismissButton_Clicked(this, System.EventArgs.Empty);

        return true;
    }

    private void ApplyButton_Clicked(object sender, System.EventArgs eventArgs)
    {
        DisplayAlert("Confirmar", $"Está seguro que desea actualizar el estado del indicador al valor de {BindingContextPageModel.Amount} {BindingContextPageModel.IndicatorUnitMeasurement.Symbol}, junto al resto de los datos especificados?", "Aplicar", "Cancelar").ContinueWith(task =>
        {
            if (!task.Result) return;

            Dispatcher.Dispatch(() =>
            {
                Validators.CommonIndicatorResultConfigurationValidator validator = new();

                if (validator != null)
                {
                    BindingContextPageModel.ValidationResult = null;

                    BindingContextPageModel.ValidationResult = validator.Validate(BindingContextPageModel);

                    if (!BindingContextPageModel.ValidationResult.IsValid)
                    {
                        DisplayAlert("Aviso", "La información especificada no es válida para aplicar los cambios en el sistema.", "Aceptar");

                        return;
                    }
                }

                Models.Data.CommonIndicatorResultDataModel model = new(BindingContextPageModel.IndicatorId, BindingContextPageModel.Amount, BindingContextPageModel.At, BindingContextPageModel.Description);

                this.NavigationPopModalPageAsync().ContinueWith(_ =>
                {
                    Dispatcher.Dispatch(() => successCallbackAction.Invoke(model));
                });
            });
        });
    }

    protected void DismissButton_Clicked(object sender, System.EventArgs eventArgs)
    {
        this.NavigationPopModalPageAsync();
    }
}