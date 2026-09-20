namespace MOROPO.SINERGIAGOB.GovCamaguey.Interop.API.Common.Models.Display.Contract
{
    public abstract class AbstractIndicatorResultDisplayModel<TModel> : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityDisplayModel<TModel, long>
        where TModel : GovCamaguey.Persistence.Entities.Contract.AbstractIndicatorResult
    {
        public long Id { get; set; }

        public System.DateTime At { get; set; }

        public double Amount { get; set; }

        public string Description { get; set; }

        public virtual void Import(TModel entity)
        {
            Id = entity.Id;
            At = entity.At;
            Amount = entity.Amount;
            Description = entity.Description;
        }
    }
}
