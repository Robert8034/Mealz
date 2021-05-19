using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModerationService.Models
{
    public class Report
    {
        public Guid ReportId { get; set; }
        public Guid PostId { get; set; }
        public Guid ReporterId { get; set; }
    }
}
