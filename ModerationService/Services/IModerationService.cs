using ModerationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModerationService.Services
{
    public interface IModerationService
    {
        Task<List<Request>> GetRequests();
        Task PostRequest(Request request);
        Task<List<Report>> GetReports();
        Task<List<Report>> GetMyReports(Guid id);
        Task PostReport(Report report);
        Task ApproveRequest(Request request);
        Task DeclineRequest(Request request);
        Task RemoveReport(Report report);
    }
}
