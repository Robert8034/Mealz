using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Messaging
{
    internal class RabbitMqConfiguration
    {
        private readonly RabbitMqConnection _connection;
        private bool configured = false;

        public RabbitMqConfiguration(RabbitMqConnection connection)
        {
            _connection = connection;
        }

        public void ConfigureRabbit()
        {
            if (!configured)
            {
                var channel = _connection.CreateChannel();

                channel.ExchangeDeclare("mealz", "fanout");

                channel.QueueDeclare("Authentication Service", false, false);
                channel.QueueDeclare("User Service", false, false);
                channel.QueueDeclare("Recipe Service", false, false);

                channel.QueueBind("Authentication Service", "mealz", "UserRegistered");
                channel.QueueBind("Authentication Service", "mealz", "UserChanged");

                configured = true;
            }
        }
    }
}
