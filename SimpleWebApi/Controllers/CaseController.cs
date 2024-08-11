using Microsoft.AspNetCore.Mvc;
using CommonLibrary.Dto;
using SimpleWebApi.Interface;

namespace SimpleWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CaseController : Controller
    {
        private readonly ILogger<CaseController> _logger;
        private readonly ICaseService _caseService;

        public CaseController(ILogger<CaseController> logger, ICaseService caseService)
        {
            _logger = logger;
            _caseService = caseService;
        }

        [HttpGet("{id}/{userRole}")]
        public async ValueTask<ActionResult<List<CaseDto>>> GetUserCases(int id, int userRole)
        {
            RoleDto userRoleDto = RoleDto.User;
            if (userRole <= 2)
                userRoleDto = (RoleDto)userRole;

            return await _caseService.GetUserCases(new SimpleUserDto() { Id = id, UserRole = userRoleDto });
        }

        [HttpPost]
        public async Task PostTodoItem(int userId, CaseDto todoItem)
        {
            await _caseService.UpdateCase(todoItem, userId);
        }
    }
}
