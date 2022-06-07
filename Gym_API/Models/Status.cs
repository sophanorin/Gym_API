using System;
using Gym_API.Models.Base;

namespace Gym_API.Models
{
	public class Status: BaseModel
	{
        public Status(): base()
        {
        }
        public Status(string Name) : base(Name)
        {
        }
    }
}

