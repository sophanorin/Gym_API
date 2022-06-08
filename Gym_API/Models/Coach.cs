using System;
using System.ComponentModel.DataAnnotations.Schema;
using Gym_API.Models.Base;

namespace Gym_API.Models
{
    public class Coach : Person
    {

        public int WorkingHours { get; set; }

        public virtual string? StatusId { get; set; }
        public virtual string? SpecializationId { get; set; }


        public virtual Status? Status { get; set; }
        public virtual Specialization? Specialization { get; set; }

    }
}

