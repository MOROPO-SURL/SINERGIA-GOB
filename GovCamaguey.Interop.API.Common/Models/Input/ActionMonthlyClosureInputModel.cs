namespace MOROPO.SINETGIAGOB.GovCamaguey.Interop.API.Common.Models.Input
{
    public class ActionMonthlyClosureInputModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityInputModel<GovCamaguey.Persistence.Entities.ActionMonthlyClosure, System.Guid>
    {
        public System.Guid Id { get; set; }

        public System.DateTime At { get; set; }

        public GovCamaguey.Persistence.Entities.ActionMonthlyClosure Export()
        {
            var entity = new GovCamaguey.Persistence.Entities.ActionMonthlyClosure { Id = Id };

            Mergue(entity);

            return entity;
        }

        public void Import(GovCamaguey.Persistence.Entities.ActionMonthlyClosure entity)
        {
            throw new System.NotImplementedException();
        }

        public void Mergue(GovCamaguey.Persistence.Entities.ActionMonthlyClosure entity)
        {
            entity.At = At;
        }
    }
}
