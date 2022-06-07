using System;
using Gym_API.Models;

namespace Gym_API.Dto
{
	public class SpecializatinDto : Specialization
	{
		public SpecializatinDto():base()
		{
		}
		public SpecializatinDto(string Name) : base(Name)
		{
		}
	}
}

