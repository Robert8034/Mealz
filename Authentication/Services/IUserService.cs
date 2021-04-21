using Authentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Services
{
    public interface IUserService
    {
        void AddUser(Guid userId, string email, string password);
        string Authenticate(string email, string password);
        void ChangeUser(Guid userId, string email);
        User GetUser(Guid userId);
        void ChangeUserRole(User user, Roles role);
    }
}
