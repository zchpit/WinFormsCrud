using Microsoft.AspNetCore.Mvc;
using SimpleWebApi.Interface;

namespace SimpleWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CaseController : Controller
    {
        private readonly ILogger<CaseController> _logger;
        private readonly ICaseService _userService;

        public CaseController(ILogger<CaseController> logger, ICaseService caseService)
        {
            _logger = logger;
            _userService = caseService;
        }



    }
}
