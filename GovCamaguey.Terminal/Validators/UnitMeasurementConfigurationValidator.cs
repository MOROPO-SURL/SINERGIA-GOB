using FluentValidation;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Terminal.Validators
{
    public class UnitMeasurementConfigurationValidator : AbstractValidator<Models.Page.Configuration.UnitMeasurementConfigurationPageModel>
    {
        public UnitMeasurementConfigurationValidator()
        {
            RuleFor(model => model.Name)
                .NotEmpty()
                .MaximumLength(255)
                .WithName("Nombre");

            RuleFor(model => model.Symbol)
                .NotNull()
                .MaximumLength(10)
                .WithName("Símbolo");
        }
    }
}
