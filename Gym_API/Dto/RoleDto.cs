using System;
using System.ComponentModel.DataAnnotations;
using Gym_API.Models;

namespace Gym_API.Dto
{
    public class RoleDto
    {
        public string Id { get; set; } = "";

        [Required(ErrorMessage = "Specialization name is required")]
        public string Name { get; set; }
    }
}

