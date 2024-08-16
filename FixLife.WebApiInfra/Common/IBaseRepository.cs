namespace FixLife.WebApiInfra.Common
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> GetByIdAsync(Guid id);
        Task AddAsync(T entity);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task UpdateAsync(T entity);
        Task SaveChangesAsync();
    }
}
