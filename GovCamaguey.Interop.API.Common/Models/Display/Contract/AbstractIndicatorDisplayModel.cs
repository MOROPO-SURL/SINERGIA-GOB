using System.Linq;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Interop.API.Common.Models.Display.Contract
{
    public abstract class AbstractIndicatorDisplayModel<TEntity> : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityDisplayModel<TEntity, System.Guid> where TEntity : GovCamaguey.Persistence.Entities.Contract.AbstractIndicator
    {
        public System.Guid Id { get; set; }

        public bool Enabled { get; set; }

        public int Priority { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public UnitMeasurementDisplayModel UnitMeasurement { get; set; }

        public double Amount { get; set; }

        public double Balance { get; set; }
        public System.DateTime? ResultAt { get; set; }
        public string Context { get; set; }

        public virtual void Import(TEntity entity)
        {
            Id = entity.Id;
            Enabled = entity.Enabled;
            Priority = entity.Priority;
            Name = entity.Name;
            Description = entity.Description;
            Amount = entity.Amount;

            if (entity.UnitMeasurement != null)
            {
                UnitMeasurement = new UnitMeasurementDisplayModel();

                UnitMeasurement.Import(entity.UnitMeasurement);
            }
        }
    }
}
