using System;
namespace Gym_API.Dto
{
	public class UserInfoQuery
	{
        public string type { get; set; } = "customer";
        public string? role { get; set; } = "all";
    }
}

