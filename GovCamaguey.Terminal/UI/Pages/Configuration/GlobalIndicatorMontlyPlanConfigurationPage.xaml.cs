using MOROPO.SINETGIAGOB.GovCamaguey.Interop.API.Common.Models.Input;

namespace MOROPO.SINETGIAGOB.GovCamaguey.Terminal.UI.Pages.Configuration;

public partial class GlobalIndicatorMontlyPlanConfigurationPage : Core.Presentation.Terminal.MAUI.UI.Pages.Base.ConfigurationBaseContentPage<GlobalIndicatorMontlyPlanInputModel, GlobalIndicatorMontlyPlanInputModel, Models.Page.Configuration.GlobalIndicatorMontlyPlanConfigurationPageModel>
{
    public GlobalIndicatorMontlyPlanConfigurationPage(System.Action<GlobalIndicatorMontlyPlanInputModel> successIndicator, Models.Page.Configuration.GlobalIndicatorMontlyPlanConfigurationPageModel model) : base(successIndicator, model, new Validators.GlobalIndicatorMontlyPlanConfigurationValidator())
    {
        InitializeComponent();
    }

    protected override string EntityTypeName => "Plan de Indicador";

    protected override System.Threading.Tasks.Task<GlobalIndicatorMontlyPlanInputModel> PerformCreationAsync()
    {
        return System.Threading.Tasks.Task.FromResult(new GlobalIndicatorMontlyPlanInputModel
        {
            Id = System.Guid.NewGuid(),
            Month = ConcreteBindingContext.YearMonth.Value,
            Year = ConcreteBindingContext.YearMonth.Key,
            Amount = ConcreteBindingContext.Amount
        });
    }

    protected override System.Threading.Tasks.Task<GlobalIndicatorMontlyPlanInputModel> PerformEditionAsync()
    {
        return System.Threading.Tasks.Task.FromResult(new GlobalIndicatorMontlyPlanInputModel
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