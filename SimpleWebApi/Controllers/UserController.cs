using CommonLibrary.Dto;
using Microsoft.AspNetCore.Mvc;
using SimpleWebApi.Helpers;
using SimpleWebApi.IServices;

namespace SimpleWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IUserService _userService;


        public UserController(ILoggerManager logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet("{username}/{password}")]
        public async ValueTask<ActionResult<SimpleUserDto>> Login(string username, string password)
        {
            _logger.LogInfo("test, test");
            return await _userService.Login(username, password);
        }
    }
}
