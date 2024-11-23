using Microsoft.EntityFrameworkCore;

namespace FixLife.WebApiInfra.Common
{
    public interface IMongoContextFactory<TContext>
    {
        public TContext CreateDbInstance();
    }
}
