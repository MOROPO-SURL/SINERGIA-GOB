namespace MOROPO.SINETGIAGOB.GovCamaguey.Persistence.Entities
{
    public class ProyectionUserBridge : Framework.Guama.Core.Persistence.Entities.Contract.IBridge<Proyection, System.Guid, Framework.Guama.Security.Personal.Persistence.Entities.User, string>
    {
        public System.Guid ProwId { get; set; }
        public Proyection Prow { get; set; }

        public string SternId { get; set; }
        public Framework.Guama.Security.Personal.Persistence.Entities.User Stern { get; set; }

        public bool IsActionsIncluded { get; set; }
    }
}
