using System;
using Gym_API.Models.Base;

namespace Gym_API.Models
{
	public class Supervisor: Person
	{
        public string StatusId { get; set; }

        public Status Status { get; set; }

        public Supervisor()
		{
		}
	}
}

