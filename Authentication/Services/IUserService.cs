using Authentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Adds a user based on a given Guid, Email and Password from the Userservice
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        void AddUser(Guid userId, string email, string password);
        /// <summary>
        /// Authenticates a user based on an Email and matching Password.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns>Returns a matching JWT Token</returns>
        string Authenticate(string email, string password);
        /// <summary>
        /// Updates a users Email based on a matching Guid delivered from the Userservice
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="email"></param>
        void ChangeUser(Guid userId, string email);
        /// <summary>
        /// Gets a user based on a Guid
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        User GetUser(Guid userId);
        /// <summary>
        /// Changes a users Role with a matching Guid
        /// </summary>
        /// <param name="user"></param>
        /// <param name="role"></param>
        void ChangeUserRole(User user, Roles role);
        /// <summary>
        /// Deletes all user data from the Authentication database. Gets called from the Userservice
        /// </summary>
        /// <param name="userId"></param>
        void DeleteUser(Guid userId);

        /// <summary>
        /// Configures the admin account role and adds it to the authentication database.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task ConfigureAdmin(Guid userId);
    }
}
