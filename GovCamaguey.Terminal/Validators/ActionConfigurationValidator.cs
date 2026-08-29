using FluentValidation;

namespace MOROPO.SINETGIAGOB.GovCamaguey.Terminal.Validators
{
    public class ActionConfigurationValidator : AbstractValidator<Models.Page.Configuration.ActionConfigurationPageModel>
    {
        public ActionConfigurationValidator()
        {
            RuleFor(model => model.Name)
                .NotEmpty()
                .MaximumLength(1024)
                .WithName("Nombre");

            RuleFor(model => model.Index)
                .NotNull()
                .GreaterThan(0)
                .WithName("Número");

            RuleFor(model => model.ResponsabilityOrganizations)
                .NotEmpty()
                .WithName("Responsables");

            RuleFor(entity => entity.Description)
                .MaximumLength(5120)
                .WithName("Descripción");
        }
    }
}
