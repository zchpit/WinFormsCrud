using CommonLibrary.Dto;
using Microsoft.AspNetCore.Mvc;
using SimpleWebApi.Helpers;
using SimpleWebApi.IServices;

namespace SimpleWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CaseController : Controller
    {
        private readonly ILoggerManager _logger;
        private readonly ICaseService _caseService;

        public CaseController(ILoggerManager logger, ICaseService caseService)
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
        public async Task UpdateCase(int userId, CaseDto caseDto)
        {
            await _caseService.UpdateCase(caseDto, userId);
        }
    }
}
