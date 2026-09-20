using FluentValidation;
using MOROPO.SINERGIAGOB.GovCamaguey.Terminal.Models.Page.Configuration;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Terminal.Validators;

public sealed class CommonIndicatorResultConfigurationValidator : AbstractValidator<CommonIndicatorResultConfigurationPageModel>
{
    public CommonIndicatorResultConfigurationValidator()
    {
        RuleFor(model => model.Description)
            .MaximumLength(2048)
            .WithName("Descripción");
    }
}
