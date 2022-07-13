using System;
namespace Gym_API.Models
{
	public class Group
	{
		public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
		public string Description { get; set; }
		public virtual string TrainerId { get; set; }
        public int Limitation { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime CloseDate { get; set; }

        public virtual Coach Trainer { get; set; }
		public virtual ICollection<Customer> Customers { get; set; }
		public virtual ICollection<Schedule> Schedules { get; set; }
	}
}

