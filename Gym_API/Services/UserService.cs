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
            ) : base(db, userManager, roleManager)
        { }

        public override async Task<dynamic> GetUserInfoAsync(string Id)
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

        public override async Task<List<dynamic>> GetUserInfos(UserInfoQuery query)
        {
            List<dynamic> userInfos = new List<dynamic>();

            if (query.type.ToLower() == "customer")
            {
                foreach (var user in this.GetCustomers())
                {
                    var userInfo = await GetUserInfoAsync(user.Id);
                    userInfos.Add(userInfo);
                }
            }
            else if (query.type.ToLower() == "stuff")
            {

                List<User> users = new List<User>();


                switch (query.role)
                {
                    case "coach":
                        users.AddRange(await this.GetCoachs());
                        break;
                    case "supervisor":
                        var seniorSupervisors = await this.GetSeniorSupervisors();
                        var supervisors = await this.GetSupervisors();

                        users.AddRange(seniorSupervisors);
                        users.AddRange(supervisors);
                        break;
                    default:
                        users.AddRange(this.GetStuffs());
                        break;
                }

                foreach (var user in users)
                {
                    var userInfo = await GetUserInfoAsync(user.Id);
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

        public override async Task<Response> UpdateUserRoles(string userId, IEnumerable<string> roleNames)
        {
            var userExist = await this._userManager.FindByIdAsync(userId);

            if (userExist == null)
            {
                throw new HttpRequestException($"User Id {userId}", null, HttpStatusCode.NotFound);
            }

            var userRoles = await this._userManager.GetRolesAsync(userExist);

            await this._userManager.RemoveFromRolesAsync(userExist, userRoles);

            await this._userManager.AddToRolesAsync(userExist, roleNames);

            return new Response { Message = $"Update user role successfully", Status = "Success" };
        }

        public override async Task<Response> RemoveUserRoles(string userId, IEnumerable<string> roleNames)
        {
            var userExist = await this._userManager.FindByIdAsync(userId);

            if (userExist == null)
            {
                throw new HttpRequestException($"User Id {userId}", null, HttpStatusCode.NotFound);
            }

            await this._userManager.RemoveFromRolesAsync(userExist, roleNames);

            return new Response { Message = $"Remove roles from user {userExist.UserName} successfully", Status = "Success" };
        }
    }
}

