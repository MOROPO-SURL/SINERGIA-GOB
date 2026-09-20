using MOROPO.SINERGIAGOB.Core.Persistence.ORM.EF.Configurators.Base;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Web.BackOffice.Persistence.ORM.EF.Providers
{
    public class ApplicationDbSchemaConfigurationProvider : Framework.Guama.Core.Persistence.ORM.EF.Providers.Facade.IDbSchemaConfigurationProviderFacade
    {
        public System.Collections.Generic.ICollection<Framework.Guama.Core.Persistence.ORM.EF.Configurators.Contract.IEntityTypeConfigurator> EntityTypeConfigurators
        {
            get
            {
                return
                [
                    new Configurators.PersonEntityTypeConfigurator(),
                    new Configurators.OrganizationEntityTypeConfigurator(),
                    new Configurators.ObjectiveEntityTypeConfigurator(),
                    new Configurators.ObjectiveUserBridgeEntityTypeConfigurator(),
                    new Configurators.ProyectionEntityTypeConfigurator(),
                    new Configurators.ProyectionUserBridgeEntityTypeConfigurator(),
                    new Configurators.ProyectionGuidelineBridgeEntityTypeConfigurator(),
                    new Configurators.ProyectionOrganizationResponsabilityBridgeEntityTypeConfigurator(),
                    new Configurators.ProyectionOrganizationParticipationBridgeEntityTypeConfigurator(),
                    new Configurators.ActionEntityTypeConfigurator(),
                    new Configurators.ActionUserBridgeEntityTypeConfigurator(),
                    new Configurators.ActionOrganizationResponsabilityBridgeEntityTypeConfigurator(),
                    new Configurators.ActionOrganizationParticipationBridgeEntityTypeConfigurator(),
                    new Configurators.GuidelineEntityTypeConfigurator(),
                    new Configurators.ActionIndicatorEntityTypeConfigurator(),
                    new BaseEntityTypeConfigurator<GovCamaguey.Persistence.Entities.ActionIndicatorMontlyPlan>(),
                    new BaseEntityTypeConfigurator<GovCamaguey.Persistence.Entities.ActionIndicatorResult>(),
                    new Configurators.ProyectionIndicatorEntityTypeConfigurator(),
                    new BaseEntityTypeConfigurator<GovCamaguey.Persistence.Entities.ProyectionIndicatorMontlyPlan>(),
                    new BaseEntityTypeConfigurator<GovCamaguey.Persistence.Entities.ProyectionIndicatorResult>(),
                    new Configurators.ObjectiveIndicatorEntityTypeConfigurator(),
                    new BaseEntityTypeConfigurator<GovCamaguey.Persistence.Entities.ObjectiveIndicatorMontlyPlan>(),
                    new BaseEntityTypeConfigurator<GovCamaguey.Persistence.Entities.ObjectiveIndicatorResult>(),
                    new Configurators.GlobalIndicatorEntityTypeConfigurator(),
                    new BaseEntityTypeConfigurator<GovCamaguey.Persistence.Entities.GlobalIndicatorMontlyPlan>(),
                    new BaseEntityTypeConfigurator<GovCamaguey.Persistence.Entities.GlobalIndicatorResult>()
                ];
            }
        }

        public string DefaultPersistenceSchema => null;
    }
}
