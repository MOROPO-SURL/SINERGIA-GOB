namespace MOROPO.SINERGIAGOB.GovCamaguey.Web.BackOffice.Persistence.ORM.EF.Configurators
{
    public class ObjectiveEntityTypeConfigurator : Framework.Guama.Core.Persistence.ORM.EF.Configurators.Contract.AbstractEntityTypeConfigurator<GovCamaguey.Persistence.Entities.Objective>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<GovCamaguey.Persistence.Entities.Objective> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasOne(entity => entity.Responsible).WithMany().HasForeignKey(entity => entity.ResponsibleId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);

            entityTypeBuilder.HasIndex(entity => entity.Index).IsUnique();
        }
    }
}
