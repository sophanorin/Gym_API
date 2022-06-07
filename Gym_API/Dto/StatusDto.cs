using System;
using Gym_API.Models;

namespace Gym_API.Dto
{
	public class StatusDto:Status
	{
		public StatusDto():base()
		{
		}
		public StatusDto(string Name) : base(Name)
		{
		}
	}
}

