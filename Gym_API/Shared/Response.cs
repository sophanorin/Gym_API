using System;
namespace Gym_API.Shared
{
	public class Response
	{
		public string Status { get; set; }
        public string Message { get; set; }
        public Object Errors { get; set; }
    }
}