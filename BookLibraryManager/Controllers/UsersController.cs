using BookLibraryManagerBL.Models;
using BookLibraryManagerBL.Services.AuthService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BookLibraryManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IAuthService _authService;

        public UsersController(ILogger<UsersController> logger, IAuthService authService)
        {
            _logger = logger;
            _authService = authService;
        }

        [HttpGet]
        public async Task<IActionResult> SignIn(string login, string password)
        {
            string token;

            try
            {
                token = await _authService.SignIn(login, password);
            }

            catch(ArgumentException)
            {
                return Unauthorized();
            }

            return token != null ? Ok(token) : Unauthorized();
        }

        [HttpGet("pass")]
        public async Task<IActionResult> HashAdminSeedPass(string pass)
        {
            bool result = false;

            try
            {
                 result = await _authService.HashAdminSeedPass(pass);
            }

            catch (ArgumentException)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(UserDto user)
        {
            try
            {
                user.Id = await _authService.SignUp(user);
            }

            catch (ArgumentException)
            {
                return Unauthorized();
            }

            return user.Id != Guid.Empty ? Created(user.Id.ToString(), user) : Unauthorized();
        }
    }
}
