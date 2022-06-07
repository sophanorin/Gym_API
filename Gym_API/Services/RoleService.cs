using System;
using System.Net;
using Gym_API.Contexts;
using Gym_API.Dto;
using Gym_API.Models;
using Gym_API.Services.Interfaces;
using Gym_API.Shared;
using Microsoft.AspNetCore.Identity;

namespace Gym_API.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly ApplicationDbContext _db;

        public RoleService()
        {
        }

        public RoleService(RoleManager<Role> roleManager, ApplicationDbContext db)
        {
            _roleManager = roleManager;
            _db = db;
        }

        public async Task<Response> AddRole(RoleDto data)
        {

            if (await this._roleManager.RoleExistsAsync(data.Name))
            {
                throw new HttpRequestException($"Role {data.Name} already existing", null, HttpStatusCode.Forbidden);
            }

            await _roleManager.CreateAsync(new Role(data.Name));

            return new Response { Message = "Successfully created", Status = "Success" };

        }

        public async Task<Response> DeleteRole(string id)
        {
            var existingRole = await this._roleManager.FindByIdAsync(id);

            if (existingRole == null)
            {
                throw new HttpRequestException($"Role Id {id} not found", null, HttpStatusCode.NotFound);
            }

            await _roleManager.DeleteAsync(existingRole);

            return new Response { Message = $"Role Name {existingRole.Name} deleted", Status = "Success" };
        }

        public List<RoleDto> GetRoles()
        {
            return this._db.Roles
                .Select((role) => new RoleDto { Id = role.Id, Name = role.Name })
                .ToList();
        }
    }
}

