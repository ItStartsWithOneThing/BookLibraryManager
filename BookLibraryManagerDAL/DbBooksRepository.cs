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

        public async Task<Book> GetFullBookInfo(Guid id)
        {
            var book = await _dbContext.Books.Include(x => x.BookRevisions).Where(x => x.Id == id).FirstOrDefaultAsync();

            return book;
        }
    }
}
