using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WiventoryAPI.Data;

namespace WiventoryAPI.Services
{
    // Interface
    public interface IStorageService<T> where T : class
    {
        Task AddAsync(T entity);
        void Delete(T entity);
        Task<T?> GetByIdAsync(object id);
        Task<T?> GetByAsync(Expression<Func<T, bool>> predicate);
        Task<int> SaveAsync();
    }

    // Implementation
    public class StorageService<T> : IStorageService<T> where T : class
    {
        private readonly AppDbContext _dbContext;

        public StorageService(AppDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task AddAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            await _dbContext.Set<T>().AddAsync(entity);
        }

        public void Delete(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _dbContext.Set<T>().Remove(entity);
        }

        public async Task<T?> GetByIdAsync(object id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<T?> GetByAsync(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            return await _dbContext.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public async Task<int> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}

