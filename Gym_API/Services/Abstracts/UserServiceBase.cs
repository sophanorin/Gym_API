using System;
using System.Net;
using Gym_API.Contexts;
using Gym_API.Dto;
using Gym_API.Models;
using Gym_API.Services.Interfaces;
using Gym_API.Shared;
using Microsoft.AspNetCore.Identity;

namespace Gym_API.Services.Abstracts
{
	public abstract class UserServiceBase: IUserService
	{
        protected ApplicationDbContext _db { get; set; }
        protected readonly UserManager<User> _userManager;
        protected readonly RoleManager<Role> _roleManager;

        public UserServiceBase(
            ApplicationDbContext db,
            UserManager<User> userManager,
            RoleManager<Role> roleManager
            )
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public abstract Task<IList<RoleDto>> GetUserRoles(string Id);
		public abstract Task<dynamic> GetUserInfo(string Id);
		public abstract Task<List<dynamic>> GetUserInfos(string type);
		public abstract Task<Response> UpdateUserInfo(string coachId, UserInfoDto data);

        protected async Task<Response> UpdateCoachInfo(string coachId, UserInfoDto data)
        {
            Coach? coach = await _db.Coaches.FindAsync(coachId);

            if (coach == null)
            {
                throw new HttpRequestException($"Coach Id {coach} not found", null, HttpStatusCode.NotFound);
            }

            coach.Fullname = data.Fullname;
            coach.Email = data.Email;
            coach.DateOfBirth = data.DateOfBirth;
            coach.PhoneNumber = data.PhoneNumber;
            coach.WorkingHours = (int)data.WorkingHours;

            if (coach.GenderId != data.GenderId)
                coach.Gender = _db.Genders.Find(data.GenderId);

            if (coach.StatusId != data.StatusId)
                coach.Status = _db.Statuses.Find(data.StatusId);

            if (coach.SpecializationId != data.SpecializationId)
                coach.Specialization = _db.Specializations.Find(data.SpecializationId);

            await _db.SaveChangesAsync().ConfigureAwait(false);

            return new Response { Message = "Coach update successfully", Status = "Success" };
        }

        protected async Task<Response> UpdateCustomerInfo(string customerId, UserInfoDto data)
        {
            Customer? customer = await _db.Customers.FindAsync(customerId);

            if (customer == null)
            {
                throw new HttpRequestException($"Customer Id {customer} not found", null, HttpStatusCode.NotFound);
            }

            customer.Fullname = data.Fullname;
            customer.Email = data.Email;
            customer.DateOfBirth = data.DateOfBirth;
            customer.PhoneNumber = data.PhoneNumber;

            if (customer.GenderId != data.GenderId)
                customer.GenderId = data.GenderId;

            await _db.SaveChangesAsync().ConfigureAwait(false);

            return new Response { Message = "Customer update successfully", Status = "Success" }; ;
        }

        protected async Task<Response> UpdateSupervisorInfo(string supervisorId, UserInfoDto data)
        {
            Supervisor? supervisor = await _db.Supervisors.FindAsync(supervisorId);

            if (supervisor == null)
            {
                throw new HttpRequestException($"Supervisor Id {supervisor} not found", null, HttpStatusCode.NotFound);
            }

            supervisor.Fullname = data.Fullname;
            supervisor.Email = data.Email;
            supervisor.DateOfBirth = data.DateOfBirth;
            supervisor.PhoneNumber = data.PhoneNumber;

            if (supervisor.GenderId != data.GenderId)
                supervisor.Gender = _db.Genders.Find(data.GenderId);

            if (supervisor.StatusId != data.StatusId)
                supervisor.Status = _db.Statuses.Find(data.StatusId);

            await _db.SaveChangesAsync().ConfigureAwait(false);
            return new Response { Message = "Supervisor update successfully", Status = "Success" }; ;
        }

