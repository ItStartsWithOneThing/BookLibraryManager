using AutoFixture;
using BookLibraryManagerDAL.Entities;
using NUnit.Framework;
using System.Threading.Tasks;
using BookLibraryManagerBL.Services.BooksService;
using Moq;
using BookLibraryManagerDAL;
using AutoMapper;
using BookLibraryManagerBL.DTOs;
using System;
using BookLibraryManagerBL.AutoMapper.Profiles;
using System.Collections.Generic;

namespace BookLibraryManagerBL.Tests
{
    public class BookServiceTests
    {
        private Fixture _fixture;
        private BooksService _bookService;
        private Mock<IDbGenericRepository<Book>> _genericBookRepositoryMock;
        private Mock<IDbBooksRepository> _booksRepositoryMock;
        private Mock<IMapper> _mapperMock;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();

            _genericBookRepositoryMock = new Mock<IDbGenericRepository<Book>>();
            _booksRepositoryMock = new Mock<IDbBooksRepository>();
            _mapperMock = new Mock<IMapper>();

            _bookService = new BooksService(
                _genericBookRepositoryMock.Object,
                _booksRepositoryMock.Object,
                _mapperMock.Object);
        }

        [Test]
        public async Task AddBook_WhenCalled_ShouldCallCreateInRepository()
        {
            var expectedGuid = Guid.NewGuid();

            var bookDto = _fixture.Create<BookDto>();
            var book = new Book()
            {
                Id = bookDto.BookId,
                Author = bookDto.Author,
                Title = bookDto.Title
            };

            _mapperMock.Setup(
               x => x.Map<Book>(bookDto))
               .Returns(book)
               .Verifiable();

            _genericBookRepositoryMock.Setup(
                x => x.Create(book))
                .ReturnsAsync(expectedGuid)
                .Verifiable();

            var actualGuid = await _bookService.CreateBook(bookDto);

            Assert.AreEqual(expectedGuid, actualGuid);

            _genericBookRepositoryMock.Verify();
            _mapperMock.Verify();
        }
    }
}