using AutoMapper;
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Core;
using FixLife.ClientApp.Common;
using FixLife.ClientApp.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.Reflection;

namespace FixLife.ClientApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            SettingsOperations.LoadOrCreateSettings();
            using var settings = Assembly
                .GetExecutingAssembly()
                .GetManifestResourceStream("FixLife.ClientApp.appsettings.json");
            var jsonBuilder = new ConfigurationBuilder()
                .AddJsonStream(settings)
                .Build();
            var builder = MauiApp.CreateBuilder();
            builder.Configuration.AddConfiguration(jsonBuilder);

            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .RegisterServices()
                .RegisterViewModels()
                .RegisterKafka()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Font Awesome 6 Brands-Regular-400.otf", "FontAwesomeBrandsRegular");
                    fonts.AddFont("Font Awesome 6 Free-Regular-400.otf", "FontAwesomeFreeRegular");
                    fonts.AddFont("Font Awesome 6 Free-Solid-900.otf", "FontAwesomeFreeSolid");
                });
            builder.Services.Configure<ApiConnectionOptions>(d => {
                d.TrustConnection = builder.Configuration.GetSection(ApiConnectionOptions.ApiConnection)["TrustConnection"] ?? "false";
                d.Windows = builder.Configuration.GetSection(ApiConnectionOptions.ApiConnection)["Windows"] ?? "";
                d.Android = builder.Configuration.GetSection(ApiConnectionOptions.ApiConnection)["Android"] ?? "";
                d.AndroidHttps = builder.Configuration.GetSection(ApiConnectionOptions.ApiConnection)["AndroidHttps"] ?? "";
                d.CertIssuer = builder.Configuration.GetSection(ApiConnectionOptions.ApiConnection)["CertIssuer"] ?? "";
            });

#if DEBUG
            builder.Logging.AddDebug();
#endif
            return builder.Build();
        }
    }
}