using BookLibraryManagerDAL.Entities;
using System;

namespace BookLibraryManagerDAL
{
    public class BookDbRepository : IDbRepository<BookDTO>
    {
        private readonly EFCoreContext _dbcontext;

        public void Create(BookDTO book)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid Id)
        {
            throw new NotImplementedException();
        }

        public void Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(BookDTO book)
        {
            throw new NotImplementedException();
        }

        public void TEST()
        {
            
        }
    }
}
