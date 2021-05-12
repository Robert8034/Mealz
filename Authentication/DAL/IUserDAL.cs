using Authentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.DAL
{
    public interface IUserDAL
    {
        Task AddUser(User user);

        User GetUserByEmail(string email);

        User GetUserById(Guid userId);

        Task Save();

        Task DeleteUser(User user);
    }
}
