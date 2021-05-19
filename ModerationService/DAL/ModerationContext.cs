using Microsoft.EntityFrameworkCore;
using ModerationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModerationService.DAL
{
    public class ModerationContext : DbContext
    {
        public DbSet<Request> Requests{ get; set; }

        public DbSet<Report> Reports { get; set; }

        public ModerationContext(DbContextOptions options) : base(options)
        {

        }

        public ModerationContext()
        {

        }
    }
}
