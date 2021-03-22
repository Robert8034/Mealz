using Authentication.Controllers;
using Shared.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.MessageHandlers
{
    public class UserRegisteredMessageHandler : IMessageHandler<UserCred>
    {
        private readonly IJwtAuthenticationManager jwtAuthenticationManager;
        public UserRegisteredMessageHandler(IJwtAuthenticationManager jwtAuthenticationManager)
        {
            this.jwtAuthenticationManager = jwtAuthenticationManager;
        }

        public Task HandleMessageAsync(string messageType, UserCred message)
        {
            jwtAuthenticationManager.AddUser(message.Email, message.Password);

            return Task.CompletedTask;
        }
    }
}
