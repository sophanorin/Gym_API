using System;
namespace Gym_API.Models
{
	public class Appointment
	{
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; } = true;
        public string CoachId { get; set; }
        public string CustomerId { get; set; }

        public Coach Coach { get; set; }
        public Customer Customer { get; set; }
    }
}

