using MOROPO.SINETGIAGOB.GovCamaguey.Interop.API.Common.Models.Input;

namespace MOROPO.SINETGIAGOB.GovCamaguey.Terminal.UI.Pages.Configuration;

public partial class ObjectiveIndicatorMontlyPlanConfigurationPage : Core.Presentation.Terminal.MAUI.UI.Pages.Base.ConfigurationBaseContentPage<ObjectiveIndicatorMontlyPlanInputModel, ObjectiveIndicatorMontlyPlanInputModel, Models.Page.Configuration.ObjectiveIndicatorMontlyPlanConfigurationPageModel>
{
    public ObjectiveIndicatorMontlyPlanConfigurationPage(System.Action<ObjectiveIndicatorMontlyPlanInputModel> successIndicator, Models.Page.Configuration.ObjectiveIndicatorMontlyPlanConfigurationPageModel model) : base(successIndicator, model, new Validators.ObjectiveIndicatorMontlyPlanConfigurationValidator())
    {
        InitializeComponent();
    }

    protected override string EntityTypeName => "Plan de Indicador";

    protected override System.Threading.Tasks.Task<ObjectiveIndicatorMontlyPlanInputModel> PerformCreationAsync()
    {
        return System.Threading.Tasks.Task.FromResult(new ObjectiveIndicatorMontlyPlanInputModel
        {
            Id = System.Guid.NewGuid(),
            Month = ConcreteBindingContext.YearMonth.Value,
            Year = ConcreteBindingContext.YearMonth.Key,
            Amount = ConcreteBindingContext.Amount
        });
    }

    protected override System.Threading.Tasks.Task<ObjectiveIndicatorMontlyPlanInputModel> PerformEditionAsync()
    {
        return System.Threading.Tasks.Task.FromResult(new ObjectiveIndicatorMontlyPlanInputModel
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