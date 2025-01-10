using FixLife.AI.Client.Abstraction;
using FixLife.AI.Client.Implementation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FixLife.AI.Client
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("AI calling process test.");
            HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddSingleton<IGeminiClient, GeminiClient>();
            builder.Services.AddSingleton<IPlanRecomendationService, PlanRecommendationService>();

            var serviceProvider = builder.Services.BuildServiceProvider();
            var recommendationService = serviceProvider.GetService<IPlanRecomendationService>();

            if(recommendationService is null)
            {
                Console.WriteLine("Service not found.");
                return;
            }
            await recommendationService.GetFreeTimes();

        }
    }
}
