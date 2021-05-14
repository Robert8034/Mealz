using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Models;

namespace UserService.Services
{
    public interface IUserService
    {
        bool CheckCredentials(string email, string emailConfirm, string password, string passwordConfirm);

        Task Register(Guid userId, string email, string displayName, string biography);

        User GetUser(Guid userId);

        Task<bool> UpdateUser(User user);

        Task<bool> DeleteUser(User user);
    }
}
