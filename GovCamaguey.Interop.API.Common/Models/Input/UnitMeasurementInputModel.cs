namespace MOROPO.SINERGIAGOB.GovCamaguey.Interop.API.Common.Models.Input
{
    public class UnitMeasurementInputModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityInputModel<GovCamaguey.Persistence.Entities.UnitMeasurement, System.Guid>
    {
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.MaxLength(255)]
        public string Name { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.MaxLength(10)]
        public string Symbol { get; set; }

        public int Priority { get; set; }

        public GovCamaguey.Persistence.Entities.UnitMeasurement Export()
        {
            var entity = new GovCamaguey.Persistence.Entities.UnitMeasurement
            {
                Id = Id,
                Enabled = true
            };

            Mergue(entity);

            return entity;
        }

        public void Import(GovCamaguey.Persistence.Entities.UnitMeasurement entity)
        {
            throw new System.NotImplementedException();
        }

        public void Mergue(GovCamaguey.Persistence.Entities.UnitMeasurement entity)
        {
            entity.Name = Name;
            entity.Symbol = Symbol;
            entity.Priority = Priority;
        }
    }
}
