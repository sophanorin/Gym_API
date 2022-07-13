using System;
using System.Text.Json;
using Gym_API.Models;

namespace Gym_API.Dto
{
	public class ResUserInfoDto
	{
        public string Id { get; set; }

        public string Fullname { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Email { get; set; }

        public string Gender { get; set; }

        public string Role { get; set; }

        public string Status { get; set; }

        public string Specialization { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}

