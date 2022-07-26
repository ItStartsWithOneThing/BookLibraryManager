using BookLibraryManagerBL.DTOs;
using BookLibraryManagerBL.Models;
using BookLibraryManagerBL.Services.LibrariesService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BookLibraryManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LibrariesController : ControllerBase
    {
        private readonly ILogger<LibrariesController> _logger;
        private readonly ILibrariesService _librariesService;

        public LibrariesController(ILogger<LibrariesController> logger, ILibrariesService librariesService)
        {
            _logger = logger;
            _librariesService = librariesService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateLibrary(LibraryDto library)
        {
            var result = await _librariesService.CreateLibrary(library);

            if (result != Guid.Empty)
            {
                return Created(library.Id.ToString(), library);
            }

            return BadRequest();
        }

        [HttpGet("getNearestLibraries")]
        public async Task<IActionResult> GetNearestLibraries([FromQuery]string cityName, [FromQuery] double latitude, [FromQuery] double longitude, [FromQuery]int amount=5)
        {
            var result = await _librariesService.GetNearestLibraries(cityName, latitude, longitude, amount);

            if(result != null)
            {
                return Ok(result);
            }
            return NotFound(result);
        }
    }
}
