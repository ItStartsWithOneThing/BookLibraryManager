using BookLibraryManagerBL.DTOs;
using System;
using System.Threading.Tasks;

namespace BookLibraryManagerBL.BooksService.Services
{
    public interface IBooksService
    {
        Task<BookDto> GetBookById(Guid id);
        Task<Guid> AddBook(BookDto book);
        Task<BookDto> GetFullInfo(Guid id);
    }
}