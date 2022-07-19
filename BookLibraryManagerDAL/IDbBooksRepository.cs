using BookLibraryManagerDAL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookLibraryManagerDAL
{
    public interface IDbBooksRepository
    {
        Task<IEnumerable<Book>> GetAll();
        Task<(Book book, IEnumerable<BookRevision> bookRevisions)> GetFullInfo(Guid id);
    }
}