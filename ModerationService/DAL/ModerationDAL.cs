using Microsoft.EntityFrameworkCore;
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

        private readonly IModerationCache _moderationCache;

        public ModerationDAL(ModerationContext moderationContext, IModerationCache moderationCache)
        {
            _moderationContext = moderationContext;

            _moderationCache = moderationCache;
        }

        public async Task<List<Report>> GetReports()
        {
            
            if (_moderationContext.Database.CanConnect())
            {
                await SyncReportData();

                return _moderationContext.Reports.ToList();
            }
          
            if (!_moderationCache.CheckIfDatabaseIsOffline()) _moderationCache.SetDatabaseOffline(true);

            return _moderationCache.GetCachedReports();
        }

        public async Task<List<Report>> GetReportsByReporterId(Guid id)
        {
            if (_moderationContext.Database.CanConnect())
            {
                await SyncReportData();

                return _moderationContext.Reports.Where(e => e.ReporterId == id).ToList();
            }

            if (!_moderationCache.CheckIfDatabaseIsOffline()) _moderationCache.SetDatabaseOffline(true);

            return _moderationCache.GetCachedReports().Where(e => e.ReporterId == id).ToList();
        }

        public async Task<List<Request>> GetRequests()
        {
            if (_moderationContext.Database.CanConnect())
            {
                await SyncRequestData();

                return _moderationContext.Requests.ToList();
            }
        
            if (!_moderationCache.CheckIfDatabaseIsOffline()) _moderationCache.SetDatabaseOffline(true);

            return _moderationCache.GetCachedRequests();
        }

        public async Task PostReport(Report report)
        {
            if (_moderationContext.Database.CanConnect())
            {
                if (_moderationCache.CheckIfDatabaseIsOffline()) await SyncReportData();

                await _moderationContext.Reports.AddAsync(report);

                await _moderationContext.SaveChangesAsync();

                await SyncReportData();
            }

            if (!_moderationCache.CheckIfDatabaseIsOffline()) _moderationCache.SetDatabaseOffline(true);

            _moderationCache.GetCachedReports().Add(report);
        }

        public async Task PostRequest(Request request)
        {
            if (_moderationContext.Database.CanConnect())
            {
                if (_moderationCache.CheckIfDatabaseIsOffline()) await SyncRequestData();

                await _moderationContext.Requests.AddAsync(request);

                await _moderationContext.SaveChangesAsync();

                await SyncRequestData();
            }

            if (!_moderationCache.CheckIfDatabaseIsOffline()) _moderationCache.SetDatabaseOffline(true);

            _moderationCache.GetCachedRequests().Add(request);
        }

        public async Task RemoveReport(Report report)
        {
            if (_moderationContext.Database.CanConnect())
            {
                _moderationContext.Reports.Remove(report);

                await _moderationContext.SaveChangesAsync();

                await SyncReportData();
            }

            if (!_moderationCache.CheckIfDatabaseIsOffline()) _moderationCache.SetDatabaseOffline(true);

            var cachedReports = _moderationCache.GetCachedReports();

            var target = cachedReports.FirstOrDefault(e => e.ReportId == report.ReportId);
            cachedReports.Remove(target);

            _moderationCache.SetCachedReports(cachedReports);
        }

        public async Task RemoveRequest(Request request)
        {
            if (_moderationContext.Database.CanConnect())
            {
                _moderationContext.Requests.Remove(request);

                await _moderationContext.SaveChangesAsync();

                await SyncRequestData();
            }

            if (!_moderationCache.CheckIfDatabaseIsOffline()) _moderationCache.SetDatabaseOffline(true);

            var cachedRequests = _moderationCache.GetCachedRequests();

            cachedRequests.Remove(request);

            _moderationCache.SetCachedRequests(cachedRequests);
        }

        private async Task SyncReportData()
        {
            if (_moderationCache.CheckIfDatabaseIsOffline())
            {
                _moderationContext.UpdateRange(_moderationCache.GetCachedReports());
                await _moderationContext.SaveChangesAsync();
                _moderationCache.SetDatabaseOffline(false);
            }
            else
            {
                var databaseReports = _moderationContext.Reports.ToList();

                _moderationCache.SetCachedReports(databaseReports);
            }
        }

        private async Task SyncRequestData()
        {
            if (_moderationCache.CheckIfDatabaseIsOffline())
            {
                _moderationContext.UpdateRange(_moderationCache.GetCachedRequests());
                await _moderationContext.SaveChangesAsync();
                _moderationCache.SetDatabaseOffline(false);
            }
            else
            {
                _moderationCache.SetCachedRequests(_moderationContext.Requests.ToList());
            }
        }
    }
}
