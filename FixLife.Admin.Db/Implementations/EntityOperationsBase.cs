using FixLife.Admin.Db.Context;
using FixLife.Admin.Db.Entities.Base;
using FixLife.Admin.Db.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Transactions;

namespace FixLife.Admin.Db.Implementations
{
    public abstract class EntityOperationsBase<T> : IDisposable where T : EntityBase
    {
        protected readonly DbSet<T> _dbTable;
        protected readonly AdminContext _dbContext;

        private IDbContextTransaction? _dbContextTransaction;

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

        public void SaveChanges()
        {
            try
            {
                _dbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new DbUpdateConcurrencyException(_tableName, ex);
            }
        }

        public async Task SaveChangesAsync()
        {
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new DbUpdateConcurrencyException(_tableName, ex);
            }
        }

        public void BeginTransaction() {

            _dbContextTransaction = _dbContext.Database.BeginTransaction();
        }

        public void BeginTransactionWithOperations(Action dbOperations)
        {
            using var transaction = _dbContext.Database.BeginTransaction();

            try
            {
                dbOperations();
                _dbContext.SaveChanges();
                transaction.Commit();
            } 
            
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new TransactionException(_tableName, ex);
            }
        }

        public void CommitOrRollbackTransaction()
        {
            if (_dbContextTransaction is null)
                return;

            try
            {
                _dbContext.SaveChanges();
                _dbContextTransaction.Commit();
            }
            catch (Exception ex)
            {
                _dbContextTransaction.Rollback();
                throw new TransactionException(_tableName, ex);
            } 
            finally
            {
                _dbContextTransaction.Dispose();
                _dbContextTransaction = null;
            }
        }

        public void Dispose()
        {
            if(_dbContextTransaction is not null)
                _dbContextTransaction.Dispose();
        }
    }
}
