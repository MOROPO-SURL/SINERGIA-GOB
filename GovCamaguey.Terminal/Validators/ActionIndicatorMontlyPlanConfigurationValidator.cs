using FluentValidation;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Terminal.Validators;

public class ActionIndicatorMontlyPlanConfigurationValidator : AbstractValidator<Models.Page.Configuration.ActionIndicatorMontlyPlanConfigurationPageModel>
{
    public ActionIndicatorMontlyPlanConfigurationValidator()
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
