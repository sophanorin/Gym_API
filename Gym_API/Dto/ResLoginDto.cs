using System;
namespace Gym_API.Dto
{
	public class ResLoginDto
	{
        public string Token { get; set; }
        public dynamic User { get; set; }
        public string RefreshToken { get; set; }
        public DateTime Expiration { get; set; }
    }
}

