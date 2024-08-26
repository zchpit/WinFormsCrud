using CommonLibrary.Dto;
using Microsoft.AspNetCore.Mvc;
using SimpleWebApi.ActionFilters;
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
        public async ValueTask<ActionResult<List<CaseDto>>> GetUserCases(int id, RoleDto userRole)
        {
            return await serviceManager.CaseService.GetUserCases(new SimpleUserDto() { Id = id, UserRole = userRole });
        }

        [HttpPost(Name = "CreateCase")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async ValueTask CreateCase([FromBody] CaseCreateDto caseCreateDto)
        {
            await serviceManager.CaseService.CreateCase(caseCreateDto);
        }

        [HttpPut(Name = "UpdateCase")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
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
