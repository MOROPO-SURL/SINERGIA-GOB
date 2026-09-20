namespace MOROPO.SINERGIAGOB.GovCamaguey.Persistence.Entities
{
    public class ProyectionOrganizationResponsabilityBridge : Framework.Guama.Core.Persistence.Entities.Contract.IBridge<Proyection, System.Guid, Organization, System.Guid>
    {
        public System.Guid ProwId { get; set; }
        public Proyection Prow { get; set; }

        public System.Guid SternId { get; set; }
        public Organization Stern { get; set; }
    }
}
