using FluentValidation;
using MOROPO.SINETGIAGOB.GovCamaguey.Terminal.Models.Page.Configuration;

namespace MOROPO.SINETGIAGOB.GovCamaguey.Terminal.Validators;

public sealed class CommonIndicatorResultConfigurationValidator : AbstractValidator<CommonIndicatorResultConfigurationPageModel>
{
    public CommonIndicatorResultConfigurationValidator()
    {
        RuleFor(model => model.Description)
            .MaximumLength(2048)
            .WithName("Descripción");
    }
}
