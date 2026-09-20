namespace MOROPO.SINERGIAGOB.GovCamaguey.Persistence.Entities
{
    public class ProyectionGuidelineBridge : Framework.Guama.Core.Persistence.Entities.Contract.IBridge<Proyection, System.Guid, Guideline, System.Guid>
    {
        public System.Guid ProwId { get; set; }
        public Proyection Prow { get; set; }

        public System.Guid SternId { get; set; }
        public Guideline Stern { get; set; }
    }
}
