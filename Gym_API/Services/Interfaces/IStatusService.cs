using System;
using Gym_API.Dto;
using Gym_API.Models;
using Gym_API.Shared;

namespace Gym_API.Services.Interfaces
{
    public interface IStatusService
    {
        public List<Status> GetStatuses();
        public Task<Status> GetStatus(string id);
        public Status AddStatus(StatusDto data);
        public Response DeleteStatus(string id);
        public Status UpdateStatus(string id, StatusDto data);
    }
}

