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
        private readonly ILoggerManager _logger;
        private readonly IReportService _reportService;

        public ReportController(ILoggerManager logger, IReportService reportService)
        {
            _logger = logger;
            _reportService = reportService;
        }

        [HttpGet("{managerId}")]
        public async ValueTask<List<ReportDto>> GetReport(int managerId)
        {
            return await _reportService.GetReport(managerId);
        }

        [HttpGet("GetException")]
        public IActionResult GetException()
        {
            _logger.LogWarn("User has click GetException method in ReportController");
            throw new Exception("Create test Exception while doing report from the database.");

            return Ok();
        }
    }
}
