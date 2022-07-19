using BookLibraryManagerBL.BooksService.Services;
using BookLibraryManagerBL.DTOs;
using BookLibraryManagerDAL;
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

        #region Post
        [HttpPost]
        public async Task<IActionResult> CreateBook(BookDto book)
        {
            try
            {
                var result = await _booksService.AddBook(book);

                book.BookId = result;

                return Created(result.ToString(), book);
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

        //[HttpGet("getAll")]
        //public async Task<IActionResult> GetAllBooks()
        //{
        //    return Ok();
        //}

        [HttpGet("fullInfo/{id}")]
        public async Task<IActionResult> GetFullInfo(Guid id)
        {
            var result = await _booksService.GetFullInfo(id);

            return Ok(result);
        }
        #endregion

        #region Put
        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateBook(Guid id, BookDto book)
        //{
        //    return Ok();
        //}
        #endregion

        #region Delete
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteBook(Guid id)
        //{
        //    return Ok();
        //}
        #endregion
    }
}
