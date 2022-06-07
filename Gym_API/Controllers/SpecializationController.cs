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
    public class SpecializationController : Controller
    {
        private readonly ISpecializationService _specializationService;
        public SpecializationController(ISpecializationService specializationService)
        {
            _specializationService = specializationService;
        }

        [HttpGet]
        public IActionResult GetSpecializations()
        {
            return Ok(this._specializationService.GetSpecializations());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetSpecialization(string id)
        {
            return Ok(await this._specializationService.GetSpecialization(id));
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteSpecialization(string id)
        {
            return Ok(this._specializationService.DeleteSpecialization(id));
        }

        [HttpPost]
        public IActionResult AddStatus([FromBody] SpecializatinDto body)
        {
            return Ok(this._specializationService.AddSpecialization(body));
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult AddStatus(string id, [FromBody] SpecializatinDto body)
        {
            return Ok(this._specializationService.UpdateSpecialization(id, body));
        }

    }
}

