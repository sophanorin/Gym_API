using System;
using Gym_API.Contexts;
using Gym_API.Dto;
using Gym_API.Services.Interfaces;
using System.Net;
using Gym_API.Models;
using Gym_API.Shared;

namespace Gym_API.Services
{
    public class UserService : IUserService
    {
        private ApplicationDbContext _db { get; set; }

        public UserService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<dynamic> GetUser(string Id)
        {
            var user = await _db.Users.FindAsync(Id);

            if (user == null)
            {
                throw new HttpRequestException($"User Id {Id} not found", null, HttpStatusCode.NotFound);
            }

            if (user.CoachId != null)
            {
                return this.GetCoachInfo(user.CoachId);
            }

            if (user.SupervisorId != null)
            {
                return this.GetSupervisorInfo(user.SupervisorId);
            }

            return this.GetCustomerInfo(user.CustomerId);
        }

        public dynamic GetCoachInfo(string id)
        {
            var _coach =
                   (from coach in _db.Coaches
                    join gender in _db.Genders on coach.GenderId equals gender.Id
                    join status in _db.Statuses on coach.StatusId equals status.Id
                    join specialization in _db.Specializations on coach.SpecializationId equals specialization.Id
                    where coach.Id == id
                    select new
                    {
                        Id = coach.Id,
                        Fullname = coach.Fullname,
                        DateOfBirth = coach.DateOfBirth,
                        PhoneNumber = coach.PhoneNumber,
                        Email = coach.Email,
                        Gender = gender.Name,
                        Status = status.Name,
                        Specialization = specialization.Name,

                    }).First();

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
                Role = UserRoles.Coach
            };
        }

        public dynamic GetCustomerInfo(string id)
        {
            var _customer =
                    (from customer in _db.Customers
                     join gender in _db.Genders on customer.GenderId equals gender.Id
                     where customer.Id == id
                     select new
                     {
                         Id = customer.Id,
                         Fullname = customer.Fullname,
                         DateOfBirth = customer.DateOfBirth,
                         PhoneNumber = customer.PhoneNumber,
                         Email = customer.Email,
                         Gender = gender.Name,

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
                Role = UserRoles.Customer,
            };

        }

        public dynamic GetSupervisorInfo(string id)
        {
            var _supervisor =
                     (from supervisor in _db.Supervisors
                      join gender in _db.Genders on supervisor.GenderId equals gender.Id
                      join status in _db.Statuses on supervisor.StatusId equals status.Id
                      where supervisor.Id == id
                      select new
                      {
                          Id = supervisor.Id,
                          Fullname = supervisor.Fullname,
                          DateOfBirth = supervisor.DateOfBirth,
                          PhoneNumber = supervisor.PhoneNumber,
                          Email = supervisor.Email,
                          Gender = gender.Name,
                          Status = status.Name,

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
                Role = UserRoles.Supervisor,
            };
        }
    }
}

