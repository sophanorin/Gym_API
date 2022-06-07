using System;
using Gym_API.Dto;
using Gym_API.Models;
using Gym_API.Shared;
using Microsoft.AspNetCore.Identity;

namespace Gym_API.Services.Interfaces
{
	public interface IRoleService
	{
		public List<RoleDto> GetRoles();
		public Task<Response> DeleteRole(string id);
		public Task<Response> AddRole(RoleDto data);
	}
}

