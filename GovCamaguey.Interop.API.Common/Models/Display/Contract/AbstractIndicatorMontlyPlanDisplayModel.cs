namespace MOROPO.SINETGIAGOB.GovCamaguey.Interop.API.Common.Models.Display.Contract
{
    public abstract class AbstractIndicatorMontlyPlanDisplayModel<TEntity> : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityDisplayModel<TEntity, System.Guid> where TEntity : GovCamaguey.Persistence.Entities.Contract.AbstractIndicatorMontlyPlan
    {
        public System.Guid Id { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public double Amount { get; set; }

        public void Import(TEntity entity)
        {
            Id = entity.Id;
            Month = entity.Month;
            Year = entity.Year;
            Amount = entity.Amount;
        }

        public override string ToString()
        {
            return $"{Constants.Globals.MONTHS[Month - 1]} / {Year} => {Amount}";
        }
    }
}
