using Authentication.Controllers;
using Authentication.Services;
using Shared.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.MessageHandlers
{
    public class UserDeletedMessageHandler : IMessageHandler<UserCred>
    {
        private readonly IUserService _userService;
        public UserDeletedMessageHandler(IUserService userService)
        {
            _userService = userService;
        }

        public Task HandleMessageAsync(string messageType, UserCred message)
        {
            _userService.DeleteUser(message.UserId);

            return Task.CompletedTask;
        }
    }
}
