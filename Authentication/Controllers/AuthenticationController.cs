using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Authentication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;

        private readonly IJwtAuthenticationManager _jwtAuthenticationManager;

        public AuthenticationController(IUserService userService, IJwtAuthenticationManager jwtAuthenticationManager)
        {
            _userService = userService;
            _jwtAuthenticationManager = jwtAuthenticationManager;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserCred userCred)
        {
            var token = _userService.Authenticate(userCred.Email, userCred.Password);

            if (token == null) return Unauthorized();

            return Ok(token);
        }

        [HttpPost("readToken")]
        public IActionResult ReadToken()
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);

            var id = _jwtAuthenticationManager.ReadToken(token);

            return Ok(id);
        }

        [HttpGet("getRole")]
        public IActionResult GetRole()
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);

            var role = _jwtAuthenticationManager.GetRole(token);

            return Ok(role);
        }

        [Authorize(Roles = "Moderator,Admin")]
        [HttpPost("upgradeToChef")]
        public IActionResult UpgradeToChef([FromBody] Guid targetUserId)
        {
            if (targetUserId == Guid.Empty) return BadRequest("User is not valid");

            var targetUser = _userService.GetUser(targetUserId);

            if (targetUser != null)
            {
                if (targetUser.Role == Models.Roles.Chef) return BadRequest("You can't promote someone that is already a chef");

                if (targetUser.Role == Models.Roles.Moderator || targetUser.Role == Models.Roles.Admin) return BadRequest("You can't promote someone to chef that is already a moderator or admin");

                _userService.ChangeUserRole(targetUser, Models.Roles.Chef);

            }

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("upgradeToModerator")]
        public IActionResult UpgradeToModerator([FromBody] Guid targetUserId)
        {
            if (targetUserId == Guid.Empty) return BadRequest("User is not valid");

            var targetUser = _userService.GetUser(targetUserId);

            if (targetUser != null)
            {

                if (targetUser.Role == Models.Roles.Moderator || targetUser.Role == Models.Roles.Admin) return BadRequest("You can't promote someone to moderator that is already a moderator or admin");

                _userService.ChangeUserRole(targetUser, Models.Roles.Moderator);

            }

            return Ok();
        }
    }
}
