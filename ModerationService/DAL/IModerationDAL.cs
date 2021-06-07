using ModerationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModerationService.DAL
{
    public interface IModerationDAL
    {
        List<Request> GetRequests();

        Task PostRequest(Request request);

        Task RemoveReport(Report report);

        List<Report> GetReports();

        List<Report> GetReportsByReporterId(Guid id);

        Task PostReport(Report report);

        Task RemoveRequest(Request request);
    }
}
