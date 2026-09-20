namespace MOROPO.SINERGIAGOB.GovCamaguey.Persistence.Entities
{
    public class ObjectiveUserBridge : Framework.Guama.Core.Persistence.Entities.Contract.IBridge<Objective, System.Guid, Framework.Guama.Security.Personal.Persistence.Entities.User, string>
    {
        public System.Guid ProwId { get; set; }
        public Objective Prow { get; set; }

        public string SternId { get; set; }
        public Framework.Guama.Security.Personal.Persistence.Entities.User Stern { get; set; }

        public bool IsProyectionsIncluded { get; set; }

        public bool IsActionsIncluded { get; set; }
    }
}
