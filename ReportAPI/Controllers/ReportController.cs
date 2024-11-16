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
        public async Task<StatusCodeResult> RequestReport([FromQuery] string startAt, [FromQuery] string endAt, [FromQuery] string user) 
        {
            DateTime startDate = DateTime.ParseExact(startAt, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime endDate = DateTime.ParseExact(endAt, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            var reportModel = new ReportModel
            {
                StartAt = startDate,
                EndAt = endDate,
                UserGuid = new Guid (user)
            };

            bool success = await reportService.RequestReport(reportModel);
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
