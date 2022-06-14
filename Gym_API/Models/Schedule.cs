using System;
namespace Gym_API.Models
{
	public class Schedule
	{
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public virtual string GroupId { get; set; }

        public virtual Group Group { get; set; }
    }
}

