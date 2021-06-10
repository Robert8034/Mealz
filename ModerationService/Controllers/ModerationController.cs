using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModerationService.Models;
using ModerationService.Services;
using Shared.Messaging;

namespace ModerationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModerationController : ControllerBase
    {
        private readonly IModerationService _moderationService;
        private readonly IMessagePublisher _messagePublisher;

        public ModerationController(IModerationService moderationService, IMessagePublisher messagePublisher)
        {
            _moderationService = moderationService;
            _messagePublisher = messagePublisher;
        }

        [Authorize(Roles = "Moderator,Admin")]
        [HttpGet("getRequests")]
        public async Task<IActionResult> GetRequests()
        {
            return Ok(await _moderationService.GetRequests());
        }

        [Authorize(Roles = "User,Chef,Moderator,Admin")]
        [HttpPost("postRequest")]
        public async Task<IActionResult> PostRequest([FromBody] Request request)
        {
            await _moderationService.PostRequest(request);

            return Ok();
        }

        [Authorize(Roles = "Moderator,Admin")]
        [HttpPost("approveRequestToChef")]
        public async Task<IActionResult> ApproveRequestToChef([FromBody] Request request)
        {
            if (request.RequestType == RequestType.Upgrade_To_Chef)
            {
                await _moderationService.ApproveRequest(request);

                await _messagePublisher.PublishMessageAsync("UserRoleUpdated", new { request.UserId, Role = 1 });

                return Ok();
            }

            return BadRequest();
        }

        [Authorize(Roles = "Moderator,Admin")]
        [HttpPost("approveRequestToModerator")]
        public async Task<IActionResult> ApproveRequestToModerator([FromBody] Request request)
        {
            if (request.RequestType == RequestType.Upgrade_To_Moderator)
            {
                await _moderationService.ApproveRequest(request);

                await _messagePublisher.PublishMessageAsync("UserRoleUpdated", new { request.UserId, Role = 2 });

                return Ok();
            }

            return BadRequest();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("approveRequestToAdmin")]
        public async Task<IActionResult> ApproveRequestToAdmin([FromBody] Request request)
        {
            if (request.RequestType == RequestType.Upgrade_To_Admin)
            {
                await _moderationService.ApproveRequest(request);

                await _messagePublisher.PublishMessageAsync("UserRoleUpdated", new { request.UserId, Role = 3 });

                return Ok();
            }

            return BadRequest();
        }

        [Authorize(Roles = "Moderator,Admin")]
        [HttpPost("declineRequestToChef")]
        public async Task<IActionResult> DeclineRequestToChef([FromBody] Request request)
        {
            if (request.RequestType == RequestType.Upgrade_To_Chef)
            {
                await _moderationService.DeclineRequest(request);

                return Ok();
            }

            return BadRequest();
        }

        [Authorize(Roles = "Moderator,Admin")]
        [HttpPost("declineRequestToModerator")]
        public async Task<IActionResult> DeclineRequestToModerator([FromBody] Request request)
        {
            if (request.RequestType == RequestType.Upgrade_To_Moderator)
            {
                await _moderationService.DeclineRequest(request);

                return Ok();
            }

            return BadRequest();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("declineRequestToAdmin")]
        public async Task<IActionResult> DeclineRequestToAdmin([FromBody] Request request)
        {
            if (request.RequestType == RequestType.Upgrade_To_Admin)
            {
                await _moderationService.DeclineRequest(request);

                return Ok();
            }

            return BadRequest();
        }

        [Authorize(Roles = "Moderator,Admin")]
        [HttpGet("getReports")]
        public async Task<IActionResult> GetReports()
        {
            return Ok(await _moderationService.GetReports());
        }


        [Authorize(Roles = "User,Chef,Moderator,Admin")]
        [HttpPost("getMyReports")]
        public async Task<IActionResult> GetMyReports([FromBody] Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid ID");
            }

            return Ok(await _moderationService.GetMyReports(id));
        }

        [Authorize(Roles = "User,Chef,Moderator,Admin")]
        [HttpPost("postReport")]
        public async Task<IActionResult> PostReport([FromBody] Report report)
        {
            if (report.PostId == Guid.Empty) return BadRequest("Invalid Post");
            if (report.ReporterId == Guid.Empty) return BadRequest("Invalid User");
            
            await _moderationService.PostReport(report);

            return Ok();
        }

        [Authorize(Roles = "Moderator,Admin")]
        [HttpPost("removeReport")]
        public async Task<IActionResult> RemoveReport([FromBody] Report report)
        {
            await _moderationService.RemoveReport(report);

            return Ok();
        }

        [Authorize(Roles = "Moderator,Admin")]
        [HttpPost("removeRecipe")]
        public async Task<IActionResult> RemoveRecipe([FromBody] Report report)
        {
            await _messagePublisher.PublishMessageAsync("RemoveRecipe", report.PostId.ToString());

            await _moderationService.RemoveReport(report);

            return Ok();
        }
    }
}
