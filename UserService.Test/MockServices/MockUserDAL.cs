using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserService.DAL;
using UserService.Models;
using System.Linq;

namespace UserService.Test.MockServices
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
