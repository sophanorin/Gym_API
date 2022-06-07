using System;
using Microsoft.AspNetCore.Identity;

namespace Gym_API.Models
{
    public class User : IdentityUser
    {
        public string? CoachId { get; set; }
        public string? CustomerId { get; set; }
        public string? SupervisorId { get; set; }
        public string? SpecializationId { get; set; }

        public Coach? Coach { get; set; }
        public Customer? Customer { get; set; }
        public Supervisor? Supervisor { get; set; }
        public Specialization? Specialization { get; set; }

        public User() : base()
        {
        }
    }
}

