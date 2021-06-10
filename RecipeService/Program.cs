using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace RecipeService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            for (int i = 1; i <= 5; i++)
            {
                try
                {
                    CreateHostBuilder(args).Build().Run();
                    break;
                }
                catch (RabbitMQ.Client.Exceptions.BrokerUnreachableException)
                {
                    Console.WriteLine("Connection failed, attempt " + i + "/5");
                    System.Threading.Thread.Sleep(3000);

                    if (i == 5)
                    {
                        Console.WriteLine("Could not successfully connect to RabbitMQ, Broker is offline");
                        break;
                    }
                }
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
