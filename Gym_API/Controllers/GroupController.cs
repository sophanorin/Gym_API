using System;
using Gym_API.Dto;
using Gym_API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gym_API.Controllers
{
	[Authorize]
	[Route("api/{controller}")]
	[ApiController]
	public class GroupController: Controller
	{

		private IGroupService _groupService { get; set; }

		public GroupController(IGroupService groupService)
		{
			this._groupService = groupService;
		}

		[HttpGet]
		public IActionResult GetGroups()
		{
			return Ok(_groupService.GetGroups());
		}

		[HttpGet]
		[Route("{id}")]
		public IActionResult GetGroupById(string Id)
		{
			return Ok(_groupService.GetGroupById(Id));
		}

		[HttpDelete]
		[Route("{id}")]
		public IActionResult DeleteGroup(string Id)
		{
			return Ok(_groupService.DeleteGroup(Id));
		}

		[HttpPost]
		public IActionResult AddGroup(GroupDto body)
		{
			return Ok(_groupService.AddGroup(body));
		}

        [HttpPut]
        [Route("{groupId}")]
        public async Task<IActionResult> UpdateGroupInfo(string groupId, GroupInfoDto body)
        {
            return Ok(await _groupService.UpdateGroupInfo(groupId, body));
        }

        [HttpPost]
        [Route("AddCustomer")]
        public IActionResult AddCustomerToGroup(CustomerWithGroupDto body)
        {
            return Ok(_groupService.AddCustomerToGroup(body.CustomerId, body.GroupId));
        }

        [HttpDelete]
        [Route("RemoveCustomer")]
        public IActionResult RemoveCustomerFromGroup(CustomerWithGroupDto body)
        {
            return Ok(_groupService.RemoveCustomerFromGroup(body.CustomerId, body.GroupId));
        }

        [HttpGet]
		[Route("Schedule/Trainer/{id}")]
		public IActionResult GetTrainerScheduleById(string Id)
		{
			return Ok(_groupService.GetTrainerScheduleById(Id));
		}

		[HttpGet]
		[Route("Schedule/Customer/{id}")]
		public IActionResult GetCustomerScheduleById(string Id)
		{
			return Ok(_groupService.GetCustomerScheduleById(Id));
		}

		[HttpPost]
		[Route("Schedule")]
		public IActionResult AddGroupSchedule(ScheduleDto body)
		{
			return Ok(_groupService.AddGroupSchedule(body));
		}

		[HttpDelete]
		[Route("Schedule/{id}")]
		public IActionResult DeleteGroupSchedule(string Id)
		{
			return Ok(_groupService.RemoveGroupSchedule(Id));
		}

		[HttpPut]
		[Route("Schedule/{id}")]
		public IActionResult UpdateGroupSchedule(string Id,ScheduleDto body)
		{
			return Ok(_groupService.UpdateGroupSchedule(Id,body));
		}
    }
}

