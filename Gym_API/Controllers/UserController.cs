using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gym_API.Dto;
using Gym_API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Gym_API.Controllers
{
    [Authorize]
    [Route("api/{controller}")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserInfos(string type)
        {
            return Ok(await _userService.GetUserInfos(type));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUserInfo(string Id = "customer")
        {
            return Ok(await _userService.GetUserInfo(Id));
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateUserInfo(string Id, [FromBody] UserInfoDto body)
        {
            return Ok(await _userService.UpdateUserInfo(Id, body));
        }

        [HttpGet]
        [Route("roles/{id}")]
        public async Task<IActionResult> GetUserRoles(string Id)
        {
            return Ok(await _userService.GetUserRoles(Id));
        }
    }
}

