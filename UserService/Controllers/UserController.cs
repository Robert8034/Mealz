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

        private readonly IUserService userService;
        private readonly IMessagePublisher messagePublisher;

        public UserController(IUserService userService, IMessagePublisher messagePublisher)
        {
            this.userService = userService;
            this.messagePublisher = messagePublisher;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserModel userModel)
        {
            if (userService.CheckCredentials(userModel.Email, userModel.EmailConfirm, userModel.Password, userModel.PasswordConfirm)) {
                userService.Register(userModel.Email, userModel.Password, userModel.DisplayName, userModel.Biography);

                await messagePublisher.PublishMessageAsync("UserRegistered", new { Email = userModel.Email, Password = userModel.Password });
                
                return Ok();
            }

            return BadRequest();
        }
    }
}
