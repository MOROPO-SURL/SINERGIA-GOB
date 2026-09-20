namespace MOROPO.SINERGIAGOB.GovCamaguey.Persistence.Entities
{
    public class Person : Core.Persistence.Entities.Contract.IFullFeatureEntity<System.Guid>
    {
        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; } = System.Guid.NewGuid();

        public bool Archived { get; set; }

        public bool Enabled { get; set; }

        public int Priority { get; set; }

        public Framework.Guama.Identity.Persistence.Entities.PersonIdentity Identity { get; set; }
        public System.Guid IdentityId { get; set; }

        [System.ComponentModel.DataAnnotations.MaxLength(255)]
        public string Role { get; set; }
    }
}