using FixLife.Admin.Db.Context;
using Microsoft.EntityFrameworkCore;

namespace FixLife.Admin.Db.Implementations
{
    public abstract class DbContextRepository
    {
        private readonly AdminContext _dbContext;

        public DbContextRepository(AdminContext adminContext)
        {
            _dbContext = adminContext;
        }

        public DbSet<T> GetDbSet<T>() where T : class
        {
            return _dbContext.Set<T>();
        }

    }
}
