﻿using BookLibraryManagerDAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<Guid> Add(T item)
        {
            item.Id = Guid.NewGuid();
            _dbSet.Add(item);

            await _dbContext.SaveChangesAsync();

            return item.Id;
        }

        public async Task<bool> DeleteById(Guid id)
        {
            var item = new T { Id = id };
            _dbContext.Entry(item).State = EntityState.Deleted;

            return await _dbContext.SaveChangesAsync() != 0;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetById(Guid id)
        {
            return await _dbSet.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> Update(T item)
        {
            _dbContext.Entry(item).State = EntityState.Modified;

            return await _dbContext.SaveChangesAsync() != 0;
        }     
    }
}
