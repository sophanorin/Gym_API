using System;
using Gym_API.Dto;

namespace Gym_API.Services.Interfaces
{
	public interface IUserService
	{
		public Task<dynamic> GetUser(string id);

		public dynamic GetCoachInfo(string id);
		public dynamic GetCustomerInfo(string id);
		public dynamic GetSupervisorInfo(string id);

	}
}

