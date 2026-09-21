using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MOROPO.SINERGIAGOB.Core.Presentation.Web.Frontend.Options;
using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.Framework.Guama.Core.Security.Common.Presentation.Web.Frontend.Extensions;
using MOROPO.Framework.Guama.Location.Presentation.Web.Common.Extensions;
using MOROPO.Framework.Guama.Media.Presentation.Web.Common.Extensions;
using MOROPO.Framework.Guama.Security.Personal.Persistence.ORM.EF.Extensions;
using MOROPO.Framework.Guama.Security.Personal.Presentation.Web.Backend.Extensions;
using MOROPO.SINERGIAGOB.GovCamaguey.Persistence.Entities;
using MOROPO.SINERGIAGOB.GovCamaguey.Web.BackOffice.Persistence.ORM.EF.Repositories;
using Serilog;

namespace MOROPO.SINERGIAGOB.GovCamaguey.Web.BackOffice
{
    public class Program
    {
        [ModuleInitializer]
        public static void Initialize()
        {
            System.AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            System.AppContext.SetSwitch("System.Drawing.EnableUnixSupport", true);
        }

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();

            builder.Services.Configure<Framework.Guama.Media.Presentation.Web.Common.Options.RelativeMediaFileManagementOptions>(builder.Configuration.GetSection("FileManagement"));
            builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JWT"));
            builder.Services.Configure<Configurations.CurrentTerminalReleaseDetailConfiguration>(builder.Configuration.GetSection("CurrentTerminalReleaseDetail"));

            builder.Services.AddDbContext<Persistence.ORM.EF.Context.ApplicationDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<DbContext, Persistence.ORM.EF.Context.ApplicationDbContext>();

            builder.Services.PerformCoreWebSecurityCommonFrontendServiceRegistration();
            builder.Services.PerformSecurityPersonalWebBackendServiceRegistration();
            builder.Services.PerformMediaWebCommonRelativePathFileManagementServiceRegistration();
            builder.Services.PerformLocationWebCommonServiceRegistration();

            builder.Services.AddScoped<IEntityRepository<ElementSource, System.Guid>, ElementSourceRepository>();
            builder.Services.AddScoped<IEntityRepository<UnitMeasurement, System.Guid>, UnitMeasurementRepository>();
            builder.Services.AddScoped<IEntityRepository<Guideline, System.Guid>, GuidelineRepository>();
            builder.Services.AddScoped<IEntityRepository<Person, System.Guid>, PersonRepository>();
            builder.Services.AddScoped<IEntityRepository<Organization, System.Guid>, OrganizationRepository>();
            builder.Services.AddScoped<IEntityRepository<Objective, System.Guid>, ObjectiveRepository>();
            builder.Services.AddScoped<IBridgeRepository<ObjectiveUserBridge, Objective, System.Guid, Framework.Guama.Security.Personal.Persistence.Entities.User, string>, ObjectiveUserBridgeRepository>();
            builder.Services.AddScoped<IEntityRepository<Proyection, System.Guid>, ProyectionRepository>();
            builder.Services.AddScoped<IBridgeRepository<ProyectionUserBridge, Proyection, System.Guid, Framework.Guama.Security.Personal.Persistence.Entities.User, string>, ProyectionUserBridgeRepository>();
            builder.Services.AddScoped<IEntityRepository<ProyectionMonthlyClosure, System.Guid>, ProyectionMonthlyClosureRepository>();
            builder.Services.AddScoped<IEntityRepository<Action, System.Guid>, ActionRepository>();
            builder.Services.AddScoped<IBridgeRepository<ActionUserBridge, Action, System.Guid, Framework.Guama.Security.Personal.Persistence.Entities.User, string>, ActionUserBridgeRepository>();
            builder.Services.AddScoped<IEntityRepository<ActionMonthlyClosure, System.Guid>, ActionMonthlyClosureRepository>();
            builder.Services.AddScoped<IEntityRepository<ActionIndicator, System.Guid>, ActionIndicatorRepository>();
            builder.Services.AddScoped<IEntityRepository<ActionIndicatorMontlyPlan, System.Guid>, ActionIndicatorMontlyPlanRepository>();
            builder.Services.AddScoped<IEntityRepository<ActionIndicatorResult, long>, ActionIndicatorResultRepository>();
            builder.Services.AddScoped<IEntityRepository<ProyectionIndicator, System.Guid>, ProyectionIndicatorRepository>();
            builder.Services.AddScoped<IEntityRepository<ProyectionIndicatorMontlyPlan, System.Guid>, ProyectionIndicatorMontlyPlanRepository>();
            builder.Services.AddScoped<IEntityRepository<ProyectionIndicatorResult, long>, ProyectionIndicatorResultRepository>();
            builder.Services.AddScoped<IEntityRepository<ObjectiveIndicator, System.Guid>, ObjectiveIndicatorRepository>();
            builder.Services.AddScoped<IEntityRepository<ObjectiveIndicatorMontlyPlan, System.Guid>, ObjectiveIndicatorMontlyPlanRepository>();
            builder.Services.AddScoped<IEntityRepository<ObjectiveIndicatorResult, long>, ObjectiveIndicatorResultRepository>();
            builder.Services.AddScoped<IEntityRepository<GlobalIndicator, System.Guid>, GlobalIndicatorRepository>();
            builder.Services.AddScoped<IEntityRepository<GlobalIndicatorMontlyPlan, System.Guid>, GlobalIndicatorMontlyPlanRepository>();
            builder.Services.AddScoped<IEntityRepository<GlobalIndicatorResult, long>, GlobalIndicatorResultRepository>();

