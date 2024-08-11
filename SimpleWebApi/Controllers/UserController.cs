using Microsoft.AspNetCore.Mvc;
using SimpleWebApi.Dto;
using SimpleWebApi.Interface;
using SimpleWebApi.Model;

namespace SimpleWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;


        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet("{username}, {password}")]
        public async ValueTask<ActionResult<SimpleUserDto>> Login(string username, string password)
        {
            return await _userService.Login(username, password);
        }
    }
}
