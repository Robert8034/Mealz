using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Services
{
    public interface IUserService
    {
        void AddUser(string email, string password);
    }
}
