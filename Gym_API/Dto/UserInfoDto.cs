using System;
namespace Gym_API.Dto
{
	public class UserInfoDto
	{
        public string Fullname { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Email { get; set; }

        public string GenderId { get; set; }

        public int? WorkingHours { get; set; }

        public string? StatusId { get; set; }

        public string? SpecializationId { get; set; }
    }
}

