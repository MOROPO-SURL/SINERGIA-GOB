using FluentValidation;

namespace MOROPO.SINETGIAGOB.GovCamaguey.Terminal.Validators;

public class GlobalIndicatorMontlyPlanConfigurationValidator : AbstractValidator<Models.Page.Configuration.GlobalIndicatorMontlyPlanConfigurationPageModel>
{
    public GlobalIndicatorMontlyPlanConfigurationValidator()
    {
        RuleFor(model => model.YearMonth)
            .NotNull()
            .WithName("Mes / Año");

        RuleFor(model => model.Amount)
            .NotNull()
            .GreaterThan(0)
            .WithName("Meta");
    }
}
