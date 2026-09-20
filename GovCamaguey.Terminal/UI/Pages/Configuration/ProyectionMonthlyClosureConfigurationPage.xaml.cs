using MOROPO.SINERGIAGOB.GovCamaguey.Interop.API.Common.Models.Input;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Terminal.UI.Pages.Configuration;

public partial class ProyectionMonthlyClosureConfigurationPage : Core.Presentation.Terminal.MAUI.UI.Pages.Base.ConfigurationBaseContentPage<ProyectionMonthlyClosureInputModel, ProyectionMonthlyClosureInputModel, Models.Page.Configuration.ProyectionMonthlyClosureConfigurationPageModel>
{
    public ProyectionMonthlyClosureConfigurationPage(System.Action<ProyectionMonthlyClosureInputModel> successAction, Models.Page.Configuration.ProyectionMonthlyClosureConfigurationPageModel model) : base(successAction, model, null)
    {
        InitializeComponent();

        hasChangesFlag = true;
    }

    protected override string EntityTypeName => "Control de Cumplimiento de Obj. Específico";

    protected override System.Threading.Tasks.Task<ProyectionMonthlyClosureInputModel> PerformCreationAsync()
    {
        return System.Threading.Tasks.Task.FromResult(new ProyectionMonthlyClosureInputModel
        {
            Id = System.Guid.NewGuid(),
            At = ConcreteBindingContext.At
        });
    }

    protected override System.Threading.Tasks.Task<ProyectionMonthlyClosureInputModel> PerformEditionAsync()
    {
        return System.Threading.Tasks.Task.FromResult(new ProyectionMonthlyClosureInputModel
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