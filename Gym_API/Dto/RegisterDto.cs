using System;
using System.ComponentModel.DataAnnotations;
using Gym_API.Shared;

namespace Gym_API.Dto
{
    public class RegisterDto
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

        [Required(ErrorMessage = "RoleId is required")]
        public string RoleId { get; set; }

        public string? StatusId { get; set; }
        public string? SpecializationId { get; set; }
    }
}