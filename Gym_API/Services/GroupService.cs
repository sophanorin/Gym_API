using System;
using Gym_API.Contexts;
using Gym_API.Dto;
using Gym_API.Models;
using Gym_API.Services.Interfaces;
using Gym_API.Shared;
using System.Net;

namespace Gym_API.Services
{
    public class GroupService : IGroupService
    {
        private readonly ApplicationDbContext _db;

        public GroupService(ApplicationDbContext db)
        {
            _db = db;
        }

        public Response AddGroup(GroupDto body)
        {
            var group = new Group
            {
                Name = body.Name,
                Description = body.Description,
                TrainerId = body.TrainerId
            };

            if (body.CustomerIds.Count > 0)
            {
                List<Customer> customers = new List<Customer>();

                foreach (string id in body.CustomerIds)
                {
                    var customer = _db.Customers.Find(id);
                    if (customer != null)
                    {
                        customers.Add(customer);
                    }
                }

                group.Customers = customers;

            }

            if (body.Schedules.Count > 0)
            {
                List<Schedule> schedules = new List<Schedule>();

                foreach (ScheduleDto schedule in body.Schedules)
                {
                    Schedule newSchedule = new Schedule
                    {
                        Title = schedule.Title,
                        Description = schedule.Description,
                        StartDate = schedule.StartDate,
                        EndDate = schedule.EndDate
                    };

                    schedules.Add(newSchedule);
                }

                _db.Schedules.AddRange(schedules);
                group.Schedules = schedules;
            }

            _db.Groups.Add(group);
            _db.SaveChanges();

            return new Response { Message = $"Group {body.Name} added successfully", Status = "Success" };
        }

        public dynamic GetGroupById(string Id)
        {
            var group = _db.Groups
                .Where(g => g.Id == Id)
                .Select(g => new
                {
                    Id = g.Id,
                    Name = g.Name,
                    Description = g.Description,
                    Customers = g.Customers.Select(customer => new
                    {
                        Id = customer.Id,
                        Fullname = customer.Fullname,
                        PhoneNumber = customer.PhoneNumber,
                        DateOfBirth = customer.DateOfBirth,
                        Email = customer.Email,
                        Gender = customer.Gender,
                    }).ToList(),
                    Trainer = new
                    {
                        Id = g.Trainer.Id,
                        Fullname = g.Trainer.Fullname,
                        PhoneNumber = g.Trainer.PhoneNumber,
                        DateOfBirth = g.Trainer.DateOfBirth,
                        Email = g.Trainer.Email,
                        Gender = g.Trainer.Gender,
                        Specialization = g.Trainer.Specializations.Select(specialization => new
                        {
                            Id = specialization.Id,
                            Name = specialization.Name
                        }),
                        Status = g.Trainer.Status,
                    },
                    Schedules = g.Schedules.Select(s => s).ToList()
                }).FirstOrDefault();

            if (group == null)
            {
                throw new HttpRequestException($"Group Id {Id} not found", null, HttpStatusCode.NotFound);
            }

            return group;
        }

        public dynamic GetTrainerScheduleById(string Id)
        {
            return _db.Groups
                .Where(g => g.TrainerId == Id)
                .Select(g => new
                {
                    Id = g.Id,
                    Name = g.Name,
                    Description = g.Description,
                    Customers = g.Customers.Select(customer => new
                    {
                        Id = customer.Id,
                        Fullname = customer.Fullname,
                        PhoneNumber = customer.PhoneNumber,
                        DateOfBirth = customer.DateOfBirth,
                        Email = customer.Email,
                        Gender = customer.Gender,
                    }).ToList(),
                    Schedules = g.Schedules.Select(s => s).ToList()
                }).ToList();
        }

        public dynamic GetCustomerScheduleById(string Id)
        {
            var customer = _db.Customers.Find(Id);

            if (customer == null)
                throw new HttpRequestException("Customer Id {Id} not found", null, HttpStatusCode.NotFound);

            var schedules = _db.Schedules
                .Where(s => s.Group.Customers
                    .Contains(customer)
                    )
                .Select(s => new {
                    Id =s.Id,
                    Title = s.Title,
                    Description = s.Description,
                    StartDate = s.StartDate,
                    EndDate = s.EndDate
                });
               
            return schedules;
        }

        public Response DeleteGroup(string Id)
        {
            var group = _db.Groups.Find(Id);


            if (group == null)
            {
                throw new HttpRequestException($"Group Id {Id} not found", null, HttpStatusCode.NotFound);
            }

            //_db.Schedules.RemoveRange(group.Schedules);
            _db.Groups.Remove(group);
            _db.SaveChanges();

            return new Response { Message = $"Group Id {Id} deleted", Status = "Success" };
        }

        public dynamic GetGroups()
        {
            return _db.Groups
               .Select(g => new
               {
                   Id = g.Id,
                   Name = g.Name,
                   Description = g.Description,
                   Customers = g.Customers.Select(customer => new
                   {
                       Id = customer.Id,
                       Fullname = customer.Fullname,
                       PhoneNumber = customer.PhoneNumber,
                       DateOfBirth = customer.DateOfBirth,
                       Email = customer.Email,
                       Gender = customer.Gender,
                   }).ToList(),
                   Trainer = new
                   {
                       Id = g.Trainer.Id,
                       Fullname = g.Trainer.Fullname,
                       PhoneNumber = g.Trainer.PhoneNumber,
                       DateOfBirth = g.Trainer.DateOfBirth,
                       Email = g.Trainer.Email,
                       Gender = g.Trainer.Gender,
                       Specialization = g.Trainer.Specializations.Select(specializatin => new
                       {
                           Id = specializatin.Id,
                           Name = specializatin.Name
                       }),
                       Status = g.Trainer.Status,
                   },
                   Schedules = g.Schedules.Select(s => s).ToList()
               })
               .ToList();
        }

        public Schedule AddGroupSchedule(ScheduleDto data)
        {
            Schedule schedule = new Schedule
            {
                Title = data.Title,
                Description = data.Description,
                StartDate = data.StartDate,
                EndDate = data.EndDate
            };

            _db.Schedules.Add(schedule);
            _db.SaveChanges();


            return schedule;
        }

        public Schedule UpdateGroupSchedule(string Id, ScheduleDto data)
        {
            Schedule schedule = _db.Schedules.Find(Id);

            if (schedule == null)
                throw new HttpRequestException($"Schedule Id {Id} not found", null, HttpStatusCode.NotFound);

            schedule.Title = data.Title;
            schedule.Description = data.Description;
            schedule.StartDate = data.StartDate;
            schedule.EndDate = data.EndDate;

            _db.SaveChanges();

            return schedule;
        }

        public Response RemoveGroupSchedule(string Id)
        {
            Schedule schedule = _db.Schedules.Find(Id);

            if (schedule == null)
                throw new HttpRequestException($"Schedule Id {Id} not found", null, HttpStatusCode.NotFound);

            _db.Schedules.Remove(schedule);
            _db.SaveChanges();

            return new Response { Message = $"Schedule Id {Id} deleted", Status = "Success" };
        }
    }
}

