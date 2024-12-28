using Microsoft.EntityFrameworkCore;

namespace FixLife.WebApiInfra.Common
{
    public interface IMongoContextFactory<TContext> : IDbContextFactory<TContext> where TContext : DbContext
    {
    }
}
