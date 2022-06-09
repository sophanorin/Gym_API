using System;
using Gym_API.Dto;
using Gym_API.Shared;

namespace Gym_API.Services.Interfaces
{
	public interface IUserService
	{
		public Task<IList<RoleDto>> GetUserRoles(string Id);
 		public Task<dynamic> GetUserInfo(string Id);
		public Task<Response> UpdateUserInfo(string coachId, UserInfoDto data);
		public Task<List<dynamic>> GetUserInfos(string type);
 	}
}

