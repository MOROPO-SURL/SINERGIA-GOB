namespace MOROPO.SINETGIAGOB.GovCamaguey.Persistence.Entities
{
    public class ActionUserBridge : Framework.Guama.Core.Persistence.Entities.Contract.IBridge<Action, System.Guid, Framework.Guama.Security.Personal.Persistence.Entities.User, string>
    {
        public System.Guid ProwId { get; set; }
        public Action Prow { get; set; }

        public string SternId { get; set; }
        public Framework.Guama.Security.Personal.Persistence.Entities.User Stern { get; set; }
    }
}
