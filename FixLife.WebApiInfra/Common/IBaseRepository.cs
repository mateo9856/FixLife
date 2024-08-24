namespace FixLife.WebApiInfra.Common
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> GetByIdAsync(string id);
        Task AddAsync(T entity);
        Task DeleteAsync(string id);
        Task<IEnumerable<T>> GetAllAsync();
        Task UpdateAsync(T entity);
        Task SaveChangesAsync();
    }
}
