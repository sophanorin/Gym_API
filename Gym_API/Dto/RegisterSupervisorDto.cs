using System;
namespace Gym_API.Dto
{
	public class RegisterSupervisorDto : RegisterBaseDto
    {
        public string? RoleId { get; set; }

        public int? WorkingHours { get; set; }

        public string? StatusId { get; set; }
    }
}

