using ModerationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModerationService.DAL
{
    public interface IModerationCache
    {
        void Initialize(ModerationContext moderationContext);
        bool CheckIfDatabaseIsOffline();
        void SetDatabaseOffline(bool databaseOffline);
        List<Report> GetCachedReports();
        List<Request> GetCachedRequests();
        void SetCachedReports(List<Report> reports);
        void SetCachedRequests(List<Request> requests);
    }
}
