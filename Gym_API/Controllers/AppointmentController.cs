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
    public class AppointmentController : Controller
    {
        public readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult AssignTrainerToGroup(string Id, [FromBody] AppointmentDto body)
        {
            return Ok(this._appointmentService.AssignOrUpdateCoachAppointment(Id, body));
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetAppointment(string Id)
        {
            return Ok(this._appointmentService.GetAppointment(Id));
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteAppointment(string Id)
        {
            return Ok(this._appointmentService.DeleteAppointment(Id));
        }

        [HttpGet]
        public IActionResult GetAppointments()
        {
            return Ok(this._appointmentService.GetAppointments());
        }

        [HttpPost]
        public IActionResult AddAppointment([FromBody] AppointmentDto body)
        {
            return Ok(this._appointmentService.AddAppointment(body));
        }

        [HttpGet]
        [Route("coach/{id}")]
        public IActionResult GetCoachAppointments(string Id)
        {
            return Ok(this._appointmentService.GetCaochAppointments(Id));
        }

        [HttpGet]
        [Route("customer/{id}")]
        public IActionResult GetCustomerAppointments(string Id)
        {
            return Ok(this._appointmentService.GetCustomerAppointments(Id));
        }
    }
}

