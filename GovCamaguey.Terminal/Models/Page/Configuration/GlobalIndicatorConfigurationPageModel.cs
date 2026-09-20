using System.Collections.ObjectModel;
using System.Linq;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Terminal.Models.Page.Configuration;

public class GlobalIndicatorConfigurationPageModel : Core.Presentation.Terminal.MAUI.Models.Page.Contract.AbstractConfigurationPageModel<Interop.API.Common.Models.Display.GlobalIndicatorDisplayModel>
{
    public static string NameValidableProperty => nameof(Name);
    public static string AmountValidableProperty => nameof(Amount);
    public static string DescriptionValidableProperty => nameof(Description);
    public static string UnitMeasurementValidableProperty => nameof(UnitMeasurement);

    private string name;
    private string description;
    private int priority = 1;
    private double amount;
    private Interop.API.Common.Models.Display.UnitMeasurementDisplayModel unitMeasurement;

    public GlobalIndicatorConfigurationPageModel() : this(null) { }

    public GlobalIndicatorConfigurationPageModel(Interop.API.Common.Models.Display.GlobalIndicatorDisplayModel model) : base(model)
    {
        if (model != null)
        {
            Id = model.Id;
            Priority = model.Priority;
            Name = model.Name;
            Description = model.Description;
            Amount = model.Amount;
            UnitMeasurementId = model.UnitMeasurement.Id;
            MontlyPlans = new ObservableCollection<Interop.API.Common.Models.Display.GlobalIndicatorMontlyPlanDisplayModel>(model.MontlyPlans.OrderBy(_model => _model.Year * 100 + _model.Month));
        }
    }

    public System.Guid Id { get; set; }

    public int Priority { get => priority; set => SetProperty(ref priority, value); }

    public string Name { get => name; set => SetProperty(ref name, value); }

    public string Description { get => description; set => SetProperty(ref description, value); }

    public double Amount { get => amount; set => SetProperty(ref amount, value); }

    public System.Guid? UnitMeasurementId { get; set; }

    public Interop.API.Common.Models.Display.UnitMeasurementDisplayModel UnitMeasurement { get => unitMeasurement; set => SetProperty(ref unitMeasurement, value); }

    public ObservableCollection<Interop.API.Common.Models.Display.GlobalIndicatorMontlyPlanDisplayModel> MontlyPlans { get; set; } = [];

    public Interop.API.Common.Models.Display.UnitMeasurementDisplayModel[] AvailableUnitMeasurements { get; set; }
}
