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

        public void Register(Guid userId, string email, string password, string displayName, string biography)
        {
            var user = new User()
            {
                UserId = userId,
                Email = email,
                DisplayName = displayName,
                Biography = biography
            };

            _userContext.Add(user);
            _userContext.SaveChanges();
        }

        public User GetUser(Guid userId)
        {
            return _userContext.Users.FirstOrDefault(e => e.UserId == userId);
        }

        public async Task<bool> UpdateUser(User user)
        {
            if (_userContext.Users.FirstOrDefault(e => e.UserId == user.UserId) != null)
            {
                if (!CheckIfEmailIsInUse(user.Email, user.UserId))
                {
                    var originalUser = _userContext.Users.First(e => e.UserId == user.UserId);
                    originalUser.Biography = user.Biography;
                    originalUser.DisplayName = user.DisplayName;
                    originalUser.Email = user.Email;

                    await _userContext.SaveChangesAsync();

                    return true;
                }
            }

            return false;
        }

        private bool CheckIfEmailIsInUse(string email, Guid userId)
        {
            var checkedUser = _userContext.Users.FirstOrDefault(e => e.Email == email);

            if (checkedUser == null) return false;

            if (checkedUser.UserId == userId) return false;

            return true;
        }
    }
}
