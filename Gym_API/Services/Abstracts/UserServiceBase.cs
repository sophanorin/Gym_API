using System;
using Gym_API.Dto;
using Gym_API.Models;
using Gym_API.Services.Interfaces;
using Gym_API.Shared;

namespace Gym_API.Services.Abstracts
{
	public abstract class UserServiceBase: IUserService
	{
		public abstract Task<dynamic> GetUserInfo(string Id);
		public abstract Task<List<dynamic>> GetUserInfos();
		public abstract Task<Response> UpdateUserInfo(string coachId, UserInfoDto data);

		protected abstract Task<Response> UpdateCoachInfo(string coachId, UserInfoDto data);
		protected abstract Task<Response> UpdateCustomerInfo(string customerId, UserInfoDto data);
		protected abstract Task<Response> UpdateSupervisorInfo(string supervisor, UserInfoDto data);

		protected abstract dynamic GetCoachInfo(User user);
		protected abstract dynamic GetCustomerInfo(User user);
		protected abstract dynamic GetSupervisorInfo(User user);
	
    }
}

