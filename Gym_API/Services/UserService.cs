using System;
using Gym_API.Contexts;
using Gym_API.Dto;
using Gym_API.Services.Interfaces;
using System.Net;
using Gym_API.Models;
using Gym_API.Shared;
using Gym_API.Services.Abstracts;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Collections.Generic;

namespace Gym_API.Services
{
    public class UserService : UserServiceBase
    {
        public UserService(
            ApplicationDbContext db,
            UserManager<User> userManager,
            RoleManager<Role> roleManager
            ): base(db,userManager,roleManager)
        {}

        public override async Task<dynamic> GetUserInfo(string Id)
        {
            var user = await this.GetUserAsync(Id);

            if (user.CoachId != null)
            {
                return await this.GetCoachInfo(user);
            }
            else if (user.SupervisorId != null)
            {
                return await this.GetSupervisorInfo(user);
            }
            else
            {
                return await this.GetCustomerInfo(user);
            }
        }

        public override async Task<Response> UpdateUserInfo(string Id, UserInfoDto data)
        {
            var user = await this.GetUserAsync(Id);

            if (user.CoachId != null)
            {
                return await UpdateCoachInfo(user.CoachId, data).ConfigureAwait(false);
            }

            if (user.SupervisorId != null)
            {
                return await UpdateSupervisorInfo(user.SupervisorId, data).ConfigureAwait(false);
            }

            return await this.UpdateCustomerInfo(user.CustomerId, data);
        }

        public override async Task<List<dynamic>> GetUserInfos(string type)
        {
            List<dynamic> userInfos = new List<dynamic>();

            if(type.ToLower() == "customer")
            {
                foreach (var user in this.GetCustomers())
                {
                    var userInfo = await GetUserInfo(user.Id);
                    userInfos.Add(userInfo);
                }
            }else if (type.ToLower() == "stuff")
            {
                foreach (var user in this.GetStuffs())
                {
                    var userInfo = await GetUserInfo(user.Id);
                    userInfos.Add(userInfo);
                }
            }          
            return userInfos;
        }

        public override async Task<IList<RoleDto>> GetUserRoles(string Id)
        {
            var user = await this.GetUserAsync(Id);
            return await this.GetUserRoles(user);
        }
    }
}

