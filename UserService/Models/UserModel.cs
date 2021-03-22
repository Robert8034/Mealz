using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Models
{
    public class UserModel
    {
        public string Email { get; set; }

        public string EmailConfirm { get; set; }

        public string Password { get; set; }

        public string PasswordConfirm { get; set; }
    }
}