            builder.Services.AddSingleton<Framework.Guama.Core.Persistence.ORM.EF.Providers.Facade.IDbSchemaConfigurationProviderFacade, Persistence.ORM.EF.Providers.ApplicationDbSchemaConfigurationProvider>();

            builder.Services.AddScoped<IEntityRepository<Framework.Guama.Core.Persistence.ORM.EF.Entities.Trace, long>, Framework.Guama.Core.Persistence.ORM.EF.Repositories.TraceRepository>();

            builder.Services.AddSingleton<Framework.Guama.Core.Persistence.ORM.EF.Helpers.Facade.IDataInitializationHelper, Helpers.GlobalDataInitializationHelper>();

            builder.Services.AddScoped<Services.Contract.IHtmlToPdfService, Services.WkhtmltopdfHtmlToPdfService>();
            builder.Services.AddScoped<Services.Contract.IViewRenderService, Services.DefaultViewRenderService>();

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddAuthentication(config =>
            {
                config.DefaultScheme = Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme;
            })
                .AddCookie(config =>
                {
                    config.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/error/403");
                    config.LoginPath = new Microsoft.AspNetCore.Http.PathString("/security/account/login");
                    config.ExpireTimeSpan = System.TimeSpan.FromMinutes(15);
                    config.SlidingExpiration = true;
                })
                .AddJwtBearer(config =>
                {
                    config.RequireHttpsMetadata = false;
                    config.SaveToken = true;
                    config.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidIssuer = builder.Configuration["JWT:Issuer"],
                        ValidAudience = builder.Configuration["JWT:Issuer"],
                        ValidateLifetime = false,
                        RequireExpirationTime = false,
                        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
                        ClockSkew = System.TimeSpan.FromMinutes(15)
                    };
                });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy(Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme, policy =>
                {
                    policy.AddAuthenticationSchemes(Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme);
                    policy.RequireClaim(System.Security.Claims.ClaimTypes.NameIdentifier);
                });
            });

            #region Security concreter dependencies configuration
            builder.Services.AddSingleton<Framework.Guama.Core.Presentation.Web.Security.Helpers.Facade.ILicenseValidationHelperFacade, Helpers.WillcardLicenseValidationHelper>();
            builder.Services.AddTransient<Framework.Guama.Core.Presentation.Web.Security.Models.View.Contrat.IUserProfileViewModel, Models.View.UserProfileViewModel>();
            builder.Services.AddSingleton<Framework.Guama.Core.Presentation.Web.Common.ModelBinding.Contract.IConcretModelBindHelper, Helpers.ConcretModelBindHelper>();
            builder.Services.AddScoped<Framework.Guama.Core.Presentation.Web.Security.Managers.Facade.IUserProfileManagerFacade, Managers.UserProfileManager>();
            #endregion

            var defaultCulture = new System.Globalization.CultureInfo("es");

            builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(defaultCulture);
                options.SupportedCultures = [defaultCulture];
                options.SupportedUICultures = [defaultCulture];

                options.RequestCultureProviders = new[] { new Framework.Guama.Core.Presentation.Web.Common.Localization.UrlRequestCultureProvider() };
            });

            builder.Services.Configure<Microsoft.AspNetCore.Routing.RouteOptions>(options => options.LowercaseUrls = true);

            builder.Services.AddSingleton<Helpers.GlobalViewLocalizationHelper>();

            builder.Services.AddResponseCaching();
            builder.Services.AddMemoryCache();
            builder.Services.AddSession();

            builder.Services
                .AddControllersWithViews(config =>
                {
                    config.ModelBinderProviders.Insert(0, new Framework.Guama.Core.Presentation.Web.Common.ModelBinding.AbstractsModelBinderProvider());
                })
                .AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization(config =>
                {
                    config.DataAnnotationLocalizerProvider = (type, factory) =>
                    {
                        return factory.Create(typeof(LocalizationResources));
                    };
                });

            builder.Services.AddCors();
            builder.Services.AddDataProtection().PersistKeysToFileSystem(new System.IO.DirectoryInfo(System.IO.Path.Combine(builder.Environment.ContentRootPath, "dpkeys")));
            builder.Services.AddSingleton<Microsoft.AspNetCore.Mvc.ViewFeatures.ITempDataProvider, Microsoft.AspNetCore.Mvc.ViewFeatures.CookieTempDataProvider>();

            var app = builder.Build();

            app.Services.GetService<Microsoft.Extensions.Logging.ILoggerFactory>().AddSerilog(Log.Logger);

            app.UseRequestLocalization(app.Services.GetService<Microsoft.Extensions.Options.IOptions<RequestLocalizationOptions>>().Value);

            using (var serviceScope = app.Services.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<DbContext>();

                if (app.Environment.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }
                else if (app.Environment.IsStaging())
                {
                    dbContext.Database.Migrate();

                    var dataInitializationHelpers = app.Services.GetServices<Framework.Guama.Core.Persistence.ORM.EF.Helpers.Facade.IDataInitializationHelper>();

                    foreach (var dataInitializationHelper in dataInitializationHelpers)
                    {
                        dataInitializationHelper.InitialiseData(dbContext);
                    }

                    if (!((Persistence.ORM.EF.Context.ApplicationDbContext)dbContext).Users.Any())
                    {
                        var defaultUserId = "admin";
                        var k3y = new System.Guid(0x7c9294dd, 0x1e74, 0x45b2, 0xa8, 0x45, 0xaf, 0x6A, 0xe5, 0x2a, 0xf6, 0x19);

                        var protectedPassword = (dbContext as Persistence.ORM.EF.Context.ApplicationDbContext).CreateSecureUser(k3y, defaultUserId, "system");

                        Log.Logger.Information(string.Format(">>>>>> Protected Password : '{0}' => '{1}'", defaultUserId, protectedPassword));
                    }
                }
                else
                {
                    app.UseExceptionHandler("/error/exception");
                    app.UseHsts();
                }
                app.UseStatusCodePagesWithReExecute("/error/{0}");

                var atStartUpRoles = dbContext.Set<Framework.Guama.Core.Security.Common.Persistence.Entities.Role>().Include(e => e.PermissionBridges).ThenInclude(b => b.Stern).ToArray();

                var roleMemoryManager = app.Services.GetService<Framework.Guama.Core.Presentation.Web.Security.Managers.Facade.IRoleMemoryManagerFacade>();

                foreach (var item in atStartUpRoles)
                {
                    roleMemoryManager.RememberRole(item);
                }
            }

            app
                .UseStaticFiles()
                .UseCors()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseSession()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute("area_default", "{area:exists}/{controller}/{action}/{id?}", new { action = "Index" });
                    endpoints.MapControllerRoute("default", "{controller}/{action}/{id?}", new { controller = "Start", action = "Index" });
                });

            app.Run();
        }
    }
}
