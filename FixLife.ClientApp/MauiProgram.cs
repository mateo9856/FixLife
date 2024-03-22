using AutoMapper;
using CommunityToolkit.Maui;
using FixLife.ClientApp.Common;
using FixLife.ClientApp.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.Reflection;

namespace FixLife.ClientApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            CultureInfo.CurrentUICulture = new CultureInfo("pl-PL", false);
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
                .RegisterViewModels()
                .RegisterServices()
                .RegisterKafka()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Font Awesome 6 Brands-Regular-400.otf", "FontAwesomeBrandsRegular");
                    fonts.AddFont("Font Awesome 6 Free-Regular-400.otf", "FontAwesomeFreeRegular");
                    fonts.AddFont("Font Awesome 6 Free-Solid-900.otf", "FontAwesomeFreeSolid");
                });

#if DEBUG
		builder.Logging.AddDebug();
#endif
            return builder.Build();
        }
    }
}