namespace MOROPO.SINETGIAGOB.GovCamaguey.Persistence.Entities
{
    public class ActionMonthlyClosure : Framework.Guama.Core.Persistence.Entities.Contract.IEntity<System.Guid>
    {
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey(nameof(ActionId))]
        public Action Action { get; set; }
        public System.Guid ActionId { get; set; }

        public System.DateTime At { get; set; }
    }
}
