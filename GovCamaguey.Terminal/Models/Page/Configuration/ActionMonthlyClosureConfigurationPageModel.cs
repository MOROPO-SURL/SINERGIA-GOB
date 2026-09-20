namespace MOROPO.SINERGIAGOB.GovCamaguey.Terminal.Models.Page.Configuration
{
    public class ActionMonthlyClosureConfigurationPageModel : Core.Presentation.Terminal.MAUI.Models.Page.Contract.AbstractConfigurationPageModel<Interop.API.Common.Models.Input.ActionMonthlyClosureInputModel>
    {
        private System.DateTime at = System.DateTime.Now;

        public ActionMonthlyClosureConfigurationPageModel(Interop.API.Common.Models.Input.ActionMonthlyClosureInputModel model) : base(model)
        {
            if (model != null)
            {
                Id = model.Id;
                at = model.At;
            }
        }

        public System.Guid Id { get; set; }

        public System.DateTime At { get => at; set => SetProperty(ref at, value); }
    }
}
