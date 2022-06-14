using System;
using Gym_API.Dto;
using Gym_API.Models;
using Gym_API.Shared;

namespace Gym_API.Services.Interfaces
{
    public interface IAppointmentService
    {
        public dynamic GetAppointment(string Id);
        public List<dynamic> GetAppointments();
        public List<dynamic> GetCaochAppointments(string coachId);
        public List<dynamic> GetCustomerAppointments(string customerId);
        public dynamic AddAppointment(AppointmentDto data);
        public dynamic AssignOrUpdateCoachAppointment(string Id, AppointmentDto body);

        public Response DeleteAppointment(string Id);
    }
}

