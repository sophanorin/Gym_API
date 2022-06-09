using System;
using System.ComponentModel.DataAnnotations;

namespace Gym_API.Dto
{
	public class RegisterBaseDto
	{
        [Required(ErrorMessage = "Fullname is required")]
        public string Fullname { get; set; }

        [Required(ErrorMessage = "Phnone Number is required")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "DateOfBirth is required")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "GenderId is required")]
        public string GenderId { get; set; }
    }
}

