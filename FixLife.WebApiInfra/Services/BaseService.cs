using FixLife.WebApiInfra.Common;
using FixLife.WebApiInfra.Contexts;
using FixLife.WebApiInfra.Exceptions;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace FixLife.WebApiInfra.Services
{
    public class BaseService<T> : IBaseRepository<T> where T : class
    {
        protected readonly IMongoContextFactory<ApplicationContext> _contextFactory;
        public BaseService(IMongoContextFactory<ApplicationContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task AddAsync(T entity)
        {
            using var ctx = await _contextFactory.CreateDbContextAsync();
            await ctx.Set<T>().AddAsync(entity);
            await ctx.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            using var ctx = await _contextFactory.CreateDbContextAsync();
            var entityObject = await ctx.Set<T>().FindAsync(id)
                ?? throw new RecordNotFoundException(id, nameof(T));

            ctx.Set<T>().Remove(entityObject);
            await ctx.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            using var ctx = await _contextFactory.CreateDbContextAsync();
            return await ctx.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            using var ctx = await _contextFactory.CreateDbContextAsync();
            return await ctx.Set<T>().FindAsync(ObjectId.Parse(id))
                ?? throw new RecordNotFoundException(id, nameof(T));
        }

        public async Task UpdateAsync(T entity)
        {
            using var ctx = await _contextFactory.CreateDbContextAsync();
            ctx.Set<T>().Attach(entity);
            await ctx.SaveChangesAsync();
        }

    }
}
