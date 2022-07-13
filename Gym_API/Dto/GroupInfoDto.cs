using System;
using Gym_API.Models;

namespace Gym_API.Dto
{
	public class GroupInfoDto
	{
        public string? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string TrainerId { get; set; }
        public int Limitation { get; set; }

        public DateTime OpenDate { get; set; }
        public DateTime CloseDate { get; set; }

        public Coach? Trainer { get; set; }
    }
}

