using MOROPO.SINETGIAGOB.GovCamaguey.Interop.API.Common.Models.Input;

namespace MOROPO.SINETGIAGOB.GovCamaguey.Terminal.UI.Pages.Configuration;

public partial class ProyectionIndicatorMontlyPlanConfigurationPage : Core.Presentation.Terminal.MAUI.UI.Pages.Base.ConfigurationBaseContentPage<ProyectionIndicatorMontlyPlanInputModel, ProyectionIndicatorMontlyPlanInputModel, Models.Page.Configuration.ProyectionIndicatorMontlyPlanConfigurationPageModel>
{
    public ProyectionIndicatorMontlyPlanConfigurationPage(System.Action<ProyectionIndicatorMontlyPlanInputModel> successIndicator, Models.Page.Configuration.ProyectionIndicatorMontlyPlanConfigurationPageModel model) : base(successIndicator, model, new Validators.ProyectionIndicatorMontlyPlanConfigurationValidator())
    {
        InitializeComponent();
    }

    protected override string EntityTypeName => "Plan de Indicador";

    protected override System.Threading.Tasks.Task<ProyectionIndicatorMontlyPlanInputModel> PerformCreationAsync()
    {
        return System.Threading.Tasks.Task.FromResult(new ProyectionIndicatorMontlyPlanInputModel
        {
            Id = System.Guid.NewGuid(),
            Month = ConcreteBindingContext.YearMonth.Value,
            Year = ConcreteBindingContext.YearMonth.Key,
            Amount = ConcreteBindingContext.Amount
        });
    }

    protected override System.Threading.Tasks.Task<ProyectionIndicatorMontlyPlanInputModel> PerformEditionAsync()
    {
        return System.Threading.Tasks.Task.FromResult(new ProyectionIndicatorMontlyPlanInputModel
        {
            Id = ConcreteBindingContext.Id,
            Month = ConcreteBindingContext.YearMonth.Value,
            Year = ConcreteBindingContext.YearMonth.Key,
            Amount = ConcreteBindingContext.Amount
        });
    }

    private void ApplyToolbarItem_Clicked(object sender, System.EventArgs eventArgs)
    {
        OnApply(sender, eventArgs);
    }

    private void CancelToolbarItem_Clicked(object sender, System.EventArgs eventArgs)
    {
        OnCancel(sender, eventArgs);
    }

    private void Picker_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        hasChangesFlag = true;
    }
}