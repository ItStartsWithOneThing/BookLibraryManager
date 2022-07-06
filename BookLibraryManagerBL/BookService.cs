using BookLibraryManagerBL.Models;
using BookLibraryManagerDAL;
using BookLibraryManagerDAL.Entities;
using System;
using System.Collections.Generic;

namespace BookLibraryManagerBL
{
    public class BookService
    {
        private IDbRepository<BookDTO> _repository;

        public BookService(IDbRepository<BookDTO> repository)
        {
            _repository = repository;
        }
        public Book CreateBook(Book book)
        {
            if(book != null)
            {

            }
            return null;
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return null;
        }

        public Book GetBookById(Guid id)
        {
            return null;
        }

        public bool UpdateBook(Book book)
        {
            return false;
        }

        public bool DeleteBook(Guid id)
        {
            return false;
        }
    }
}
