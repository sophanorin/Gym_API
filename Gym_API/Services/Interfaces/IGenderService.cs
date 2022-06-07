using System;
using Gym_API.Dto;
using Gym_API.Models;
using Gym_API.Shared;

namespace Gym_API.Services.Interfaces
{
	public interface IGenderService
	{
		public List<Gender> GetGenders();
		public Response DeleteGender(string id);
		public Gender AddGender(GenderDto data);
	}
}

