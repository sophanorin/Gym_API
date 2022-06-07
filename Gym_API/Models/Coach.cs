using System;
using System.ComponentModel.DataAnnotations.Schema;
using Gym_API.Models.Base;

namespace Gym_API.Models
{
    public class Coach : Person
    {

        public int WorkingHours { get; set; }

        public string StatusId { get; set; }
        public string SpecializationId { get; set; }


        public Status Status { get; set; }
        public Specialization Specialization { get; set; }

    }
}

