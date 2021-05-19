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

        public List<Report> GetReports()
        {
            return _moderationDAL.GetReports();
        }

        public List<Request> GetRequests()
        {
            return _moderationDAL.GetRequests();
        }

        public async Task PostReport(Report report)
        {
            await _moderationDAL.PostReport(report);
        }

        public async Task PostRequest(Request request)
        {
            await _moderationDAL.PostRequest(request);
        }
    }
}
