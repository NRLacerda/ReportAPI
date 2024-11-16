using ReportAPI.Interfaces;
using ReportAPI.Models;

namespace ReportAPI.Services
{
    public class ReportService : IReportService
    {
        private readonly IRabbitService _rabbitService;
        public ReportService() 
        { 
            _rabbitService = new RabbitService();
        }
        public async Task<bool> RequestReport(ReportModel reportReq)
        {
            try
            {
                await _rabbitService.SendMessageAsync("reports", reportReq);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending message: {ex.Message}");
                return false;
            }
        }

    }
}
