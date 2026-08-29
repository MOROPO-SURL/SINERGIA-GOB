using FluentValidation;

namespace MOROPO.SINETGIAGOB.GovCamaguey.Terminal.Validators;

public class ObjectiveIndicatorMontlyPlanConfigurationValidator : AbstractValidator<Models.Page.Configuration.ObjectiveIndicatorMontlyPlanConfigurationPageModel>
{
    public ObjectiveIndicatorMontlyPlanConfigurationValidator()
    {
        RuleFor(model => model.YearMonth)
            .Must(model => model.Key != 0)
            .WithName("Mes/Año");

        RuleFor(model => model.Amount)
            .NotNull()
            .GreaterThan(0)
            .WithName("Meta");
    }
}
