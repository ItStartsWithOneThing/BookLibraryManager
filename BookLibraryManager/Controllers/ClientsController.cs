﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BookLibraryManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly ILogger<ClientsController> _logger;
        //private readonly IClientService _clientService;

        public ClientsController(ILogger<ClientsController> logger)
        {
            _logger = logger;
        }
    }
}
