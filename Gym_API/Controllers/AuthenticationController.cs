using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Gym_API.Dto;
using Gym_API.Services.Interfaces;
using Gym_API.Models;
using Gym_API.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Gym_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationService _authenticationService;


        public AuthenticationController(
          IAuthenticationService authenticationService
            )
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto body)
        {
            return Ok(await this._authenticationService.Register(body));
        }

        [HttpPost]
        [Route("RegisterAdmin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] AdminRegisterDto body)
        {
            return Ok(await this._authenticationService.RegisterAdmin(body));
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto body)
        {
            return Ok(await this._authenticationService.Login(body));
        }

        [HttpPut]
        [Route("UpdateRoles/{userId}")]
        public async Task<IActionResult> UpdateUserRoles(string userId, [FromBody] IEnumerable<string> roleNames)
        {
            return Ok(await this._authenticationService.UpdateUserRoles(userId, roleNames));
        }

        [HttpPut]
        [Route("RemoveRoles/{userId}")]
        public async Task<IActionResult> RemoveUserRoles(string userId, [FromBody] IEnumerable<string> roleNames)
        {
            return Ok(await this._authenticationService.RemoveUserRoles(userId, roleNames));
        }
    }
}

