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

        // GET: api/<NameController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "New Jersey", "New York" };
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
    }
}
