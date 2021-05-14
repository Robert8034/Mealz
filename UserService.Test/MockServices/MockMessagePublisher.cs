using Shared.Messaging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Test.MockServices
{
    public class MockMessagePublisher : IMessagePublisher
    {
        public Task PublishMessageAsync<T>(string messageType, T value)
        {
            return Task.CompletedTask;
        }
    }
}
