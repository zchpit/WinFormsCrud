using Microsoft.AspNetCore.Mvc;
using SimpleWebApi.Helpers;

namespace SimpleWebApi.Controllers
{

    namespace SimpleWebApi.Controllers
    {
        [ApiController]
        [Route("[controller]")]
        public class ExceptionTestController : Controller
        {
            private readonly ILoggerManager _logger;

            public ExceptionTestController(ILoggerManager logger)
            {
                _logger = logger;
            }

            [HttpGet("GetException")]
            public IActionResult GetException()
            {
                _logger.LogWarn("User has click GetException method in ReportController");
                throw new Exception("Create test Exception while doing report from the database.");

                return Ok();
            }

            [HttpGet("GetAccessViolationException")]
            public IActionResult GetAccessViolationException()
            {
                _logger.LogWarn("User has click GetException method in ReportController");
                throw new AccessViolationException("Create test AccessViolationException while doing report from the database.");

                return Ok();
            }
        }
    }
}