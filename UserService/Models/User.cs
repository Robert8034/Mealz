using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Models
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string Biography { get; set; }
    }
}
