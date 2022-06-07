using System;
using Gym_API.Models.Base;
    
namespace Gym_API.Models
{
	public class Gender: BaseModel
	{
        public Gender():base()
        {
        }

        public Gender(string Name):base(Name)
        {
        }
        public Gender(string Id, string Name) : base(Id, Name)
        {
        }
    }
}

