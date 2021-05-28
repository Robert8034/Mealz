using Authentication.Models;
using System;

namespace Authentication.Controllers
{
    public class UserCred
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}