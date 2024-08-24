using Microsoft.EntityFrameworkCore;

namespace FixLife.WebApiInfra.Common
{
    public interface IMongoContextFactory<T> where T : DbContext
    {
        public T CreateDbInstance();
    }
}
