namespace MOROPO.SINETGIAGOB.GovCamaguey.Persistence.Entities
{
    public class ProyectionMonthlyClosure : Framework.Guama.Core.Persistence.Entities.Contract.IEntity<System.Guid>
    {
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey(nameof(ProyectionId))]
        public Proyection Proyection { get; set; }
        public System.Guid ProyectionId { get; set; }

        public System.DateTime At { get; set; }
    }
}
