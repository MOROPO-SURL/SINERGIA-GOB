namespace MOROPO.SINETGIAGOB.GovCamaguey.Interop.API.Common.Models.Display
{
    public class ActionMonthlyClosureDisplayModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityDisplayModel<GovCamaguey.Persistence.Entities.ActionMonthlyClosure, System.Guid>
    {
        public System.Guid Id { get; set; }

        public System.DateTime At { get; set; }

        public void Import(GovCamaguey.Persistence.Entities.ActionMonthlyClosure entity)
        {
            Id = entity.Id;
            At = entity.At;
        }
    }
}
