using System;
using Microsoft.AspNetCore.Identity;

namespace Gym_API.Models
{
	public class Role: IdentityRole
	{
        public Role():base()
        {

        }

        public Role(string Name) : base(Name)
        {

        }
    }
}

