using BookLibraryManagerBL;
using BookLibraryManagerBL.Models;
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
        private readonly BookService _booksService;

        public BooksController(ILogger<BooksController> logger, BookService booksService)
        {
            _logger = logger;
            _booksService = booksService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook(Book book)
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(Guid id)
        {
            return Ok();
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllBooks()
        {
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(Guid id, Book book)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            return Ok();
        }
    }
}
