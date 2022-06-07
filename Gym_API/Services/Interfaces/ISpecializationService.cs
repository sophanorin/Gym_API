using System;
using Gym_API.Dto;
using Gym_API.Models;
using Gym_API.Shared;

namespace Gym_API.Services.Interfaces
{
    public interface ISpecializationService
    {
        public List<Specialization> GetSpecializations();
        public Task<Specialization> GetSpecialization(string id);
        public Specialization AddSpecialization(SpecializatinDto data);
        public Response DeleteSpecialization(string id);
        public Specialization UpdateSpecialization(string id, SpecializatinDto data);
    }
}

