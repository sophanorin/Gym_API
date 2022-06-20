using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using Gym_API.Models.Base;

namespace Gym_API.Models
{
    public class Coach : Person
    {
        public int WorkingHours { get; set; } = 0;

        public virtual string? StatusId { get; set; }

        public virtual Status? Status { get; set; }

        public virtual ICollection<Specialization> Specializations { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
    }
}

