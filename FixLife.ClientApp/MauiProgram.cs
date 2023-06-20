using AutoMapper;
using CommunityToolkit.Maui;
using FixLife.ClientApp.Common;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace FixLife.ClientApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            CultureInfo.CurrentUICulture = new CultureInfo("pl-PL", false);
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .RegisterViewModels()
                .RegisterServices()
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