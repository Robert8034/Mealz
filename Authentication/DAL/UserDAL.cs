using Authentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.DAL
{
    public class UserDAL : IUserDAL
    {
        private readonly UserContext _userContext;
        public UserDAL(UserContext userContext)
        {
            _userContext = userContext;
        }

        public async Task AddUser(User user)
        {
            _userContext.Add(user);
            await _userContext.SaveChangesAsync();
        }

        public User GetUserByEmail(string email)
        {
            return _userContext.Users.FirstOrDefault(e => e.Email == email);
        }

        public User GetUserById(Guid userId)
        {
            return _userContext.Users.FirstOrDefault(e => e.UserId == userId);
        }

        public async Task Save()
        {
            await _userContext.SaveChangesAsync();
        }

        public async Task DeleteUser(User user)
        {
            _userContext.Remove(user);
            await _userContext.SaveChangesAsync();
        }
    }
}
