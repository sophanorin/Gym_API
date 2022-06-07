using System;
using System.Net;
using Gym_API.Contexts;
using Gym_API.Dto;
using Gym_API.Models;
using Gym_API.Services.Interfaces;
using Gym_API.Shared;

namespace Gym_API.Services
{
	public class SpecializationService: ISpecializationService
	{
		private ApplicationDbContext _db;

        public SpecializationService()
        {
        }

		public SpecializationService(ApplicationDbContext db)
		{
			this._db = db;
		}

        public Specialization AddSpecialization(SpecializatinDto data)
        {
            var specialization = new Specialization(data.Name);
            _db.Specializations.Add(specialization);
            _db.SaveChanges();

            return specialization;
        }

        public async Task<Specialization> GetSpecialization(string Id)
        {
            var specialization = await _db.Specializations.FindAsync(Id);
            if (specialization == null)
            {
                throw new HttpRequestException($"Specialization Id {Id} not found",null, HttpStatusCode.NotFound);
            }
            return specialization;
        }

        public List<Specialization> GetSpecializations()
        {
            return _db.Specializations.ToList();
        }

        public Response DeleteSpecialization(string id)
        {
            var existingSpecialization = _db.Specializations.Find(id);
            if(existingSpecialization != null)
            {
                _db.Specializations.Remove(existingSpecialization);
                _db.SaveChanges();
                return new Response { Message = "Successfully Deleted", Status = "Success" };
            }
            throw new HttpRequestException($"Specialization Id {id} not found", null, HttpStatusCode.NotFound);

        }
        public Specialization UpdateSpecialization(string id, SpecializatinDto data)
        {
            var existingSpecialization = _db.Specializations.Find(id);
            if (existingSpecialization != null)
            {
                existingSpecialization.Name = data.Name;
                _db.SaveChanges();
                return existingSpecialization;
            }
            throw new HttpRequestException($"Specialization Id {id} not found", null, HttpStatusCode.NotFound);
        }
    }
}

