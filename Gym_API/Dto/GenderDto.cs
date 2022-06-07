using System;
using Gym_API.Models;

namespace Gym_API.Dto
{
	public class GenderDto:Gender
	{
		public GenderDto():base()
		{
		}
        public GenderDto(string Name): base(Name)
        {

        }
	}
}

