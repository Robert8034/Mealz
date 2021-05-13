using Authentication.DAL;
using Authentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Tests.MockServices
{
    public class MockUserDAL : IUserDAL
    {

        public List<User> users;

        public MockUserDAL()
        {
            users = new List<User>();
        }

        public Task AddUser(User user)
        {
            users.Add(user);
            return Task.CompletedTask;
        }

        public Task DeleteUser(User user)
        {
            users.Remove(user);
            return Task.CompletedTask;
        }

        public User GetUserByEmail(string email)
        {
            return users.FirstOrDefault(e => e.Email == email);
        }

        public User GetUserById(Guid userId)
        {
            return users.FirstOrDefault(e => e.UserId == userId);
        }

        public Task Save()
        {
            return Task.CompletedTask;
        }
    }
}
