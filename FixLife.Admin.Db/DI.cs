using FixLife.Admin.Db.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FixLife.Admin.Db
{
    public static class DI
    {
        public static void AddDatabase(this IServiceCollection services)
        {
            services.AddDbContext<AdminContext>((ctx) =>
            {
                ctx.UseSqlServer("TODO: CONNECTION STRING WITH HASH");
            });
        }
    }
}
