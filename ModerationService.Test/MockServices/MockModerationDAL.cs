using ModerationService.DAL;
using ModerationService.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ModerationService.Test.MockServices
{
    public class MockModerationDAL : IModerationDAL
    {
        public List<Report> reports;
        public List<Request> requests;

        public MockModerationDAL()
        {
            reports = new List<Report>();
            requests = new List<Request>();
        }

        public Task<List<Report>> GetReports()
        {
            return Task.FromResult(reports);
        }

        public Task<List<Report>> GetReportsByReporterId(Guid id)
        {
            return Task.FromResult(reports.FindAll(e => e.ReporterId == id));
        }

        public Task<List<Request>> GetRequests()
        {
            return Task.FromResult(requests);
        }

        public Task PostReport(Report report)
        {
            reports.Add(report);
            return Task.CompletedTask;
        }

        public Task PostRequest(Request request)
        {
            requests.Add(request);
            return Task.CompletedTask;
        }

        public Task RemoveReport(Report report)
        {
            reports.Remove(report);
            return Task.CompletedTask;
        }

        public Task RemoveRequest(Request request)
        {
            requests.Remove(request);
            return Task.CompletedTask;
        }
    }
}
