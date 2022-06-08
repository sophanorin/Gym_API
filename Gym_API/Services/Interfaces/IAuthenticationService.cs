using System;
using Gym_API.Dto;
using Gym_API.Shared;
using Microsoft.AspNetCore.Mvc;


namespace Gym_API.Services.Interfaces
{
    public interface IAuthenticationService
    {
        public Task<Response> RemoveUserRoles(string userId, IEnumerable<string> roleNames);
        public Task<Response> UpdateUserRoles(string userId, IEnumerable<string> roleNames);

        public Task<Response> Register(RegisterDto model);
        public Task<Response> RegisterAdmin(AdminRegisterDto model);
        public Task<ResLoginDto> Login(LoginDto model);
    }
}

