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
using Microsoft.AspNetCore.Authorization;

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
        [Route("RegisterCustomer")]
        public async Task<IActionResult> RegisterCustomer([FromBody] RegisterCustomerDto body)
        {
            return Ok(await this._authenticationService.RegisterCustomer(body));
        }

        [Authorize(Roles = UserRoles.SeniorSupervisor)]
        [HttpPost]
        [Route("RegisterStuff")]
        public async Task<IActionResult> RegisterStuff([FromBody] RegisterStuffDto body)
        {
            return Ok(await this._authenticationService.RegisterStuff(body));
        }

        [HttpPost]
        [Route("RegisterSeniorSupervisor")]
        public async Task<IActionResult> RegisterSeniorSupervisor([FromBody] RegisterStuffDto body)
        {
            return Ok(await this._authenticationService.RegisterSeniorSupervisor(body));
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto body)
        {
            return Ok(await this._authenticationService.Login(body));
        }
    }
}

