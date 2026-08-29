using FluentValidation;

namespace MOROPO.SINETGIAGOB.GovCamaguey.Terminal.Validators;

public class ObjectiveConfigurationValidator : AbstractValidator<Models.Page.Configuration.ObjectiveConfigurationPageModel>
{
    public ObjectiveConfigurationValidator()
    {
        RuleFor(model => model.Responsible)
            .NotNull()
            .WithName("Responsable");

        RuleFor(model => model.Name)
            .NotEmpty()
            .MaximumLength(1024)
            .WithName("Nombre");

        RuleFor(model => model.Index)
            .NotNull()
            .GreaterThan(0)
            .WithName("Número");

        RuleFor(entity => entity.Description)
            .MaximumLength(5120)
            .WithName("Descripción");
    }
}
