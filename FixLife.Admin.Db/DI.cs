using FixLife.Admin.Db.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FixLife.Admin.Db
{
    public static class DI
    {
        public static void AddDatabase(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AdminContext>((ctx) =>
            {
                ctx.UseSqlServer(connectionString);
            });
        }
    }
}
