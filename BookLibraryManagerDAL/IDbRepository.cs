using System;
using System.Collections.Generic;
using System.Text;

namespace BookLibraryManagerDAL
{
    public interface IDbRepository<T> where T: class
    {
        void Create(T book);

        void Get(Guid id);

        void Update(T book);

        void Delete(Guid Id);
    }
}
