namespace MOROPO.SINERGIAGOB.Core.Interop.API.Common.Models.Input.Contract
{
    public abstract class AbstractNotificableEntityInputModel<TEntity, TId> : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityInputModel<TEntity, TId> where TEntity : class, Framework.Guama.Core.Persistence.Entities.Contract.IEntity<TId>
    {
        public TId Id { get; set; }

        public string[] NotificationUserIds { get; set; }

        public abstract TEntity Export();

        public void Import(TEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public abstract void Mergue(TEntity entity);
    }
}