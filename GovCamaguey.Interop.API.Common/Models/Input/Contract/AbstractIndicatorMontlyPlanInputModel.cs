namespace MOROPO.SINERGIAGOB.GovCamaguey.Interop.API.Common.Models.Input.Contract
{
    public abstract class AbstractIndicatorMontlyPlanInputModel<TEntity> : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityInputModel<TEntity, System.Guid> where TEntity : GovCamaguey.Persistence.Entities.Contract.AbstractIndicatorMontlyPlan
    {
        public System.Guid Id { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public double Amount { get; set; }

        public abstract TEntity Export();

        public void Import(TEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public virtual void Mergue(TEntity entity)
        {
            entity.Month = Month;
            entity.Year = Year;
            entity.Amount = Amount;
        }
    }
}
