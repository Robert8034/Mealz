using Microsoft.EntityFrameworkCore;
using ModerationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModerationService.DAL
{
    public class ModerationCache : IModerationCache
    {
        public List<Report> reports;

        public List<Request> requests;

        public bool databaseOffline;

        public ModerationCache()
        {
            reports = new List<Report>();
            requests = new List<Request>();
        }

        public void Initialize(ModerationContext moderationContext)
        {
            for (int i = 1; i < 5; i++)
            {
                if (moderationContext.Database.CanConnect())
                {
                    requests = moderationContext.Requests.AsNoTracking().ToList();
                    reports = moderationContext.Reports.AsNoTracking().ToList();

                    databaseOffline = false;
                    break;
                }

                databaseOffline = true;
                Console.WriteLine("Connection failed, attempt " + i + "/5");
                System.Threading.Thread.Sleep(3000);

                if (i == 5)
                {
                    Console.WriteLine("ModerationDB is offline, data could not be cached");
                }
            }
        }

        public bool CheckIfDatabaseIsOffline()
        {
            return databaseOffline;
        }

        public void SetDatabaseOffline(bool databaseOffline)
        {
            this.databaseOffline = databaseOffline;
        }

        public List<Report> GetCachedReports() 
        {
            return reports;
        }
        

        public List<Request> GetCachedRequests()
        {
            return requests;
        }

        public void SetCachedReports(List<Report> reports)
        {
            this.reports = reports;
        }

        public void SetCachedRequests(List<Request> requests)
        {
            this.requests = requests;
        }
    }
}
