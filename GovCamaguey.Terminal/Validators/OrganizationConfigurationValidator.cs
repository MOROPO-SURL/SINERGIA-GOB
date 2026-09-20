using FluentValidation;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Terminal.Validators
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
