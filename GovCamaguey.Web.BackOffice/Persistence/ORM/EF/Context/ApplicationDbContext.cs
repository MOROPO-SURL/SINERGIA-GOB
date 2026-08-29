using Microsoft.EntityFrameworkCore;
using MOROPO.Framework.Guama.Core.Security.Common.Persistence.Entities;
using MOROPO.Framework.Guama.Identity.Persistence.Entities;
using MOROPO.Framework.Guama.Location.Persistence.Entities;
using MOROPO.Framework.Guama.Media.Persistence.Entities;
using MOROPO.Framework.Guama.Security.Personal.Persistence.Entities;
using MOROPO.SINETGIAGOB.GovCamaguey.Persistence.Entities;

namespace MOROPO.SINETGIAGOB.GovCamaguey.Web.BackOffice.Persistence.ORM.EF.Context
{
    public class ApplicationDbContext :
        Framework.Guama.Core.Persistence.ORM.EF.Context.Contract.AbstractTraceableDbContext,
        Framework.Guama.Core.Security.Common.Persistence.ORM.EF.Contract.IModulePersistenceDbContext,
        Framework.Guama.Location.Persistence.ORM.EF.Context.IPersistenceDbContext,
        Framework.Guama.Media.Persistence.ORM.EF.Context.IPersistenceDbContext,
        Framework.Guama.Security.Personal.Persistence.ORM.EF.Contract.IPersistenceDbContext
    {
        public ApplicationDbContext(System.Collections.Generic.IEnumerable<Framework.Guama.Core.Persistence.ORM.EF.Providers.Facade.IDbSchemaConfigurationProviderFacade> dbSchemaConfigurationProviders, DbContextOptions options) : base(dbSchemaConfigurationProviders, options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserRoleBridge> UserRoleBridges { get; set; }
        public DbSet<PersonIdentity> PersonIdentities { get; set; }
        public DbSet<OrganizationIdentity> OrganizationIdentities { get; set; }
        public DbSet<OrganizationPersonMembershipBridge> OrganizationPersonMembershipBridges { get; set; }
        public DbSet<EmailContactItem> EmailContactItems { get; set; }
        public DbSet<PhoneContactItem> PhoneContactItems { get; set; }
        public DbSet<SocialContactItem> SocialContactItems { get; set; }
        public DbSet<WebSiteContactItem> WebSiteContactItems { get; set; }
        public DbSet<PostalContactItem> PostalContactItems { get; set; }
        public DbSet<PersonEmailContactItemBridge> PersonEmailContactItemBridges { get; set; }
        public DbSet<PersonPhoneContactItemBridge> PersonPhoneContactItemBridges { get; set; }
        public DbSet<PersonPostalContactItemBridge> PersonPostalContactItemBridges { get; set; }
        public DbSet<PersonSocialContactItemBridge> PersonSocialContactItemBridges { get; set; }
        public DbSet<PersonWebSiteContactItemBridge> PersonWebSiteContactItemBridges { get; set; }
        public DbSet<OrganizationEmailContactItemBridge> OrganizationEmailContactItemBridges { get; set; }
        public DbSet<OrganizationPhoneContactItemBridge> OrganizationPhoneContactItemBridges { get; set; }
        public DbSet<OrganizationPostalContactItemBridge> OrganizationPostalContactItemBridges { get; set; }
        public DbSet<OrganizationSocialContactItemBridge> OrganizationSocialContactItemBridges { get; set; }
        public DbSet<OrganizationWebSiteContactItemBridge> OrganizationWebSiteContactItemBridges { get; set; }
        public DbSet<Thumbnail> Thumbnails { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Municipality> Municipalities { get; set; }
        public DbSet<Locality> Localities { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<StateLocation> StateLocations { get; set; }
        public DbSet<MunicipalityLocation> MunicipalityLocations { get; set; }
        public DbSet<LocalityLocation> LocalityLocations { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RolePermissionBridge> RolePermissionBridges { get; set; }

        public DbSet<Guideline> Guidelines { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<UnitMeasurement> UnitMeasurement { get; set; }
        public DbSet<Objective> Objectives { get; set; }
        public DbSet<ObjectiveUserBridge> ObjectiveUserBridges { get; set; }
        public DbSet<Proyection> Proyections { get; set; }
        public DbSet<ProyectionUserBridge> ProyectionUserBridges { get; set; }
        public DbSet<ProyectionGuidelineBridge> ProyectionGuidelineBridges { get; set; }
        public DbSet<ProyectionMonthlyClosure> ProyectionMonthlyClosures { get; set; }
        public DbSet<ProyectionOrganizationResponsabilityBridge> ProyectionOrganizationResponsabilityBridges { get; set; }
        public DbSet<ProyectionOrganizationParticipationBridge> ProyectionOrganizationParticipationBridges { get; set; }
        public DbSet<Action> Actions { get; set; }
        public DbSet<ActionUserBridge> ActionUserBridges { get; set; }
        public DbSet<ActionMonthlyClosure> ActionMonthlyClosures { get; set; }
        public DbSet<ActionOrganizationResponsabilityBridge> ActionOrganizationResponsabilityBridges { get; set; }
        public DbSet<ActionOrganizationParticipationBridge> ActionOrganizationParticipationBridges { get; set; }
        public DbSet<ActionIndicator> ActionIndicators { get; set; }
        public DbSet<ActionIndicatorMontlyPlan> ActionIndicatorMontlyPlans { get; set; }
        public DbSet<ProyectionIndicator> ProyectionIndicators { get; set; }
        public DbSet<ProyectionIndicatorMontlyPlan> ProyectionIndicatorMontlyPlans { get; set; }
        public DbSet<ObjectiveIndicator> ObjectiveIndicators { get; set; }
        public DbSet<ObjectiveIndicatorMontlyPlan> ObjectiveIndicatorMontlyPlans { get; set; }
        public DbSet<GlobalIndicator> GlobalIndicators { get; set; }
        public DbSet<GlobalIndicatorMontlyPlan> GlobalIndicatorMontlyPlans { get; set; }
        public DbSet<ActionIndicatorResult> ActionIndicatorResults { get; set; }
        public DbSet<ProyectionIndicatorResult> ProyectionIndicatorResults { get; set; }
        public DbSet<ObjectiveIndicatorResult> ObjectiveIndicatorResults { get; set; }
        public DbSet<GlobalIndicatorResult> GlobalIndicatorResults { get; set; }
    }
}
