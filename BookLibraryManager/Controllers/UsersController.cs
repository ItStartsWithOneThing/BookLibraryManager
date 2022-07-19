using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BookLibraryManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        //private readonly IClientService _clientService;

        public UsersController(ILogger<UsersController> logger)
        {
            _logger = logger;
        }
    }
}
