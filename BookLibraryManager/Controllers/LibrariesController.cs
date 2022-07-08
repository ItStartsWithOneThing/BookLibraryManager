using BookLibraryManagerBL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BookLibraryManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LibrariesController : ControllerBase
    {
        private readonly ILogger<LibrariesController> _logger;
        //private readonly ILibraryService _libraryService;

        public LibrariesController(ILogger<LibrariesController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Create(Library _library)
        {

            return null;
        }
    }
}
