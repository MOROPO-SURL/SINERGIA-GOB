namespace MOROPO.SINETGIAGOB.GovCamaguey.Interop.API.Common.Models.Input.Contract
{
    public abstract class AbstractIndicatorResultInputModel<TModel> : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityInputModel<TModel, long>
        where TModel : GovCamaguey.Persistence.Entities.Contract.AbstractIndicatorResult
    {
        public long Id { get; set; }

        public System.DateTime At { get; set; }

        public double Amount { get; set; }

        [System.ComponentModel.DataAnnotations.MaxLength(2048)]
        public string Description { get; set; }

        public abstract TModel Export();

        public void Import(TModel entity)
        {
            throw new System.NotImplementedException();
        }

        public virtual void Mergue(TModel entity)
        {
            entity.At = At;
            entity.Amount = Amount;
            entity.Description = Description;
        }
    }
}
