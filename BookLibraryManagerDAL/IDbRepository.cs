using BookLibraryManagerDAL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookLibraryManagerDAL
{
    public interface IDbRepository<T> where T: BaseEntity, new()
    {
        Task<Guid> Add(T item);

        Task<IEnumerable<T>> GetAll();

        Task<T> GetByID(Guid id);

        Task<bool> Update(T item);

        Task<bool> DeleteById(Guid Id);
    }
}
