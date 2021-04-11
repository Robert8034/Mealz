using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Messaging;
using UserService.Models;
using UserService.Services;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly IMessagePublisher _messagePublisher;

        public UserController(IUserService userService, IMessagePublisher messagePublisher)
        {
            _userService = userService;
           _messagePublisher = messagePublisher;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserModel userModel)
        {
            if (_userService.CheckCredentials(userModel.Email, userModel.EmailConfirm, userModel.Password, userModel.PasswordConfirm)) {
                var userId = Guid.NewGuid();

                _userService.Register(userId, userModel.Email, userModel.Password, userModel.DisplayName, userModel.Biography);

                await _messagePublisher.PublishMessageAsync("UserRegistered", new { UserId = userId, Email = userModel.Email, Password = userModel.Password });
                
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost("getUser")]
        public async Task<IActionResult> GetUser([FromBody] Guid userId)
        {

            if (userId == null) return BadRequest("User ID is invalid");

            var user = _userService.GetUser(userId);

            if (user == null) return BadRequest("User could not be found");

            return Ok(user);
        }

        [HttpPost("updateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] User user)
        {
            if (await _userService.UpdateUser(user)) return Ok();

            return BadRequest("User could not be updated");
        }
    }
}
