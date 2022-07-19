using AutoMapper;
using BookLibraryManagerBL.DTOs;
using BookLibraryManagerDAL;
using BookLibraryManagerDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookLibraryManagerBL.BooksService.Services
{
    public class BooksService : IBooksService

    // Make Intarface Extraction and create service
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

        public async Task<Guid> AddBook(BookDto book)
        {
            if (book != null)
            {
                var dbBook = _mapper.Map<Book>(book);

                var result = await _genericBookRepository.Add(dbBook);

                return result;
            }

            return Guid.Empty;
        }

        //public async Task<IEnumerable<BookDto>> GetAllBooks()
        //{
        //    return null;
        //}

        public async Task<BookDto> GetBookById(Guid id)
        {
            var book = await _genericBookRepository.GetById(id);

            return _mapper.Map<BookDto>(book);
        }



        //public async Task<bool> UpdateBook(BookDto book)
        //{
        //    return false;
        //}

        //public async Task<bool> DeleteBook(Guid id)
        //{
        //    return false;
        //}

        public async Task<BookDto> GetFullInfo(Guid id)
        {
            var result = await _booksRepository.GetFullInfo(id);

            return MapTupleToBookDto(result);
        }

        private BookDto MapTupleToBookDto((Book book, IEnumerable<BookRevision> bookRevisions) result)
        {
            return new BookDto()
            {
                Author = result.book?.Author,
                Title = result.book?.Title,
                BookId = result.book.Id,
                BookRevisions = MapRevisions(result.bookRevisions)
            };
        }

        private IEnumerable<BookRevisionDto> MapRevisions(IEnumerable<BookRevision> bookRevisions)
        {
            return bookRevisions.Select(x => new BookRevisionDto()
            {
                Price = x.Price,
                PagesCount = x.PagesCount,
                PublishingYear = x.PublishingYear
            });
        }
    }
}
