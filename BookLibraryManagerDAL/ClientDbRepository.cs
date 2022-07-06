using BookLibraryManagerDAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookLibraryManagerDAL
{
    public class ClientDbRepository : IDbRepository<ClientDTO>
    {
        private readonly EFCoreContext _dbcontext;

        public void Create(ClientDTO book)
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

        public void Update(ClientDTO book)
        {
            throw new NotImplementedException();
        }
    }
}
