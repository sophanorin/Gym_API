using System;
using System.ComponentModel.DataAnnotations.Schema;
using Gym_API.Models.Base;

namespace Gym_API.Models
{
	public class Customer: Person
	{
        public virtual ICollection<Group> Groups { get; set; }
    }
}

