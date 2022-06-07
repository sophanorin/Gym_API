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
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : Controller
    {
        private readonly IStatusService _statusService;
        public StatusController(IStatusService statusService)
        {
            _statusService = statusService;
        }

        [HttpGet]
        public IActionResult GetStatuses()
        {
            return Ok(this._statusService.GetStatuses());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetStatus(string id)
        {
            return Ok(await this._statusService.GetStatus(id));
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteStatus(string id)
        {
            return Ok(this._statusService.DeleteStatus(id));
        }

        [HttpPost]
        public IActionResult AddStatus([FromBody] StatusDto body)
        {
            return Ok(this._statusService.AddStatus(body));
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult AddStatus(string id,[FromBody] StatusDto body)
        {
            return Ok(this._statusService.UpdateStatus(id,body));
        }

    }
}

