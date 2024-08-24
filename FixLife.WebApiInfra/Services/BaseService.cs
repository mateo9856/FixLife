using FixLife.WebApiInfra.Common;
using FixLife.WebApiInfra.Contexts;
using FixLife.WebApiInfra.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace FixLife.WebApiInfra.Services
{
    public class BaseService<T> : IBaseRepository<T> where T : class
    {
        protected readonly ApplicationContext _context;
        private DbSet<T> _dbSet;
        public BaseService(IMongoContextFactory<ApplicationContext> contextFactory)
        {
             _context = contextFactory.CreateDbInstance();
            _dbSet = _context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task DeleteAsync(string id)
        {
            var entityObject = await _dbSet.FindAsync(id)
                ?? throw new RecordNotFoundException(id, nameof(T));

            _dbSet.Remove(entityObject);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            return await _dbSet.FindAsync(id)
                ?? throw new RecordNotFoundException(id, nameof(T));
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Attach(entity);
        }

        public async Task SaveChangesAsync()
        {
             await _context.SaveChangesAsync();
        }

    }
}
