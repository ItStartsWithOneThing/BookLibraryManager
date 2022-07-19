using BookLibraryManagerDAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookLibraryManagerDAL
{
    public class DbBooksRepository : IDbBooksRepository
    {
        private EFCoreContext _dbContext { get; set; }

        public DbBooksRepository(EFCoreContext context)
        {
            _dbContext = context;
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            return await _dbContext.Books.ToListAsync();
        }

        public async Task<(Book book, IEnumerable<BookRevision> bookRevisions)> GetFullInfo(Guid id)
        {
            var result = await _dbContext.BookRevisions.Include(x => x.Book).Where(x => x.BookId == id).ToListAsync();

            var book = result.FirstOrDefault()?.Book;

            return (book, result);
        }
    }
}
