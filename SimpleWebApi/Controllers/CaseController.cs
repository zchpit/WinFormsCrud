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
        private readonly ILoggerManager logger;
        private readonly IServiceManager serviceManager;

        public CaseController(ILoggerManager logger, IServiceManager serviceManager)
        {
            this.logger = logger;
            this.serviceManager = serviceManager;
        }

        [HttpGet("{id}/{userRole}")]
        public async ValueTask<ActionResult<List<CaseDto>>> GetUserCases(int id, int userRole)
        {
            RoleDto userRoleDto = RoleDto.User;
            if (userRole <= 2)
                userRoleDto = (RoleDto)userRole;

            return await serviceManager.CaseService.GetUserCases(new SimpleUserDto() { Id = id, UserRole = userRoleDto });
        }

        [HttpPost]
        public async Task UpdateCase(int userId, CaseDto caseDto)
        {
            await serviceManager.CaseService.UpdateCase(caseDto, userId);
        }
    }
}
