using CommonLibrary.Dto;
using Microsoft.AspNetCore.Mvc;
using SimpleWebApi.Helpers;
using SimpleWebApi.IServices;

namespace SimpleWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
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

        [HttpPost(Name = "CreateCase")]
        public async ValueTask CreateCase([FromBody]  CaseCreateDto caseCreateDto)
        {
            await serviceManager.CaseService.CreateCase(caseCreateDto);
        }

        [HttpPut(Name = "UpdateCase")]
        public async ValueTask UpdateCase([FromBody] CaseUpdateDto caseUpdateDto)
        {
            await serviceManager.CaseService.UpdateCase(caseUpdateDto);
        }

        [HttpDelete("{caseId}/{userId}", Name = "DeleteCase")]
        public async ValueTask DeleteCase(int caseId, int userId)
        {
            CaseDeleteDto caseDeleteDto = new CaseDeleteDto() { Id = caseId, DeletedBy = userId, DeletedDate = DateTime.UtcNow };
            await serviceManager.CaseService.DeleteCase(caseDeleteDto);
        }
    }
}
