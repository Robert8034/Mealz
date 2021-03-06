using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Mealz.Serverless
{
    public static class ProfanityFilter
    {
        [FunctionName("ProfanityFilter")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["input"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            if (name.Contains("Fuck") || name.Contains("Shit") || name.Contains("Aids") || name.Contains("Asshole") || name.Contains("Cunt") || name.Contains("Bitch")) {
                return new BadRequestResult();
            }

            return new OkResult();
        }
    }
}
