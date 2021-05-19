using ModerationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModerationService.DAL
{
    public class ModerationDAL : IModerationDAL
    {
        private readonly ModerationContext _moderationContext;

        public ModerationDAL(ModerationContext moderationContext)
        {
            _moderationContext = moderationContext;
        }

        public List<Report> GetReports()
        {
            return _moderationContext.Reports.ToList();
        }

        public List<Request> GetRequests()
        {
            return _moderationContext.Requests.ToList();
        }

        public async Task PostReport(Report report)
        {
            await _moderationContext.Reports.AddAsync(report);

            await _moderationContext.SaveChangesAsync();
        }

        public async Task PostRequest(Request request)
        {
           await _moderationContext.Requests.AddAsync(request);

           await _moderationContext.SaveChangesAsync();
        }
    }
}
