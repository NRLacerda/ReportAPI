using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using ReportAPI.Interfaces;
using ReportAPI.Models;
using ReportAPI.Services;

namespace ReportAPI.Controllers
{
    [ApiController]
    [Route("api/")]
    public class ReportController : ControllerBase
    {
        private readonly ILogger<ReportController> _logger;
        private readonly IReportService reportService;
        public ReportController(ILogger<ReportController> logger)
        {
            reportService = new ReportService();
            _logger = logger;
        }

        [HttpGet(Name ="IsAlive")]
        public bool isAlive()
        {
            return true;
        }

        [HttpPost(Name = "RequestReport")]
        public async Task<StatusCodeResult> RequestReport([FromBody] ReportModel report) 
        {
            if (report == null)
            {
                return BadRequest();
            }

            bool success = await reportService.RequestReport(report);

            if (success) {
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }
    }
}
