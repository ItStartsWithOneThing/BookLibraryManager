using BookLibraryManagerBL.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookLibraryManagerBL.BooksService.Services
{
    public interface IBooksService
    {
        Task<Guid> CreateBook(BookDto book);
        Task<BookDto> GetBookById(Guid id);
        Task<IEnumerable<BookDto>> GetAllBooks();
        Task<bool> UpdateBook(BookDto book);
        Task<bool> DeleteBook(Guid id);
        Task<BookDto> GetFullBookInfo(Guid id);
        Task<IEnumerable<BookDto>> GetBooksByAuthor(string author);
        Task<IEnumerable<BookDto>> GetNearestBooks(string cityName, float latitude, float longitude);
    }
}