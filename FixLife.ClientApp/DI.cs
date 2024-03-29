﻿using CommunityToolkit.Maui;
using FixLife.ClientApp.Common.Abstraction;
using FixLife.ClientApp.Infrastructure.Dashboard;
using FixLife.ClientApp.ViewModels;
using FixLife.ClientApp.ViewModels.AppSettings;
using FixLife.ClientApp.Views.AppSettings;
using FixLife.ClientApp.Views.MainPage;
using FixLife.Kafka.Interfaces;
using FixLife.Kafka.Services;

namespace FixLife.ClientApp
{
    public static class DI
    {
        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder appBuilder)
        {
            appBuilder.Services.AddSingleton<Dashboard>();
            appBuilder.Services.AddSingleton<DashboardViewModel>();
            appBuilder.Services.AddTransient<AppSettingsPage>();
            appBuilder.Services.AddTransient<AppSettingsViewModel>();
            return appBuilder;
        }

        public static MauiAppBuilder RegisterKafka(this MauiAppBuilder appBuilder)
        {
            appBuilder.Services.AddTransient<IAdminClientService, AdminClientService>();
            appBuilder.Services.AddTransient(typeof(IProducer<,>), typeof(ProducerService<,>));
            appBuilder.Services.AddTransient(typeof(IConsumer<,>), typeof(ConsumerService<,>));
            return appBuilder;
        }

        public static MauiAppBuilder RegisterServices(this MauiAppBuilder appBuilder)
        {
            appBuilder.Services.AddScoped<IDashboardService, DashboardService>();
            return appBuilder;
        }
    }
}