        protected async Task<dynamic> GetCoachInfo(User user)
        {
            var _coach =
                   (from coach in _db.Coaches
                    join gender in _db.Genders on coach.GenderId equals gender.Id
                    join status in _db.Statuses on coach.StatusId equals status.Id
                    join specialization in _db.Specializations on coach.SpecializationId equals specialization.Id
                    where coach.Id == user.CoachId
                    select new
                    {
                        Id = coach.Id,
                        Fullname = coach.Fullname,
                        DateOfBirth = coach.DateOfBirth,
                        PhoneNumber = coach.PhoneNumber,
                        Email = coach.Email,
                        WorkingHours = coach.WorkingHours,
                        Gender = gender,
                        Status = status,
                        Specialization = specialization,
                    }).First();

            return new
            {
                Id = _coach.Id,
                Fullname = _coach.Fullname,
                DateOfBirth = _coach.DateOfBirth,
                PhoneNumber = _coach.PhoneNumber,
                Gender = _coach.Gender,
                Email = _coach.Email,
                WorkingHours = _coach.WorkingHours,
                Status = _coach.Status,
                Specialization = _coach.Specialization,
                Roles = await this.GetUserRoles(user)
            };
        }

        protected async Task<dynamic> GetCustomerInfo(User user)
        {
            var _customer =
                    (from customer in _db.Customers
                     join gender in _db.Genders on customer.GenderId equals gender.Id
                     where customer.Id == user.CustomerId
                     select new
                     {
                         Id = customer.Id,
                         Fullname = customer.Fullname,
                         DateOfBirth = customer.DateOfBirth,
                         PhoneNumber = customer.PhoneNumber,
                         Email = customer.Email,
                         Gender = gender,
                     }
                     ).First();

            return new
            {
                Id = _customer.Id,
                Fullname = _customer.Fullname,
                DateOfBirth = _customer.DateOfBirth,
                PhoneNumber = _customer.PhoneNumber,
                Email = _customer.Email,
                Gender = _customer.Gender,
                Roles = await this.GetUserRoles(user)
            };

        }

        protected async Task<dynamic> GetSupervisorInfo(User user)
        {
            var _supervisor =
                     (from supervisor in _db.Supervisors
                      join gender in _db.Genders on supervisor.GenderId equals gender.Id
                      join status in _db.Statuses on supervisor.StatusId equals status.Id
                      where supervisor.Id == user.SupervisorId
                      select new
                      {
                          Id = supervisor.Id,
                          Fullname = supervisor.Fullname,
                          DateOfBirth = supervisor.DateOfBirth,
                          PhoneNumber = supervisor.PhoneNumber,
                          Email = supervisor.Email,
                          Gender = gender,
                          Status = status,

                      }).First();

            return new
            {
                Id = _supervisor.Id,
                Fullname = _supervisor.Fullname,
                DateOfBirth = _supervisor.DateOfBirth,
                PhoneNumber = _supervisor.PhoneNumber,
                Email = _supervisor.Email,
                Status = _supervisor.Status,
                Gender = _supervisor.Gender,
                Roles = await this.GetUserRoles(user)
            };
        }

        protected async Task<IList<RoleDto>> GetUserRoles(User user)
        {
            var userRoles = await this._userManager.GetRolesAsync(user);
            return this._roleManager.Roles
                .Where(role => userRoles.Contains(role.Name))
                .Select(role => new RoleDto { Id = role.Id, Name = role.Name }).ToList();
        }

        protected async Task<User> GetUserAsync(string Id)
        {
            var user = await this._userManager.FindByIdAsync(Id);

            if (user == null)
            {
                throw new HttpRequestException($"User Id {Id} not found", null, HttpStatusCode.NotFound);
            }

            return user;
        }

        protected IList<User> GetCustomers()
        {
            return this._userManager.Users.Where(user => user.IsCustomer).ToList();
        }

        protected IList<User> GetStuffs()
        {
            return this._userManager.Users.Where(user => user.IsStuff).ToList();
        }
    }
}

