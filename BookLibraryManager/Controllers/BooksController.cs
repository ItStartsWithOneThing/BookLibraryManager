using BookLibraryManagerBL.BooksService.Services;
using BookLibraryManagerBL.DTOs;
using BookLibraryManagerDAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BookLibraryManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly ILogger<BooksController> _logger;
        private readonly IBooksService _booksService;

        public BooksController(ILogger<BooksController> logger, IBooksService booksService)
        {
            _logger = logger;
            _booksService = booksService;
        }

        #region StandartCRUD

        #region Post
        [HttpPost]
        public async Task<IActionResult> CreateBook(BookDto book)
        {
            try
            {
                var result = await _booksService.CreateBook(book);

                if(!result.Equals(Guid.Empty))
                {
                    book.BookId = result;

                    return Created(result.ToString(), book);
                }

                return NotFound();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Get
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(Guid id)
        {
            var book = await _booksService.GetBookById(id);

            if(book != null)
            {
                return Ok(book);
            }

            return  NotFound(id);
        }

        //[Authorize(Roles = BookLibraryManagerBL.Auth.Roles.Librarian)] 
        [Authorize(Roles = BookLibraryManagerBL.Auth.Roles.Reader+", "+ BookLibraryManagerBL.Auth.Roles.Librarian)] // Any Role, of list
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllBooks()
        {
            var result = await _booksService.GetAllBooks();

            if(result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        #endregion

        #region Put

        [HttpPut]
        public async Task<IActionResult> UpdateBook(BookDto book)
        {
            var result = await _booksService.UpdateBook(book);

            if(result)
            {
                return Ok();
            }

            return NotFound();
        }

        #endregion

        #region Delete

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            var result = await _booksService.DeleteBook(id);

            return Ok();
        }

        #endregion

        #endregion StandartCRUD

        #region Get

        [HttpGet("fullBookInfo/{id}")]
        public async Task<IActionResult> GetFullBookInfo(Guid id) //TODO TEST MY QUERRY
        {
            var result = await _booksService.GetFullBookInfo(id);

            return Ok(result);
        }

        [HttpGet("authorBooks/{author}")]
        public async Task<IActionResult> GetAuthorBooks(string author)
        {
            var result = await _booksService.GetBooksByAuthor(author);

            return Ok(result);
        }

        #endregion
    }
}
