using System;
namespace Gym_API.Dto
{
	public class ScheduleDto
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
	}
}

