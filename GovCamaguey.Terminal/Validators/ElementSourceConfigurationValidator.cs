using FluentValidation;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Terminal.Validators;

public class ElementSourceConfigurationValidator : AbstractValidator<Models.Page.Configuration.ElementSourceConfigurationPageModel>
{
    public ElementSourceConfigurationValidator()
    {
        RuleFor(model => model.Name)
            .NotEmpty()
            .MaximumLength(255)
            .WithName("Nombre");
    }
}