using System;
using Gym_API.Contexts;
using Gym_API.Dto;
using Gym_API.Models;
using Gym_API.Services.Interfaces;
using Gym_API.Shared;
using System.Net;
using System.Linq;

namespace Gym_API.Services
{
    public class GroupService : IGroupService
    {
        private readonly ApplicationDbContext _db;
        private readonly IUserService _userService;

        public GroupService(IUserService userService,ApplicationDbContext db)
        {
            _db = db;
            _userService = userService;
        }

        public Response AddGroup(GroupDto body)
        {
            if (body.OpenDate > body.CloseDate)
            {
                throw new HttpRequestException("Invalid OpenDate & CloseDate", null, HttpStatusCode.BadRequest);
            }

            var group = new Group
            {
                Name = body.Name,
                Description = body.Description,
                TrainerId = body.TrainerId,
                Limitation = body.Limitation,
                OpenDate = body.OpenDate,
                CloseDate = body.CloseDate
            };

            if (body.CustomerIds.Count > group.Limitation)
            {
                throw new HttpRequestException("Customers or members can't over the limitation", null, HttpStatusCode.BadRequest);
            }

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
                    Limitation = g.Limitation,
                    IsFull = g.Customers.Count >= g.Limitation,
                    TotalCustomers = g.Customers.Count,
                    Customers = g.Customers.Select(customer => new
                    {
                        Id = customer.Id,
                        Fullname = customer.Fullname,
                        Firstname = customer.Firstname,
                        Lastname = customer.Lastname,
                        PhoneNumber = customer.PhoneNumber,
                        DateOfBirth = customer.DateOfBirth,
                        Email = customer.Email,
                        Gender = customer.Gender,
                    }).ToList(),
                    Trainer = new
                    {
                        Id = g.Trainer.Id,
                        Fullname = g.Trainer.Fullname,
                        Firstname = g.Trainer.Firstname,
                        Lastname = g.Trainer.Lastname,
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
                    Schedules = g.Schedules.Select(s => new
                    {
                        Id = s.Id,
                        Title = s.Title,
                        Description = s.Description,
                        StartDate = s.StartDate,
                        EndDate = s.EndDate
                    }).ToList()
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
                    Limitation = g.Limitation,
                    OpenDate = g.OpenDate,
                    CloseDate = g.CloseDate,
                    Customers = g.Customers.Select(customer => new
                    {
                        Id = customer.Id,
                        Fullname = customer.Fullname,
                        Firstname = customer.Firstname,
                        Lastname = customer.Lastname,
                        PhoneNumber = customer.PhoneNumber,
                        DateOfBirth = customer.DateOfBirth,
                        Email = customer.Email,
                        Gender = customer.Gender,
                    }).ToList(),
                    Schedules = g.Schedules.Select(s => new
                    {
                        Id = s.Id,
                        Title = s.Title,
                        Description = s.Description,
                        StartDate = s.StartDate,
                        EndDate = s.EndDate
                    }).ToList()
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
                .Select(s => new
                {
                    Id = s.Id,
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
                   Limitation = g.Limitation,
                   OpenDate = g.OpenDate,
                   CloseDate = g.CloseDate,
                   IsFull = g.Customers.Count >= g.Limitation,
                   TotalCustomers = g.Customers.Count,
                   Customers = g.Customers.Select(customer => new
                   {
                       Id = customer.Id,
                       Fullname = customer.Fullname,
                       Firstname = customer.Firstname,
                       Lastname = customer.Lastname,
                       PhoneNumber = customer.PhoneNumber,
                       DateOfBirth = customer.DateOfBirth,
                       Email = customer.Email,
                       Gender = customer.Gender,
                   }).ToList(),
                   Trainer = new
                   {
                       Id = g.Trainer.Id,
                       Fullname = g.Trainer.Fullname,
                       Firstname = g.Trainer.Firstname,
                       Lastname = g.Trainer.Lastname,
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
                   Schedules = g.Schedules.Select(s => new
                   {
                       Id = s.Id,
                       Title = s.Title,
                       Description = s.Description,
                       StartDate = s.StartDate,
                       EndDate = s.EndDate
                   }).ToList()
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

        public Response AddCustomerToGroup(string customerId, string groupId)
        {
            var group = _db.Groups.Where(g => g.Id == groupId).Select(g => new Group
            {
                Id = g.Id,
                Limitation = g.Limitation,
                CloseDate = g.CloseDate,
                Customers = g.Customers
            }).First();


            if (group == null)
            {
                throw new HttpRequestException($"Group Id {groupId} not found", null, HttpStatusCode.NotFound);
            }

            if (group.CloseDate < DateTime.Now)
            {
                throw new HttpRequestException("The group has been closed", null, HttpStatusCode.BadRequest);
            }

            if (group.Customers.Count >= group.Limitation)
            {
                throw new HttpRequestException("Customers or members can't over the limitation", null, HttpStatusCode.BadRequest);
            }

            _db.Groups.Attach(group);

            var customer = _db.Customers.Find(customerId);

            if (group.Customers.Contains(customer))
            {
                throw new HttpRequestException("Customer already existing in this group", null, HttpStatusCode.BadRequest);
            }

            group.Customers.Add(customer);
            _db.SaveChanges();

            return new Response { Message = $"Add customer id {customerId} to group successfully", Status = "Success" };
        }

        public Response RemoveCustomerFromGroup(string customerId, string groupId)
        {
            var group = _db.Groups.Where(g => g.Id == groupId).Select(g => new Group
            {
                Id = g.Id,
                Customers = g.Customers
            }).First();

            if (group == null)
            {
                throw new HttpRequestException($"Group Id {groupId} not found", null, HttpStatusCode.NotFound);
            }

            _db.Groups.Attach(group);

            var customer = _db.Customers.Find(customerId);

            group.Customers.Remove(customer);
            _db.SaveChanges();

            return new Response { Message = $"Remove customer id {customerId} from the group successfully", Status = "Success" };
        }

        public async Task<dynamic> UpdateGroupInfo(string groupId, GroupInfoDto data)
        {
            var group = _db.Groups.Find(groupId);
              
            if (group == null)
            {
                throw new HttpRequestException($"Group Id {groupId} not found", null, HttpStatusCode.NotFound);
            }

            group.Name = data.Name;
            group.Description = data.Description;
            group.Limitation = data.Limitation;
            group.OpenDate = data.OpenDate;
            group.CloseDate = data.CloseDate;

            if (group.TrainerId != data.TrainerId && data.TrainerId != null)
            {
                var trainer = _db.Coaches.Find(data.TrainerId);

                group.Trainer = trainer;
            }

            var groupTrainer = await _userService.GetUserInfoAsync(data.TrainerId);

            _db.SaveChanges();

            return new
            {
                Id = group.Id,
                Name = group.Name,
                Description = group.Description,
                Limitation = group.Limitation,
                OpenDate = group.OpenDate,
                CloseDate = group.CloseDate,
                Trainer = groupTrainer,
            };
        }
    }
}

