namespace MOROPO.SINETGIAGOB.GovCamaguey.Interop.API.Common.Models.Input.Contract
{
    public abstract class AbstractIndicatorInputModel<TEntity> : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityInputModel<TEntity, System.Guid> where TEntity : GovCamaguey.Persistence.Entities.Contract.AbstractIndicator
    {
        public System.Guid Id { get; set; }

        public int Priority { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.MaxLength(255)]
        public string Name { get; set; }

        [System.ComponentModel.DataAnnotations.MaxLength(1024)]
        public string Description { get; set; }

        public System.Guid UnitMeasurementId { get; set; }

        public double Amount { get; set; }

        public abstract TEntity Export();

        public void Import(TEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public virtual void Mergue(TEntity entity)
        {
            entity.Priority = Priority;
            entity.Name = Name;
            entity.Description = Description;
            entity.Amount = Amount;

            if (entity.UnitMeasurementId != UnitMeasurementId)
            {
                entity.UnitMeasurement = null;
                entity.UnitMeasurementId = UnitMeasurementId;
            }
        }
    }
}
