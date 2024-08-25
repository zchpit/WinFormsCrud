using CommonLibrary.Dto;
using Microsoft.AspNetCore.Mvc;
using SimpleWebApi.Helpers;
using SimpleWebApi.IServices;

namespace SimpleWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        public async ValueTask<List<ReportDto>> GetReport(int managerId)
        {
            return await serviceManager.ReportService.GetReport(managerId);
        }
    }
}
