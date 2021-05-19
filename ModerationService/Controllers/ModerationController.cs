using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModerationService.Models;
using ModerationService.Services;

namespace ModerationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModerationController : ControllerBase
    {
        private readonly IModerationService _moderationService;

        public ModerationController(IModerationService moderationService)
        {
            _moderationService = moderationService;
        }

        [Authorize(Roles = "Moderator,Admin")]
        [HttpGet("getRequests")]
        public IActionResult GetRequests()
        {
            return Ok(_moderationService.GetRequests());
        }

        [Authorize(Roles = "User,Chef,Moderator,Admin")]
        [HttpPost("postRequest")]
        public async Task<IActionResult> PostRequest([FromBody] Request request)
        {
            await _moderationService.PostRequest(request);

            return Ok();
        }

        [Authorize(Roles = "Moderator,Admin")]
        [HttpGet("getReports")]
        public IActionResult GetReports()
        {
            return Ok(_moderationService.GetReports());
        }

        [Authorize(Roles = "User,Chef,Moderator,Admin")]
        [HttpPost("postReport")]
        public async Task<IActionResult> PostReport([FromBody] Report report)
        {
            await _moderationService.PostReport(report);

            return Ok();
        }
    }
}
