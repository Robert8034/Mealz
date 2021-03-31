using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Services
{
    public class UserService : IUserService
    {
        public bool checkCredentials(string email, string emailConfirm, string password, string passwordConfirm)
        {
            if ((email == emailConfirm) && (password == passwordConfirm)) return true;

            return false;
        }

        public void register(string email, string password)
        {
        }
    }
}
