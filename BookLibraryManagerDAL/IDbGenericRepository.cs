using BookLibraryManagerDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BookLibraryManagerDAL
{
    public interface IDbGenericRepository<T> where T: BaseEntity, new()
    {
        Task<Guid> Create(T item);
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(Guid id);
        Task<bool> Update(T item);
        Task<bool> DeleteById(Guid Id);
        Task<IEnumerable<T>> GetRangeByPredicate(Expression<Func<T, bool>> predicate);
    }
}
