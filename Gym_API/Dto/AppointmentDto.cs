using System;
namespace Gym_API.Dto
{
	public class AppointmentDto
	{
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string CoachId { get; set; }
        public string CustomerId { get; set; }
    }
}

