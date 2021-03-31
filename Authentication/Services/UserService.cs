using Authentication.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Services
{
    public class UserService : IUserService
    {
        private readonly UserContext _userContext;

        public UserService(UserContext userContext)
        {
            _userContext = userContext;
        }

        public void AddUser(string email, string password)
        {
            _userContext.Users.Add(new Models.User { Email = email, Password = password });
            _userContext.SaveChanges();
        }
    }
}
