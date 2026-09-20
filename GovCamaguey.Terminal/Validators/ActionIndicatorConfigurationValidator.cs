using FluentValidation;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Terminal.Validators;

public class ActionIndicatorConfigurationValidator : AbstractValidator<Models.Page.Configuration.ActionIndicatorConfigurationPageModel>
{
    public ActionIndicatorConfigurationValidator()
    {
        RuleFor(model => model.Name)
            .NotEmpty()
            .MaximumLength(255)
            .WithName("Nombre");

        RuleFor(model => model.Amount)
            .NotNull()
            .GreaterThan(0)
            .WithName("Cantidad");

        RuleFor(entity => entity.Description)
            .MaximumLength(1024)
            .WithName("Descripción");

        RuleFor(entity => entity.UnitMeasurement)
            .NotNull()
            .WithName("Unidad de medida");
    }
}
