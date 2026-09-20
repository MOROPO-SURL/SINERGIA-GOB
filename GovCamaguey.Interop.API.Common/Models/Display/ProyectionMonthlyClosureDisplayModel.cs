namespace MOROPO.SINERGIAGOB.GovCamaguey.Interop.API.Common.Models.Display
{
    public class ProyectionMonthlyClosureDisplayModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityDisplayModel<Persistence.Entities.ProyectionMonthlyClosure, System.Guid>
    {
        public System.Guid Id { get; set; }

        public System.DateTime At { get; set; }

        public void Import(Persistence.Entities.ProyectionMonthlyClosure entity)
        {
            Id = entity.Id;
            At = entity.At;
        }
    }
}
