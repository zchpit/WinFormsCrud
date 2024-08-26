using CommonLibrary.Dto;
using Microsoft.AspNetCore.Mvc;
using SimpleWebApi.Helpers;
using SimpleWebApi.IServices;

namespace SimpleWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly ILoggerManager logger;
        private readonly IServiceManager serviceManager;

        public UserController(ILoggerManager logger, IServiceManager serviceManager)
        {
            this.logger = logger;
            this.serviceManager = serviceManager;
        }

        [HttpGet("{username}/{password}", Name = "Login")]
        public async ValueTask<ActionResult<SimpleUserDto>> Login(string username, string password)
        {
            logger.LogInfo("test, test");

            var response = await serviceManager.UserService.Login(username, password);
            if (response != null) {
                return response;
            }
            else
            {
                return NoContent();
            }
        }
    }
}
