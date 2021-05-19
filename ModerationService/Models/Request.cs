using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModerationService.Models
{
    public class Request
    {
        public Guid RequestId { get; set; }
        public Guid UserId { get; set; }
        public RequestType RequestType { get; set; }
    }
}
