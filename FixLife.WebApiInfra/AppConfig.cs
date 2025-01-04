using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FixLife.WebApiInfra
{
    public static class AppConfig
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtOptions>(configuration.GetSection("Jwt"));
            return services;
        }
    }
}