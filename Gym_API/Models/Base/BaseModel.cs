using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Gym_API.Models.Base
{
	public class BaseModel
	{
		[Required(ErrorMessage = "Id is required")]
		public string Id { get; set; }

		[Required(ErrorMessage = "Specialization name is required")]
		public string Name { get; set; }

        public BaseModel()
        {
			this.Id = Guid.NewGuid().ToString();
        }

        public BaseModel(string Name)
        {
			this.Id = Guid.NewGuid().ToString();
			this.Name = Name;
        }

		public BaseModel(string Id,string Name)
		{
			this.Id = Id;
			this.Name = Name;
		}
	}
}

