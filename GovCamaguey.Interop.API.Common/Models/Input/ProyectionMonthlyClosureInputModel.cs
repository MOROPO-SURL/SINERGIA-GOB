namespace MOROPO.SINERGIAGOB.GovCamaguey.Interop.API.Common.Models.Input
{
    public class ProyectionMonthlyClosureInputModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityInputModel<Persistence.Entities.ProyectionMonthlyClosure, System.Guid>
    {
        public System.Guid Id { get; set; }

        public System.DateTime At { get; set; }

        public Persistence.Entities.ProyectionMonthlyClosure Export()
        {
            var entity = new Persistence.Entities.ProyectionMonthlyClosure { Id = Id };

            Mergue(entity);

            return entity;
        }

        public void Import(Persistence.Entities.ProyectionMonthlyClosure entity)
        {
            throw new System.NotImplementedException();
        }

        public void Mergue(Persistence.Entities.ProyectionMonthlyClosure entity)
        {
            entity.At = At;
        }
    }
}
