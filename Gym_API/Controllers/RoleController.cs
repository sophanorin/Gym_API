using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
    public class RoleController : Controller
    {

        private IRoleService _roleService { get; set; }

        public RoleController(IRoleService roleService)
        {
            this._roleService = roleService;
        }

        [HttpPost]
        public async Task<IActionResult> AddRole([FromBody] RoleDto body)
        {
            return Ok(await this._roleService.AddRole(body));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            return Ok(await this._roleService.DeleteRole(id));
        }

        public IActionResult getRoles()
        {
            return Ok(this._roleService.GetRoles());
        }
    }
}

