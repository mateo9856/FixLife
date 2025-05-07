using FixLife.Admin.Db.Context;
using FixLife.Admin.Db.Entities.Base;
using FixLife.Admin.Db.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace FixLife.Admin.Db.Implementations
{
    public abstract class EntityOperationsBase<T> where T : EntityBase
    {
        private readonly DbSet<T> _dbTable;
        private readonly AdminContext _dbContext;

        private string _tableName;

        protected EntityOperationsBase(AdminContext adminContext)
        {
            _dbContext = adminContext;
            _dbTable = adminContext.Set<T>();
            _tableName = typeof(T).Name;
        }

        public IEnumerable<T> GetAll()
            => _dbTable.ToList();

        public async Task<IEnumerable<T>> GetAllAsync() 
            => await _dbTable.ToListAsync();

        public T GetById(Guid id)
        {
            return _dbTable.FirstOrDefault(d => d.Id == id)
                ?? throw new RecordNotFoundException(_tableName);
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbTable.FirstOrDefaultAsync(d => d.Id == id)
                ?? throw new RecordNotFoundException(_tableName);
        }

        public void DeleteById(Guid id)
        {
            _dbTable.Remove(GetById(id));
        }

        public void Update(T record)
        {
            _dbTable.Attach(record);
        }

        public void Add(T record)
        {
            _dbTable.Add(record);
        }

        public async Task AddAsync(T record)
        {
            await _dbTable.AddAsync(record);
        }

        public void Commit()
        {

        }

        public void Rollback()
        {

        }
    }
}
