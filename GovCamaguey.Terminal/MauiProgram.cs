using CommunityToolkit.Maui;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using MOROPO.SINETGIAGOB.Core.Presentation.Terminal.MAUI.UI.Factories.Facade;
using MOROPO.SINETGIAGOB.GovCamaguey.Interop.API.Common.Models.Display;
using MOROPO.SINETGIAGOB.GovCamaguey.Terminal.Factories.Configuration;
using MOROPO.SINETGIAGOB.GovCamaguey.Terminal.Factories.Detail;
using MOROPO.SINETGIAGOB.GovCamaguey.Terminal.Models.Page.Configuration;
using SkiaSharp.Views.Maui.Controls.Hosting;

namespace MOROPO.SINETGIAGOB.GovCamaguey.Terminal
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()
               .UseMauiCommunityToolkit()
               .UseMauiCommunityToolkitMediaElement()
               .UseSkiaSharp()
               .ConfigureFonts(fonts =>
               {
                   fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                   fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                   fonts.AddFont("materialdesignicons-webfont.ttf", "MaterialDesignIcons");
               })
               .Services
                   .AddSingleton<Framework.Guama.Core.Presentation.API.Connect.Providers.Facade.IHttpClientHelperProviderFacade, Providers.HttpClientHelperProvider>()
                   .AddSingleton<Core.Presentation.Terminal.MAUI.Providers.Facade.IRuntimeContextProviderFacade, Providers.SingleRuntimeContextProvider>()
                   .AddSingleton<Core.Presentation.Terminal.MAUI.Helpers.Facade.IExceptionHandlingHelperFacade, Helpers.MainExceptionHandlingHelper>()
                   .AddSingleton<Core.Presentation.Terminal.MAUI.Managers.Facade.ISessionManagerFacade, Core.Presentation.Terminal.MAUI.Managers.DefaultSessionManager>()
                   .AddSingleton<Core.Presentation.Terminal.MAUI.Managers.Facade.ISettingManagerFacade, Core.Presentation.Terminal.MAUI.Managers.DefaultSettingManager>()
                   .AddSingleton<IDetailContentPageFactoryFacade<GuidelineDisplayModel>, GuidelineDetailPageFactory>()
                   .AddSingleton<IDetailContentPageFactoryFacade<UnitMeasurementDisplayModel>, UnitMeasurementDetailPageFactory>()
                   .AddSingleton<IDetailContentPageFactoryFacade<PersonDisplayModel>, PersonDetailPageFactory>()
                   .AddSingleton<IDetailContentPageFactoryFacade<OrganizationDisplayModel>, OrganizationDetailPageFactory>()
                   .AddSingleton<IDetailContentPageFactoryFacade<ObjectiveDisplayModel>, ObjectiveDetailPageFactory>()
                   .AddSingleton<IDetailContentPageFactoryFacade<GlobalIndicatorDisplayModel>, GlobalIndicatorDetailPageFactory>()
                   .AddSingleton<IConfigurationContentPageFactoryFacade<GuidelineDisplayModel, GuidelineConfigurationPageModel>, GuidelineConfigurationPageFactory>()
                   .AddSingleton<IConfigurationContentPageFactoryFacade<UnitMeasurementDisplayModel, UnitMeasurementConfigurationPageModel>, UnitMeasurementConfigurationPageFactory>()
                   .AddSingleton<IConfigurationContentPageFactoryFacade<PersonDisplayModel, PersonConfigurationPageModel>, PersonConfigurationPageFactory>()
                   .AddSingleton<IConfigurationContentPageFactoryFacade<OrganizationDisplayModel, OrganizationConfigurationPageModel>, OrganizationConfigurationPageFactory>()
                   .AddSingleton<IConfigurationContentPageFactoryFacade<ObjectiveDisplayModel, ObjectiveConfigurationPageModel>, ObjectiveConfigurationPageFactory>()
                   .AddSingleton<IConfigurationContentPageFactoryFacade<GlobalIndicatorDisplayModel, GlobalIndicatorConfigurationPageModel>, GlobalIndicatorConfigurationPageFactory>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
