using BookLibraryManagerDAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BookLibraryManagerDAL
{
    public class DbGenericRepository<T> : IDbGenericRepository<T> where T: BaseEntity, new()
    {
        private readonly EFCoreContext _dbContext;

        private DbSet<T> _dbSet;

        public DbGenericRepository(EFCoreContext context)
        {
            _dbContext = context;
            _dbSet = _dbContext.Set<T>();
        }

        #region CRUD

        public async Task<Guid> Create(T item)
        {
            item.Id = Guid.NewGuid();

            await _dbSet.AddAsync(item);

            var result = await _dbContext.SaveChangesAsync();

            if (result > 0)
            {
                return item.Id;
            }

            return Guid.Empty;
        }

        public async Task<T> GetById(Guid id)
        {
            return await _dbSet.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<bool> Update(T item)
        {
            _dbContext.Entry(item).State = EntityState.Modified;

            return await _dbContext.SaveChangesAsync() != 0;
        }

        public async Task<bool> DeleteById(Guid id)
        {
            var item = new T { Id = id };
            _dbContext.Entry(item).State = EntityState.Deleted;

            return await _dbContext.SaveChangesAsync() != 0;
        }

        #endregion CRUD

        #region Get

        public async Task<T> GetSingleByPredicate(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).FirstOrDefaultAsync() ;
        }

        public async Task<IEnumerable<T>> GetRangeByPredicate(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        #endregion
    }
}
