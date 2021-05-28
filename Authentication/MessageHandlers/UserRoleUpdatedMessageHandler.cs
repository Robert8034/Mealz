using Authentication.Models;
using Authentication.Services;
using Shared.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.MessageHandlers
{
    public class UserRoleUpdatedMessageHandler : IMessageHandler<User>
    {
        private readonly IUserService _userService;
        public UserRoleUpdatedMessageHandler(IUserService userService)
        {
            _userService = userService;
        }

        public Task HandleMessageAsync(string messageType, User message)
        {
            var user = _userService.GetUser(message.UserId);

            _userService.ChangeUserRole(user, message.Role);

            return Task.CompletedTask;
        }
    }
}
