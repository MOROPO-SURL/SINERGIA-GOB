using FluentValidation;

namespace MOROPO.SINETGIAGOB.GovCamaguey.Terminal.Validators
{
    public class ProyectionConfigurationValidator : AbstractValidator<Models.Page.Configuration.ProyectionConfigurationPageModel>
    {
        public ProyectionConfigurationValidator()
        {
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

            RuleFor(model => model.ResponsabilityOrganizations)
                .NotEmpty()
                .WithName("Responsables");
        }
    }
}
