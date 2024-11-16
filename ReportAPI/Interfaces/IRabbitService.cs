using ReportAPI.Models;

namespace ReportAPI.Interfaces
{
    public interface IRabbitService
    {
        Task SendMessageAsync(string queueName, ReportModel reportReq);
    }
}
