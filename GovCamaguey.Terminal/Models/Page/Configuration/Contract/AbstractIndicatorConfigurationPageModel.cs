using System.Collections.ObjectModel;
using System.Linq;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Terminal.Models.Page.Configuration.Contract;

public abstract class AbstractIndicatorConfigurationPageModel<TIndicatorEntity, TIndicatorInputModel, TIndicatorMontlyPlanEntity, TIndicatorMontlyPlanDisplayModel> : Core.Presentation.Terminal.MAUI.Models.Page.Contract.AbstractConfigurationPageModel<TIndicatorInputModel>
    where TIndicatorEntity : GovCamaguey.Persistence.Entities.Contract.AbstractIndicator
    where TIndicatorInputModel : Interop.API.Common.Models.Input.Contract.AbstractIndicatorInputModel<TIndicatorEntity>
    where TIndicatorMontlyPlanEntity : GovCamaguey.Persistence.Entities.Contract.AbstractIndicatorMontlyPlan
    where TIndicatorMontlyPlanDisplayModel : Interop.API.Common.Models.Display.Contract.AbstractIndicatorMontlyPlanDisplayModel<TIndicatorMontlyPlanEntity>
{
    private string name;
    private string description;
    private int priority = 1;
    private double amount;
    private Interop.API.Common.Models.Display.UnitMeasurementDisplayModel unitMeasurement;

    public AbstractIndicatorConfigurationPageModel() : this(null) { }

    public AbstractIndicatorConfigurationPageModel(TIndicatorInputModel model, TIndicatorMontlyPlanDisplayModel[] montlyPlans = null) : base(model)
    {
        if (model != null)
        {
            Id = model.Id;
            Priority = model.Priority;
            Name = model.Name;
            Description = model.Description;
            Amount = model.Amount;
            UnitMeasurementId = model.UnitMeasurementId;

            if (montlyPlans != null)
            {
                MontlyPlans = new ObservableCollection<TIndicatorMontlyPlanDisplayModel>(montlyPlans.OrderBy(_model => _model.Year * 100 + _model.Month));
            }
        }
    }

    public System.Guid Id { get; set; }

    public int Priority { get => priority; set => SetProperty(ref priority, value); }

    public string Name { get => name; set => SetProperty(ref name, value); }

    public string Description { get => description; set => SetProperty(ref description, value); }

    public double Amount { get => amount; set => SetProperty(ref amount, value); }

    public System.Guid? UnitMeasurementId { get; set; }

    public Interop.API.Common.Models.Display.UnitMeasurementDisplayModel UnitMeasurement { get => unitMeasurement; set => SetProperty(ref unitMeasurement, value); }

    public ObservableCollection<TIndicatorMontlyPlanDisplayModel> MontlyPlans { get; set; } = [];

    public Interop.API.Common.Models.Display.UnitMeasurementDisplayModel[] AvailableUnitMeasurements { get; set; }
}
