using System;
using ReportAPI.Models;

namespace ReportAPI.Interfaces
{
    public interface IReportService
    {
        Task<bool> RequestReport(ReportModel reportReq);
    }
}
