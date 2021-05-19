using ModerationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModerationService.Services
{
    public interface IModerationService
    {
        List<Request> GetRequests();
        Task PostRequest(Request request);
        List<Report> GetReports();
        Task PostReport(Report report);
    }
}
