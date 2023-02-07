using FixLife.WebApiInfra.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FixLife.WebApiInfra
{
    public static class AppConfig
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(configuration.GetConnectionString("FixLife")));
            services.AddDbContext<IdentityContext>(options => options.UseSqlServer(configuration.GetConnectionString("FixLife")));
            services.Configure<JwtOptions>(configuration.GetSection("Jwt"));
            return services;
        }
    }
}