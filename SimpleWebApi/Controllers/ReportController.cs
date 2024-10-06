using CommonLibrary.Dto;
using Microsoft.AspNetCore.Mvc;
using SimpleWebApi.Helpers;
using SimpleWebApi.IServices;

namespace SimpleWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ReportController : Controller
    {
        private readonly ILoggerManager logger;
        private readonly IServiceManager serviceManager;

        public ReportController(ILoggerManager logger, IServiceManager serviceManager)
        {
            this.logger = logger;
            this.serviceManager = serviceManager;
        }

        [HttpGet("{managerId}")]
        public ValueTask<List<ReportDto>> GetReport(int managerId)
        {
            return serviceManager.ReportService.GetReport(managerId);
        }
    }
}
