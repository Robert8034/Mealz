using Authentication.Controllers;
using Authentication.Services;
using Shared.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.MessageHandlers
{
    public class UserRegisteredMessageHandler : IMessageHandler<UserCred>
    {
        private readonly IUserService _userService;
        public UserRegisteredMessageHandler(IUserService userService)
        {
            _userService = userService;
        }

        public Task HandleMessageAsync(string messageType, UserCred message)
        {
            _userService.AddUser(message.Email, message.Password);

            return Task.CompletedTask;
        }
    }
}
