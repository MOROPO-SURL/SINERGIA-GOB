namespace MOROPO.SINETGIAGOB.GovCamaguey.Persistence.Entities
{
    public class ActionOrganizationResponsabilityBridge : Framework.Guama.Core.Persistence.Entities.Contract.IBridge<Action, System.Guid, Organization, System.Guid>
    {
        public System.Guid ProwId { get; set; }
        public Action Prow { get; set; }

        public System.Guid SternId { get; set; }
        public Organization Stern { get; set; }
    }
}
