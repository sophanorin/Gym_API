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
    public class GenderController : Controller
    {
        private readonly IGenderService _genderService;

        public GenderController(IGenderService genderService)
        {
            _genderService = genderService;
        }

        [HttpGet]
        public IActionResult GetGenders()
        {
            return Ok(this._genderService.GetGenders());
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteGender(string id)
        {
            return Ok(this._genderService.DeleteGender(id));;
        }

        [HttpPost]
        public IActionResult AddGender([FromBody] GenderDto body)
        {
            return Ok(this._genderService.AddGender(body));
        }
    }
}

