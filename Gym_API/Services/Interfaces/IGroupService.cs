using System;
using Gym_API.Dto;
using Gym_API.Models;
using Gym_API.Shared;

namespace Gym_API.Services.Interfaces
{
	public interface IGroupService
	{
		public dynamic GetGroups();
		public dynamic GetGroupById(string Id);
		public Response DeleteGroup(string Id);
		public Task<dynamic> UpdateGroupInfo(string groupId, GroupInfoDto data);
		public Response AddGroup(GroupDto body);
		public Response AddCustomerToGroup(string customerId,string groupId);
        public Response RemoveCustomerFromGroup(string customerId, string groupId);

        public dynamic GetTrainerScheduleById(string Id);
		public dynamic GetCustomerScheduleById(string Id);

		public Schedule AddGroupSchedule(ScheduleDto data);
		public Response RemoveGroupSchedule(string Id);
		public Schedule UpdateGroupSchedule(string Id, ScheduleDto data);

	}
}

