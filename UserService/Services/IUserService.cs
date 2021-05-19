using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Models;

namespace UserService.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Checks if the given emails and passwords are the same. Returns <see langword="true"/> if they are, <see langword="false"/> if they are not.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="emailConfirm"></param>
        /// <param name="password"></param>
        /// <param name="passwordConfirm"></param>
        /// <returns></returns>
        bool CheckCredentials(string email, string emailConfirm, string password, string passwordConfirm);
        /// <summary>
        /// Registers a <see cref="User"/> to the Userservice database. 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="email"></param>
        /// <param name="displayName"></param>
        /// <param name="biography"></param>
        /// <returns></returns>
        Task Register(Guid userId, string email, string displayName, string biography);
        /// <summary>
        /// Gets a user based on a corrosponding <see langword="Guid"/> <paramref name="userId"/>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        User GetUser(Guid userId);
        /// <summary>
        /// Updates the user information in the database to match the provided <paramref name="user"/>
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<bool> UpdateUser(User user);
        /// <summary>
        /// Deletes all data from the provided <paramref name="user"/> in the Userservice database.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<bool> DeleteUser(User user);
    }
}
