using FluentValidation;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Terminal.Validators;

public class ProyectionIndicatorMontlyPlanConfigurationValidator : AbstractValidator<Models.Page.Configuration.ProyectionIndicatorMontlyPlanConfigurationPageModel>
{
    public ProyectionIndicatorMontlyPlanConfigurationValidator()
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
