using System;
using System.ComponentModel.DataAnnotations;

namespace Gym_API.Dto
{
	public class LoginDto
	{
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}

