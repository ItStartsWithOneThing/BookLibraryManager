using BookLibraryManagerDAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookLibraryManagerDAL
{
    public class LibraryDbRepository : IDbRepository<LibraryDTO>
    {
        private readonly EFCoreContext _dbcontext;

        public void Create(LibraryDTO book)
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

        public void Update(LibraryDTO book)
        {
            throw new NotImplementedException();
        }
    }
}
