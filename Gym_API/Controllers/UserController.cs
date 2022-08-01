using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gym_API.Dto;
using Gym_API.Services.Interfaces;
using Gym_API.Shared;
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
        public async Task<IActionResult> GetUserInfos([FromQuery] UserInfoQuery query)
        {
            return Ok(await _userService.GetUserInfos(query));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUserInfo(string Id)
        {
            return Ok(await _userService.GetUserInfoAsync(Id));
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

        [Authorize(Roles = UserRoles.SeniorSupervisor)]
        [HttpPut]
        [Route("UpdateRoles/{userId}")]
        public async Task<IActionResult> UpdateUserRoles(string userId, [FromBody] IEnumerable<string> roleNames)
        {
            return Ok(await this._userService.UpdateUserRoles(userId, roleNames));
        }

        [Authorize(Roles = UserRoles.SeniorSupervisor)]
        [HttpPut]
        [Route("RemoveRoles/{userId}")]
        public async Task<IActionResult> RemoveUserRoles(string userId, [FromBody] IEnumerable<string> roleNames)
        {
            return Ok(await this._userService.RemoveUserRoles(userId, roleNames));
        }
    }
}

