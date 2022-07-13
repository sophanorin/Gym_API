using System;
using Gym_API.Dto;
using Gym_API.Shared;
using Microsoft.AspNetCore.Mvc;


namespace Gym_API.Services.Interfaces
{
    public interface IAuthenticationService
    {
        public Task<Response> RegisterCustomer(RegisterCustomerDto model);
        public Task<Response> RegisterStuff(RegisterStuffDto model);
        public Task<Response> RegisterSeniorSupervisor(RegisterSupervisorDto model);
        public Task<ResLoginDto> Login(LoginDto model);
        public Task<TokenDto> RefreshToken(TokenDto tokenDto);
    }
}

