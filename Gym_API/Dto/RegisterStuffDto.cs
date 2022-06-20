using System;
using System.ComponentModel.DataAnnotations;
using Gym_API.Shared;

namespace Gym_API.Dto
{
    public class RegisterStuffDto: RegisterBaseDto
    {
        public string? RoleId { get; set; }

        public int? WorkingHours { get; set; }

        public string? StatusId { get; set; }
        public ICollection<string> SpecializationIds { get; set; }
    }
}