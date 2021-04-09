using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.DAL;
using UserService.Models;

namespace UserService.Services
{
    public class UserService : IUserService
    {
        private readonly UserContext _userContext;
        public UserService(UserContext userContext)
        {
            _userContext = userContext;
        }
        public bool CheckCredentials(string email, string emailConfirm, string password, string passwordConfirm)
        {
            if ((email == emailConfirm) && (password == passwordConfirm))
            {
                if (_userContext.Users.FirstOrDefault(e => e.Email == email) == null) return true;
            }

            return false;
        }

        public void Register(string email, string password, string displayName, string biography)
        {
            var user = new User()
            {
                Email = email,
                DisplayName = displayName,
                Biography = biography
            };

            _userContext.Add(user);
            _userContext.SaveChanges();
        }

        public User GetUser(int userId)
        {
            return _userContext.Users.FirstOrDefault(e => e.UserId == userId);
        }
    }
}
