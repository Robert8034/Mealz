using Shared.Messaging;
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
        private readonly IUserDAL _userDAL;
        private readonly IMessagePublisher _messagePublisher;

        public UserService(IUserDAL userDAL, IMessagePublisher messagePublisher)
        {
            _userDAL = userDAL;
            _messagePublisher = messagePublisher;
        }

        public bool CheckCredentials(string email, string emailConfirm, string password, string passwordConfirm)
        {
            if ((email == emailConfirm) && (password == passwordConfirm) && (_userDAL.GetUserByEmail(email) == null)) return true;

            return false;
        }

        public async Task Register(Guid userId, string email, string displayName, string biography)
        {
            var user = new User()
            {
                UserId = userId,
                Email = email,
                DisplayName = displayName,
                Biography = biography
            };

            await _userDAL.AddUser(user);
        }

        public User GetUser(Guid userId)
        {
            return _userDAL.GetUserById(userId);
        }

        public async Task<bool> UpdateUser(User user)
        {
            var originalUser = _userDAL.GetUserById(user.UserId);

            if ((originalUser != null) && (!CheckIfEmailIsInUse(user.Email, user.UserId)))
            { 
                originalUser.Biography = user.Biography;
                originalUser.DisplayName = user.DisplayName;

                if (originalUser.Email != user.Email)
                {
                    originalUser.Email = user.Email;
                    await _messagePublisher.PublishMessageAsync("UserChanged", new { user.UserId, user.Email, Password = "" });
                }

                await _userDAL.Save();

                return true;  
            }

            return false;
        }

        private bool CheckIfEmailIsInUse(string email, Guid userId)
        {
            var checkedUser = _userDAL.GetUserByEmail(email);

            if (checkedUser == null) return false;

            if (checkedUser.UserId == userId) return false;

            return true;
        }

        public async Task<bool> DeleteUser(User user)
        {
            await _userDAL.DeleteUser(user);

            if (_userDAL.GetUserById(user.UserId) == null)
            {
                await _messagePublisher.PublishMessageAsync("UserDeleted", new { user.UserId });
                return true;
            }
            return false;
        }

        public async Task ConfigureAdmin(Guid id)
        {  
           await _messagePublisher.PublishMessageAsync("AdminAdded", new { id }); 
        }
    }
}
