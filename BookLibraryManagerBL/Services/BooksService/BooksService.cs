using AutoMapper;
using BookLibraryManagerBL.DTOs;
using BookLibraryManagerDAL;
using BookLibraryManagerDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookLibraryManagerBL.Services.BooksService
{
    public class BooksService : IBooksService
    {
        private IDbGenericRepository<Book> _genericBookRepository;

        private IDbBooksRepository _booksRepository;

        private IMapper _mapper;

        public BooksService(IDbGenericRepository<Book> repository, IDbBooksRepository bookRepository, IMapper mapper)
        {
            _genericBookRepository = repository;

            _booksRepository = bookRepository;

            _mapper = mapper;
        }

        #region CRUD

        public async Task<Guid> CreateBook(BookDto book)
        {
            if (book != null)
            {
                var dbBook = _mapper.Map<Book>(book);

                return await _genericBookRepository.Create(dbBook);
            }

            throw new ArgumentException("Cannot create empty book");
        }

        public async Task<BookDto> GetBookById(Guid id)
        {
            var book = await _genericBookRepository.GetById(id);

            return _mapper.Map<BookDto>(book);
        }

        public async Task<IEnumerable<BookDto>> GetAllBooks()
        {
            var result = await _genericBookRepository.GetAll();

            return _mapper.Map<IEnumerable<BookDto>>(result);
        }


        public async Task<bool> UpdateBook(BookDto book)
        {
            var targetBook = _mapper.Map<Book>(book);

            return await _genericBookRepository.Update(targetBook);
        }

        public async Task<bool> DeleteBook(Guid id)
        {
            return await _genericBookRepository.DeleteById(id);
        }

        #endregion CRUD

        #region Get

        public async Task<BookDto> GetFullBookInfo(Guid id)
        {;

            var result = await _booksRepository.GetFullBookInfo(id);

            var book = _mapper.Map<BookDto>(result);

            book.BookRevisions = _mapper.Map<IEnumerable<BookRevisionDto>>(book.BookRevisions);

            return book;
        }

        public async Task<IEnumerable<BookDto>> GetBooksByAuthor(string author)
        {
            var booksList = await _genericBookRepository.GetRangeByPredicate(x=>x.Author==author);

            var result = _mapper.Map<IEnumerable<BookDto>>(booksList);

            return result;
        }

        public async Task<IEnumerable<BookDto>> GetNearestBooks(string cityName, float latitude, float longitude)
        {

            return null;
        }

        #endregion
    }
}
