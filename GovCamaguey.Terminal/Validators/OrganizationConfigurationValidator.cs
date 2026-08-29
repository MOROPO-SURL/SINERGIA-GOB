using FluentValidation;

namespace MOROPO.SINETGIAGOB.GovCamaguey.Terminal.Validators
{
    public class OrganizationConfigurationValidator : AbstractValidator<Models.Page.Configuration.OrganizationConfigurationPageModel>
    {
        public OrganizationConfigurationValidator()
        {
            RuleFor(model => model.Name)
                .NotEmpty()
                .WithName("Nombre");
        }
    }
}
