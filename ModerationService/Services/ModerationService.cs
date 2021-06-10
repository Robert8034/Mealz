using ModerationService.DAL;
using ModerationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModerationService.Services
{
    public class ModerationService : IModerationService
    {
        private readonly IModerationDAL _moderationDAL;

        public ModerationService(IModerationDAL moderationDAL)
        {
            _moderationDAL = moderationDAL;
        }

        public async Task ApproveRequest(Request request)
        {
            await _moderationDAL.RemoveRequest(request);
        }

        public async Task<List<Report>> GetReports()
        {
            return await _moderationDAL.GetReports();
        }

        public async Task<List<Report>> GetMyReports(Guid id)
        {
            return await _moderationDAL.GetReportsByReporterId(id);
        }

        public async Task<List<Request>> GetRequests()
        {
            return await _moderationDAL.GetRequests();
        }

        public async Task PostReport(Report report)
        {
            await _moderationDAL.PostReport(report);
        }

        public async Task PostRequest(Request request)
        {
            await _moderationDAL.PostRequest(request);
        }

        public async Task DeclineRequest(Request request)
        {
            await _moderationDAL.RemoveRequest(request);
        }

        public async Task RemoveReport(Report report)
        {
            await _moderationDAL.RemoveReport(report);
        }
    }
}
