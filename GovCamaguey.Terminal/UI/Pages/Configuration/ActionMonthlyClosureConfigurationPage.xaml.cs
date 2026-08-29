using MOROPO.SINETGIAGOB.GovCamaguey.Interop.API.Common.Models.Input;

namespace MOROPO.SINETGIAGOB.GovCamaguey.Terminal.UI.Pages.Configuration;

public partial class ActionMonthlyClosureConfigurationPage : Core.Presentation.Terminal.MAUI.UI.Pages.Base.ConfigurationBaseContentPage<ActionMonthlyClosureInputModel, ActionMonthlyClosureInputModel, Models.Page.Configuration.ActionMonthlyClosureConfigurationPageModel>
{
    public ActionMonthlyClosureConfigurationPage(System.Action<ActionMonthlyClosureInputModel> successAction, Models.Page.Configuration.ActionMonthlyClosureConfigurationPageModel model) : base(successAction, model, null)
    {
        InitializeComponent();

        hasChangesFlag = true;
    }

    protected override string EntityTypeName => "Control de Cumplimiento de Acción";

    protected override System.Threading.Tasks.Task<ActionMonthlyClosureInputModel> PerformCreationAsync()
    {
        return System.Threading.Tasks.Task.FromResult(new ActionMonthlyClosureInputModel
        {
            Id = System.Guid.NewGuid(),
            At = ConcreteBindingContext.At
        });
    }

    protected override System.Threading.Tasks.Task<ActionMonthlyClosureInputModel> PerformEditionAsync()
    {
        return System.Threading.Tasks.Task.FromResult(new ActionMonthlyClosureInputModel
        {
            Id = ConcreteBindingContext.Id,
            At = ConcreteBindingContext.At
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
}