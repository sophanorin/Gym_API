using System;
using Gym_API.Contexts;
using Gym_API.Dto;
using Gym_API.Services.Interfaces;
using System.Net;
using Gym_API.Models;
using Gym_API.Shared;
using Gym_API.Services.Abstracts;
using Microsoft.AspNetCore.Identity;

namespace Gym_API.Services
{
    public class UserService : UserServiceBase
    {
        private ApplicationDbContext _db { get; set; }
        private readonly UserManager<User> _userManager;

        public UserService(ApplicationDbContext db, UserManager<User> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public override async Task<dynamic> GetUserInfo(string Id)
        {
            var user = await this._userManager.FindByIdAsync(Id);

            if (user == null)
            {
                throw new HttpRequestException($"User Id {Id} not found", null, HttpStatusCode.NotFound);
            }

            if (user.IsAdmin)
            {
                return new
                {
                    Id = user.Id,
                    Fullname = user.UserName,
                    DateOfBirth = "",
                    PhoneNumber = "",
                    Email = "admin@gym.com",
                    Gender = "",
                    Roles = await this._userManager.GetRolesAsync(user)
                };
            }

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
            var user = await _db.Users.FindAsync(Id);

            if (user == null)
            {
                throw new HttpRequestException($"User Id {Id} not found", null, HttpStatusCode.NotFound);
            }

            if (user.CoachId != null)
            {
                return await UpdateCoachInfo(user.CoachId, data).ConfigureAwait(false);
            }

            if (user.SupervisorId != null)
            {
                return await UpdateSupervisorInfo(user.SupervisorId, data).ConfigureAwait(false);
            }

            return await UpdateCustomerInfo(user.CustomerId, data).ConfigureAwait(false);

        }

        public override async Task<List<dynamic>> GetUserInfos()
        {
            return new List<dynamic>();
        }

        protected override async Task<Response> UpdateCoachInfo(string coachId, UserInfoDto data)
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

            if(coach.GenderId != data.GenderId)
                coach.Gender = _db.Genders.Find(data.GenderId);

            if(coach.StatusId != data.StatusId)
                coach.Status = _db.Statuses.Find(data.StatusId);

            if(coach.SpecializationId != data.SpecializationId)
                coach.Specialization = _db.Specializations.Find(data.SpecializationId);

            await _db.SaveChangesAsync().ConfigureAwait(false);

            return new Response { Message = "Coach update successfully", Status = "Success" };
        }

        protected override async Task<Response> UpdateCustomerInfo(string customerId, UserInfoDto data)
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

            if(customer.GenderId != data.GenderId)
                customer.GenderId = data.GenderId;

            await _db.SaveChangesAsync().ConfigureAwait(false);

            return new Response { Message = "Customer update successfully", Status = "Success" }; ;
        }

        protected override async Task<Response> UpdateSupervisorInfo(string supervisorId, UserInfoDto data)
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

            if(supervisor.GenderId != data.GenderId)
                supervisor.Gender = _db.Genders.Find(data.GenderId);

            if(supervisor.StatusId != data.StatusId)
                supervisor.Status = _db.Statuses.Find(data.StatusId);

            await _db.SaveChangesAsync().ConfigureAwait(false);
            return new Response { Message = "Supervisor update successfully", Status = "Success" }; ;
        }

        protected override async Task<dynamic> GetCoachInfo(User user)
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
                        Gender = gender,
                        Status = status,
                        Specialization = specialization,

                    }).First();

            var roles = await this._userManager.GetRolesAsync(user); 

            return new
            {
                Id = _coach.Id,
                Fullname = _coach.Fullname,
                DateOfBirth = _coach.DateOfBirth,
                PhoneNumber = _coach.PhoneNumber,
                Gender = _coach.Gender,
                Email = _coach.Email,
                Status = _coach.Status,
                Specialization = _coach.Specialization,
                Roles = roles
            };
        }

        protected override async Task<dynamic> GetCustomerInfo(User user)
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

            var roles = await this._userManager.GetRolesAsync(user);

            return new
            {
                Id = _customer.Id,
                Fullname = _customer.Fullname,
                DateOfBirth = _customer.DateOfBirth,
                PhoneNumber = _customer.PhoneNumber,
                Email = _customer.Email,
                Gender = _customer.Gender,
                Roles = roles
            };

        }

        protected override async Task<dynamic> GetSupervisorInfo(User user)
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

            var roles = await this._userManager.GetRolesAsync(user);

            return new
            {
                Id = _supervisor.Id,
                Fullname = _supervisor.Fullname,
                DateOfBirth = _supervisor.DateOfBirth,
                PhoneNumber = _supervisor.PhoneNumber,
                Email = _supervisor.Email,
                Status = _supervisor.Status,
                Gender = _supervisor.Gender,
                Roles = roles
            };
        }
    }
}

