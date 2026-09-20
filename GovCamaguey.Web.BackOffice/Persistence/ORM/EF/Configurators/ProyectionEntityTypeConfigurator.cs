namespace MOROPO.SINERGIAGOB.GovCamaguey.Web.BackOffice.Persistence.ORM.EF.Configurators
{
    public class ProyectionEntityTypeConfigurator : Framework.Guama.Core.Persistence.ORM.EF.Configurators.Contract.AbstractEntityTypeConfigurator<GovCamaguey.Persistence.Entities.Proyection>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<GovCamaguey.Persistence.Entities.Proyection> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasIndex(entity => new { entity.Index, entity.ObjectiveId }).IsUnique();
        }
    }
}
