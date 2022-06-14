using System;
using System.Net;
using Gym_API.Contexts;
using Gym_API.Dto;
using Gym_API.Models;
using Gym_API.Services.Interfaces;
using Gym_API.Shared;

namespace Gym_API.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ApplicationDbContext _db;
        private readonly IUserService _userService;

        public AppointmentService(ApplicationDbContext db, IUserService userService)
        {
            _db = db;
            _userService = userService;
        }
        public dynamic AddAppointment(AppointmentDto data)
        {
            if (data.StartDate > data.EndDate)
            {
                throw new HttpRequestException("Invalid start date greater than end date", null, HttpStatusCode.BadGateway);
            }

            Appointment appointment = new Appointment
            {
                Description = data.Description,
                StartDate = data.StartDate,
                EndDate = data.EndDate,
                CoachId = data.CoachId,
                CustomerId = data.CustomerId
            };

            _db.Appointments.Add(appointment);
            _db.SaveChanges();

            dynamic newAppointment = new
            {
                Description = appointment.Description,
                StartDate = appointment.StartDate,
                EndDate = appointment.EndDate,
                CoachId = appointment.CoachId,
                CustomerId = appointment.CustomerId,
                IsActive = appointment.IsActive,
                Coach = _userService.GetUserInfoAsync(appointment.CoachId).Result,
                Customer = _userService.GetUserInfoAsync(appointment.CustomerId).Result
            };

            return newAppointment;
        }

        public dynamic AssignOrUpdateCoachAppointment(string Id, AppointmentDto body)
        {
            var appointment = _db.Appointments.Find(Id);

            if (appointment == null)
            {
                throw new HttpRequestException($"Appointment Id {Id}", null, HttpStatusCode.NotFound);
            }

            if (appointment.CoachId != body.CoachId)
            {
                var coach = _db.Coaches.Find(body.CoachId);

                if (coach == null)
                {
                    throw new HttpRequestException($"Coach Id {body.CoachId}", null, HttpStatusCode.NotFound);
                }

                appointment.Coach = coach;
            }

            if (appointment.CustomerId != body.CustomerId)
            {
                var customer = _db.Customers.Find(body.CustomerId);

                if (customer == null)
                {
                    throw new HttpRequestException($"Coach Id {body.CustomerId}", null, HttpStatusCode.NotFound);
                }

                appointment.Customer = customer;
            }


            appointment.Description = body.Description;
            appointment.StartDate = body.StartDate;
            appointment.EndDate = body.EndDate;

            _db.SaveChanges();  

            return this.GetAppointment(appointment.Id);
        }

        public Response DeleteAppointment(string Id)
        {
            var appointment = _db.Appointments.Find(Id);

            if (appointment == null)
            {
                throw new HttpRequestException($"Appointment Id {Id}", null, HttpStatusCode.NotFound);
            }

            _db.Appointments.Remove(appointment);

            _db.SaveChanges();

            return new Response { Message = $"Appointment Id {Id} deleted successfully", Status="Deleted Successfully" };

        }

        public dynamic GetAppointment(string Id)
        {
            Appointment appointment = _db.Appointments.Find(Id);

            if(appointment == null)
            {
                throw new HttpRequestException($"Appointment Id {Id} not found",null,HttpStatusCode.NotFound);
            }

            dynamic resAppointment = new
            {
                Id = appointment.Id,
                Description = appointment.Description,
                StartDate = appointment.StartDate,
                EndDate = appointment.EndDate,
                IsActive = appointment.IsActive,
                Coach = _userService.GetUserInfoAsync(appointment.CoachId).Result,
                Customer = _userService.GetUserInfoAsync(appointment.CustomerId).Result
            };

            return resAppointment;
        }

        public List<dynamic> GetAppointments()
        {
            var appointments = from appointment in _db.Appointments
                               select new
                               {
                                   Id = appointment.Id,
                                   Description = appointment.Description,
                                   StartDate = appointment.StartDate,
                                   EndDate = appointment.EndDate,
                                   Coach = _userService.GetUserInfoAsync(appointment.CoachId).Result,
                                   Customer = _userService.GetUserInfoAsync(appointment.CustomerId).Result
                               };

            return appointments.ToList<dynamic>();
        }

        public List<dynamic> GetCaochAppointments(string coachId)
        {
            var appointments = from appointment in _db.Appointments
                               where appointment.CoachId == coachId
                               select new
                               {
                                   Id = appointment.Id,
                                   Description = appointment.Description,
                                   StartDate = appointment.StartDate,
                                   EndDate = appointment.EndDate,
                                   Coach = _userService.GetUserInfoAsync(appointment.CoachId).Result,
                                   Customer = _userService.GetUserInfoAsync(appointment.CustomerId).Result
                               };

            return appointments.ToList<dynamic>();
        }

        public List<dynamic> GetCustomerAppointments(string customerId)
        {
            var appointments = from appointment in _db.Appointments
                               where appointment.CustomerId == customerId
                               select new
                               {
                                   Id = appointment.Id,
                                   Description = appointment.Description,
                                   StartDate = appointment.StartDate,
                                   EndDate = appointment.EndDate,
                                   Coach = _userService.GetUserInfoAsync(appointment.CoachId).Result,
                                   Customer = _userService.GetUserInfoAsync(appointment.CustomerId).Result
                               };

            return appointments.ToList<dynamic>();
        }
    }
}

